using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataModel
{
    public class WeiboData
    {        


        #region Properties
        
        public long weiboID { get; set; }
        public string WeiboDescription { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LikeRate { get; set; }

    
        #endregion

        #region Methods

        #endregion
    }
}
