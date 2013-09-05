using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace Weibo.DataAccess
{
    interface IWeiboDataService
    {
        IList<WeiboData> GetData();

    }
}
