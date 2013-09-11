using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataModel;
using ReadSQL;
using Weibo.DataAccess;
namespace ASP.Controllers
{
    public class DataController : ApiController
    {
        private static IWeiboDataService dataService = new WeiboDataService();
        private IEnumerable<WeiboData> weiboDataList = dataService.GetData();

        /* /api/data */
        public IEnumerable<WeiboData> GetAllWeiboData()
        {
            return weiboDataList;
        }

        /* /api/data/id */
        public WeiboData GetWeiboById(int id)
        {
            var weiboData = new WeiboData();
            foreach (var currentdata in weiboDataList)
            {
                if (currentdata.weiboID == id)
                {
                    weiboData = currentdata;
                    break;
                }
            }
            if (weiboData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return weiboData;
        }
    }
}
