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
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public string MsgName { get; set; }
        public string MsgAddress { get; set; }
        public string MsgPhone { get; set; }
        public string MsgEmail { get; set; }
        public string MsgTitle { get; set; }
        public string MsgContent { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        public string BackContent { get; set; }
        private DateTime _backTime = DateTime.Now;
        public DateTime GMTBack {
            get { return _backTime; }
            set { _backTime = value; }
        }
        public bool IsBack { get; set; }
        public bool IsShow { get; set; }

        public virtual WebColumn WebColumn { get; set; }
    }
}
