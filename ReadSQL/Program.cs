using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
//using System.Data.ADO;
using System.Data.SqlClient;
using System.Drawing;
using DataModel;
using Weibo.DataAccess;


namespace ReadSQL
{
    class Program
    {
        //private static void GetSqlData(out SqlDataReader reader)
        //{
        //    string connectString = @"Server=.\SQLExpress;database =MyTestDB;Integrated Security=true;";
        //    string queryString = "select WeiboDescription,CreatedBy,CreatedOn,likerate from MyWeibo";
        //    //            string paramValue = "add my description here";

        //    using (SqlConnection conn = new SqlConnection(connectString))
        //    {
        //        SqlCommand command = new SqlCommand(queryString, conn);
        //        //command.Parameters.AddWithValue("@WeiboDescription", paramValue);

        //        try
        //        {
        //            conn.Open();
        //            reader = command.ExecuteReader();
        //        }

        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);

        //        }
        //    }
        //}

        private static void OutputData(List<WeiboData> weiboDataList)
        {
            foreach (var weiboData in weiboDataList)
            {
                Console.WriteLine("ID : {0}", weiboData.weiboID);
                Console.WriteLine(weiboData.WeiboDescription);
                Console.WriteLine("ImageUrl:{0}",weiboData.ImageUrl);
                Console.WriteLine("Created By: {0}",weiboData.CreatedBy);
                Console.WriteLine("CreatedOn: {0}" , weiboData.CreatedOn);
                Console.WriteLine(" {0} People like this", weiboData.LikeRate);
                Console.WriteLine("---------------------------------------");
            }

        }
        
        private static bool AddData()
        {
            WeiboDataService dataModel = new WeiboDataService();
            WeiboData myData = new WeiboData();

//            myData.weiboID = 
            Console.WriteLine("please input description:");
            myData.WeiboDescription = Console.ReadLine();

            Console.WriteLine("Please input the imageUrl");
//            myData.ImageUrl = "http://ww3.sinaimg.cn/thumbnail/5487fa6cgw1e87rrax91ej20p00gowfa.jpg";
            myData.ImageUrl = Console.ReadLine();

            Console.WriteLine("Please input the Author");
            myData.CreatedBy = Console.ReadLine();

            myData.CreatedOn = DateTime.Now;
 //           myData.LikeRate = "default";

            if (dataModel.InsertData(myData))
            {
                Console.WriteLine("add data succeed");
                return true;
            }
            else
            {
                Console.WriteLine("add data failed");
                return false;
            }
        }
        static void Main(string[] args)
        {
            WeiboDataService dataModel = new WeiboDataService();
            var weiboDataList = dataModel.GetData();
            bool a = true;
            while (a)
            {
                Console.WriteLine("What do you want to do? Search Data or Add Data? S/A");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "A": 
                       AddData();
                        break;
                    case "a":
                        AddData();
                        break;
                    case "S":
                        weiboDataList = dataModel.GetData();
                        OutputData(weiboDataList);
                        break;
                    case "s":
                        weiboDataList = dataModel.GetData();
                        OutputData(weiboDataList);
                        break;
                    default:
                        Console.WriteLine("input wrong value, please input S or A");
                        break;
                }
                Console.WriteLine("Do you want to quit? Y/N");
                string input2 = Console.ReadLine();
                if (input2 == "Y" || input2 == "y")
                {
                   
                    a = false;
                }
                else
                    a = true;                
            }
              
        }
    }
}
