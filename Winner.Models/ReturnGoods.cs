using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ReturnGoods
    {
        [Key]
        public string returnnum { get; set; }
        public int memberid { get; set; }
        public string ordernum { get; set; }
        public int productid { get; set; }
        public string productname { get; set; }
        public string producttype { get; set; }
        public int colorid { get; set; }
        public string productcolor { get; set; }
        public decimal productprice { get; set; }
        public int productcount { get; set; }
        public int returntype { get; set; }
        public string remark { get; set; }

        private DateTime _addtime = DateTime.Now;
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        public int returnstate { get; set; }
        public string returntime { get; set; }
        public string expresstype { get; set; }
        public string expressnum { get; set; }
        public string area { get; set; }
        public string consigneename { get; set; }
        public string consigneephone { get; set; }
        public string consigneetelephone { get; set; }
        public string consigneeaddress { get; set; }
        public string consigneeemail { get; set; }
        public string chukutime { get; set; }
        public string donetime { get; set; }

        public virtual Member Member { get; set; }

        public virtual Express Express { get; set; }
        public virtual ICollection<ReturnPicture> ReturnPicture { get; set; }
    }
}
