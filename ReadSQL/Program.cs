using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using DataModel;
using Weibo.DataAccess;


namespace ReadSQL
{
    class Program
    {
    
        private static void OutputData(IList<WeiboData> weiboDataList)
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
        private static  WeiboData GetInputData()
        {
            WeiboData myData = new WeiboData();


            Console.WriteLine("please input description:");
            myData.WeiboDescription = Console.ReadLine();

            Console.WriteLine("Please input the imageUrl");
            myData.ImageUrl = Console.ReadLine();

            Console.WriteLine("Please input the Author");
            myData.CreatedBy = Console.ReadLine();

            myData.CreatedOn = DateTime.Now;
            
            return myData;
        }        
        private static bool AddData()
        {
            WeiboDataService dataModel = new WeiboDataService();

            if (dataModel.InsertData(GetInputData()))
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
        private static bool DeleteData()
        {
            WeiboDataService dataModel = new WeiboDataService();
            WeiboData myData = new WeiboData();
            Console.WriteLine("please input Data ID:");
            long checkdata;
            if (long.TryParse(Console.ReadLine(), out checkdata))
            {
                myData.weiboID = checkdata;
                if (dataModel.DeleteData(myData.weiboID))
                {
                    Console.WriteLine("delete success");
                    return true;
                }
                else
                {
                    Console.WriteLine("delete failed");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("please input Data ID here, such as 10008");
                return false;
            }

        }
        private static bool UpdateData()
        {
            WeiboDataService dataModel = new WeiboDataService();
            WeiboData myData = new WeiboData();
            Console.WriteLine("please input Data ID:");
            long checkdata;
            if (long.TryParse(Console.ReadLine(), out checkdata))
            {
                myData = GetInputData();
                myData.weiboID = checkdata;
                if (dataModel.UpdateData(myData))
                {
                    Console.WriteLine("Update success");
                    return true;
                }
                else
                {
                    Console.WriteLine("Update failed");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("please input Data ID here, such as 10008");
                return false;
            }

        }
        private static bool ContinueValidation()
        {
            Console.WriteLine("Do you want to quit? Y/N");
            string input2 = Console.ReadLine();
            if (input2 == "Y" || input2 == "y")
            {

                return false;
            }
            else
                return true;     
        }
        private static void ExecuteInput()
        {
            WeiboDataService dataModel = new WeiboDataService();
            var weiboDataList = dataModel.GetData();
            Console.WriteLine("What do you want to do? Search Data/Add Data/Delete Data/Update Data?\nPlease input S/A/D/U");
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
                case "D":
                    DeleteData();
                    break;
                case "d":
                    DeleteData();
                    break;
                case "U":
                    UpdateData();
                    break;
                case "u":
                    UpdateData();
                    break;
                default:
                    Console.WriteLine("input wrong value, please input S/A/D/M");
                    break;
            }
        }
        static void Main(string[] args)
        {
            ExecuteInput();
            while (ContinueValidation())
            {
                ExecuteInput();
                          
            }
              
        }
    }
}
