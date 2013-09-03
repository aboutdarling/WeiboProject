using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using System.Data.SqlClient;


namespace Weibo.DataAccess
{
    //test version 1
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
        public  List<WeiboData> GetData()
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
        public  bool InsertData(WeiboData addData)
        {
           
          //string   insertString = "insert into MyWeibo values ('"+addData.WeiboDescription+"','"+addData.ImageUrl+"','"
          //    +addData.CreatedBy+"','"+addData.CreatedOn+"',"+addData.LikeRate+")";
          string insertFields = "WeiboDescription,ImageUrl,CreatedBy,CreatedOn,likerate";
          string insertString = "insert into MyWeibo ({0}) values ('{1}','{2}','{3}','{4}','{5}')";
          string insertQueryString = string.Format(insertString,insertFields,  addData.WeiboDescription, addData.ImageUrl, addData.CreatedBy, addData.CreatedOn, addData.LikeRate);
          if (ConnectServer())
          {
              using (SqlCommand command = new SqlCommand())
              {
                  try
                  {
                      command.CommandText = insertQueryString;
                      command.Connection = Connection;
                      command.ExecuteNonQuery();
                      Connection.Close();
                      Connection.Dispose();
                      return true;
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
          else
          {
              return false;
          }
                   
        }
    
    }
}

    