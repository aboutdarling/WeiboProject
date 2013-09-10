using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace Weibo.DataAccess
{
    public interface IWeiboDataService
    {
        IList<WeiboData> GetData();
        bool InsertData(WeiboData addData);
        bool DeleteData(long weiboID);
        bool UpdateData(WeiboData updateData);
    }
}
