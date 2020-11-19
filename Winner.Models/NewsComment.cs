using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Winner.Models
{
    public class NewsComment
    {
        [Key]
        public string Id { get; set; }
        public string NewsId { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string Contents { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        public bool IsShow { get; set; }
        public bool IsHead { get; set; }

        public virtual News News { get; set; }
    }
}
