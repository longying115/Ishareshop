using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class AdminLoginLog
    {
        [Key]
        public string Id { get; set; }
        public int AdminId { get; set; }
        public string Ips { get; set; }
        public string Remark { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }


        public virtual Admin Admin { get; set; }
    }
}
