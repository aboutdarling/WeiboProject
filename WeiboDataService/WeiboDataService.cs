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
        public const string QueryString = "select weiboID,WeiboDescription,ImageUrl,CreatedBy,CreatedOn,likerate from MyWeibo";
        public const string QueryProcedureName = "query_MyWeibo";
        public const string InsertProcedureName = "insertData_MyWeibo";
        public const string deleteString = "Delete MyWeibo where WeiboID = {0}";
        public const string insertFields = "WeiboDescription,ImageUrl,CreatedBy,CreatedOn,likerate";
        public const string insertString = "insert into MyWeibo ({0}) values ('{1}','{2}','{3}','{4}','{5}')";

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
        private bool ExcecuteSQLQuery(string queryString)
        {
            using (SqlCommand command = new SqlCommand())
            {
                try
                {
                    command.CommandText = queryString;
                    command.Connection = Connection;
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Connection.Close();
                    Connection.Dispose();
                    return false;

                }
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
        public IList<WeiboData> GetData2() //old method
        {
            
            List<WeiboData> weiboDataList = new List<WeiboData>();

            if (ConnectServer())
            {
                SqlCommand command = new SqlCommand(QueryString, Connection);
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
        public bool InsertData2(WeiboData addData)
        {

            string insertQueryString = string.Format(insertString, insertFields, addData.WeiboDescription, addData.ImageUrl, addData.CreatedBy, addData.CreatedOn, addData.LikeRate);
            if (ConnectServer())
            {
                if (ExcecuteSQLQuery(insertQueryString))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        public bool InsertData(WeiboData addData)
        {

            string insertQueryString = string.Format(insertString, insertFields, addData.WeiboDescription, addData.ImageUrl, addData.CreatedBy, addData.CreatedOn, addData.LikeRate);
            
            if (ConnectServer())
            {
                SqlCommand command = new SqlCommand(InsertProcedureName, Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@weiboDescription", SqlDbType.NVarChar, 140).Value = addData.WeiboDescription;
                command.Parameters.Add("@imageUrl", SqlDbType.NVarChar, 380).Value = addData.ImageUrl;
                command.Parameters.Add("@createdBy", SqlDbType.NVarChar, 40).Value = addData.CreatedBy;
                command.Parameters.Add("@createdOn", SqlDbType.DateTime).Value = addData.CreatedOn;
                command.Parameters.Add("@likerate", SqlDbType.Int).Value = addData.LikeRate;
                
  //              long weiboID = (long)command.Parameters["@weiboID"].Value;

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
            else
            {
                return false;
            }

        }
        public bool DeleteData(long weiboID)
        {
            string deleteQueryString = string.Format(deleteString, weiboID);
            if (ConnectServer())
            {
                if (ExcecuteSQLQuery(deleteString))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            { return false; }
        }
        public bool UpdateData(WeiboData updateData)
        {
            string updateString = "UPDATE MyWeibo SET WeiboDescription = '{0}',ImageUrl = '{1}',CreatedBy = '{2}',CreatedOn='{3}',likerate ='{4}' WHERE WeiboID ='{5}'";
            string updateQueryString = string.Format(updateString,updateData.WeiboDescription, updateData.ImageUrl, updateData.CreatedBy, updateData.CreatedOn, updateData.LikeRate,updateData.weiboID);
            if (ConnectServer())
            {
                if (ExcecuteSQLQuery(updateQueryString))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            { return false; }

        }
        #endregion
    }
}
    
