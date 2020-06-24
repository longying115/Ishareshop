using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ProductQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int productid { get; set; }
        public string question { get; set; }
        private DateTime _questiontime = DateTime.Now;
        public DateTime questiontime {
            get { return _questiontime; }
            set { _questiontime = value; }
        }
        public string answer { get; set; }
        public string answertime { get; set; }
        public int praise { get; set; }
        public bool isshow { get; set; }
        public virtual Products Products { get; set; }
    }
}
