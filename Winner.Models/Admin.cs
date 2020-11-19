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
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassString { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Telephone { get; set; }
        public string QQNumber { get; set; }
        public string WeChat { get; set; }
        public int Power { get; set; }
        public string Flag { get; set; }
        public bool IsAdd { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEdit { get; set; }
        public bool Status { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        private DateTime _modifiedTime = DateTime.Now;
        public DateTime GMTModified 
        { 
            get { return _modifiedTime; } 
            set { _modifiedTime = value; }
        }
        private DateTime _lastLoginTime = DateTime.Now;
        public DateTime GMTLastLogin 
        {
            get { return _lastLoginTime; }
            set { _lastLoginTime = value; }
        }
        public string LastLoginIp { get; set; }

        public virtual ICollection<AdminLoginLog> AdminLoginLog { get; set; }
    }
}
