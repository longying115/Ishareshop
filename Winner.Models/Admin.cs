using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string username { get; set; }
        public string passstring { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public string telephone { get; set; }
        public string qqnumber { get; set; }
        public string weixin { get; set; }
        public string lastloginip { get; set; }
        public DateTime lastlogintime { get; set; }
        public int power { get; set; }
        public string flag { get; set; }
        public bool isadd { get; set; }
        public bool isdel { get; set; }
        public bool isedit { get; set; }
        public bool status { get; set; }

        public virtual ICollection<AdminLoginLog> AdminLoginLog { get; set; }
    }
}
