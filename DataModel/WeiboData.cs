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
        #region Fields
        //private string weiboDescription; //
        //private string imageUrl;
        //private string createdBy;
        //private DateTime createdOn;
        //private int likeRate;
        #endregion

        #region Properties

        //public string WeiboDescription
        //{
        //    get { return weiboDescription; }
        //    set { this.weiboDescription = value; }
        //}
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
