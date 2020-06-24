using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int tid { get; set; }
        public string mesname { get; set; }
        public string mesaddress { get; set; }
        public string mesphone { get; set; }
        public string mesmail { get; set; }
        public string mestitle { get; set; }
        public string mescontent { get; set; }
        private DateTime _addtime = DateTime.Now;
        public DateTime addtime {
            get { return _addtime; }
            set { _addtime = value; }
        }
        public string backcontent { get; set; }
        private DateTime _backtime = DateTime.Now;
        public DateTime backtime {
            get { return _backtime; }
            set { _backtime = value; }
        }
        public bool isback { get; set; }
        public bool isshow { get; set; }

        public virtual WebColumn WebColumn { get; set; }
    }
}
