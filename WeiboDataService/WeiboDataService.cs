using System;
using System.Collections.Generic;
using DataModel;
using System.Data;
using System.Data.SqlClient;


namespace Weibo.DataAccess
{

    public class WeiboDataService : IWeiboDataService
    {
        #region const string
        public const string ConnectString = @"Server=.\SQLExpress;database =MyTestDB;Integrated Security=true;";
        public const string QueryProcedureName = "query_MyWeibo";
        public const string InsertProcedureName = "insertData_MyWeibo";
        public const string DeleteProcedureName = "deleteData_MyWeibo";
        public const string UpdateProcedureName = "updateData_MyWeibo";

  
        #endregion

        #region private method

        private void Initialize()
        {
            Connection = new SqlConnection(ConnectString);
        }
        private bool ConnectServer()
        {
            Initialize();

            {

                try
                {
                    Connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        private void CommandAddParameter(SqlCommand command, WeiboData data, params SqlParameter[] additionalParams)
        {
            foreach (var sqlParam in ConstructParams(data))
            {
                command.Parameters.Add(sqlParam);
            }

            foreach (var sqlParam in additionalParams)
            {
                command.Parameters.Add(sqlParam);
            }
        }

        private IEnumerable<SqlParameter> ConstructParams(WeiboData data)
        {
            var sqlParams = new List<SqlParameter> () {
                ConstructSqlParameter("@weiboDescription", SqlDbType.NVarChar, data.WeiboDescription, 140),
                ConstructSqlParameter("@imageUrl", SqlDbType.NVarChar, data.ImageUrl, 380),
                ConstructSqlParameter("@createdBy", SqlDbType.NVarChar, data.CreatedBy, 40),
                ConstructSqlParameter("@createdOn", SqlDbType.DateTime, data.CreatedOn),
                ConstructSqlParameter("@likerate", SqlDbType.Int, data.LikeRate)
            };

            return sqlParams;
        }

        private static SqlParameter ConstructSqlParameter(string name, SqlDbType type, object value = null, 
            int size = -1, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            var sqlParam = new SqlParameter
                {
                    ParameterName = name,
                    SqlDbType = type,
                    Direction = parameterDirection
                };

            if (size != -1) sqlParam.Size = size;
            if (value != null) sqlParam.Value = value;

            return sqlParam;
        }

        private bool ExecuteQuery(SqlCommand command)
        {
            if (command.ExecuteNonQuery() <= 0)
            {
                Connection.Close();
                Connection.Dispose();
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region public method
        public SqlConnection Connection { get; set; }
        public WeiboDataService()
        {
            Initialize();
        }
        public IList<WeiboData> GetData()
        {
            List<WeiboData> weiboDataList = new List<WeiboData>();
            if (ConnectServer())
            {
                SqlCommand command = new SqlCommand(QueryProcedureName, Connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    WeiboData currentData = new WeiboData();
                    currentData.weiboID = int.Parse(reader[0].ToString());
                    currentData.WeiboDescription = reader[1].ToString();
                    currentData.ImageUrl = reader[2].ToString();
                    currentData.CreatedBy = reader[3].ToString();
                    currentData.CreatedOn = Convert.ToDateTime(reader[4]);
                    currentData.LikeRate = int.Parse(reader[5].ToString());
                    weiboDataList.Add(currentData);
                }
                reader.Close();
            }
            Connection.Close();
            Connection.Dispose();
            return weiboDataList;
        }
        public bool InsertData(WeiboData addData)
        {
            if (ConnectServer())
            {
                var command = new SqlCommand(InsertProcedureName, Connection)
                    {CommandType = CommandType.StoredProcedure};
                CommandAddParameter(command, addData, 
                    ConstructSqlParameter("@weiboID", SqlDbType.BigInt, null, -1, ParameterDirection.Output));

                return ExecuteQuery(command);      
            }
            
            return false;
        }

        public bool DeleteData(long weiboID)
        {
            if (ConnectServer())
            {
                var command = new SqlCommand(DeleteProcedureName, Connection)
                    {CommandType = CommandType.StoredProcedure};
                command.Parameters.Add(ConstructSqlParameter("@weiboID", SqlDbType.BigInt, weiboID));
                return ExecuteQuery(command);            

            }
            
            return false;
        }

        public bool UpdateData(WeiboData updateData)
        {
            if (ConnectServer())
            {
                var command = new SqlCommand(UpdateProcedureName, Connection)
                    {CommandType = CommandType.StoredProcedure};
                CommandAddParameter(command, updateData,
                    ConstructSqlParameter("@weiboID", SqlDbType.BigInt, updateData.weiboID));
              
                return ExecuteQuery(command);      
            }
            
            return false;
        }

        #endregion
    }
}
    
