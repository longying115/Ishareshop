using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ShippingAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string RealyName { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Telephone { get; set; }
        public string Post { get; set; }
        public string Email { get; set; }
        public string Othername { get; set; }
        public DateTime Addtime { get; set; }
        public bool IsDefault { get; set; }
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
        public string ModifiedIp { get; set; }

        public virtual Member Member { get; set; }
    }
}
