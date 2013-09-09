using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private void CommandAddParameter( SqlCommand command, WeiboData data)
        {
            command.Parameters.Add("@weiboDescription", SqlDbType.NVarChar, 140).Value = data.WeiboDescription;
            command.Parameters.Add("@imageUrl", SqlDbType.NVarChar, 380).Value = data.ImageUrl;
            command.Parameters.Add("@createdBy", SqlDbType.NVarChar, 40).Value = data.CreatedBy;
            command.Parameters.Add("@createdOn", SqlDbType.DateTime).Value = data.CreatedOn;
            command.Parameters.Add("@likerate", SqlDbType.Int).Value = data.LikeRate;
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
                SqlCommand command = new SqlCommand(InsertProcedureName, Connection);
                command.CommandType = CommandType.StoredProcedure;
                CommandAddParameter(command, addData);
                command.Parameters.Add("@weiboID", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                return ExecuteQuery(command);      
            }
            else
            {
                return false;
            }

        }
        public bool DeleteData(long weiboID)
        {
            if (ConnectServer())
            {
                SqlCommand command = new SqlCommand(DeleteProcedureName, Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@weiboID", SqlDbType.BigInt).Value = weiboID;
                return ExecuteQuery(command);            

            }
            else
            { return false; }
        }
        public bool UpdateData(WeiboData updateData)
        {
            if (ConnectServer())
            {
                SqlCommand command = new SqlCommand(UpdateProcedureName, Connection);
                command.CommandType = CommandType.StoredProcedure;
                CommandAddParameter(command, updateData);
                command.Parameters.Add("@weiboID", SqlDbType.BigInt).Value = updateData.weiboID;
              
                return ExecuteQuery(command);      
            }
            else
            { return false; }

        }
        #endregion
    }
}
    
