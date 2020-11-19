using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class PhoneCode
    {
        [Key]
        public string Id { get; set; }
        public string Phone { get; set; }
        public string MachineCode { get; set; }
        public string Code { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
    }
}
