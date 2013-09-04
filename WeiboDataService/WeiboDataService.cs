using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using System.Data.SqlClient;


namespace Weibo.DataAccess
{
    //comment2
    // comments 3
    //commentd 4
    public class WeiboDataService
    {
        public const string ConnectString = @"Server=.\SQLExpress;database =MyTestDB;Integrated Security=true;";
        public const string QueryString = "select weiboID,WeiboDescription,ImageUrl,CreatedBy,CreatedOn,likerate from MyWeibo";
        public SqlConnection Connection { get; set; }
        public WeiboDataService()
        {
            Initialize();
        }
        private void Initialize()
        {
            //ConnectString = @"Server=.\SQLExpress;database =MyTestDB;Integrated Security=true;";
            //QueryString = "select WeiboDescription,ImageUrl,CreatedBy,CreatedOn,likerate from MyWeibo";
            Connection = new SqlConnection(ConnectString);
        }
        private bool ConnectServer()
        {
            Initialize();
            //using (SqlConnection Connection = new SqlConnection(ConnectString))

            {
                //command.Parameters.AddWithValue("@WeiboDescription", paramValue);

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
        /* get the data from input 
         */
      
        public List<WeiboData> GetData()
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
        public bool InsertData(WeiboData addData)
        {

            string insertFields = "WeiboDescription,ImageUrl,CreatedBy,CreatedOn,likerate";
            string insertString = "insert into MyWeibo ({0}) values ('{1}','{2}','{3}','{4}','{5}')";
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
        public bool DeleteData(long weiboID)
        {
            string deleteString = "Delete MyWeibo where WeiboID = {0}";
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
        public bool UpdateData(long weiboId,WeiboData updateData)
        {
            string updateString = "UPDATE MyWeibo SET WeiboDescription = '{0}',ImageUrl = '{1}',CreatedBy = '{2}',CreatedOn='{3}',likerate ='{4}' WHERE WeiboID ='{5}'";
            string updateQueryString = string.Format(updateString,updateData.WeiboDescription, updateData.ImageUrl, updateData.CreatedBy, updateData.CreatedOn, updateData.LikeRate,weiboId);
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
    }
}
    
