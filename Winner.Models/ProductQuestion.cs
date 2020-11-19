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
        public int Id { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 产品问
        /// </summary>
        public string Question { get; set; }
        private DateTime _questionTime = DateTime.Now;
        public DateTime GMTQuestion {
            get { return _questionTime; }
            set { _questionTime = value; }
        }
        /// <summary>
        /// 产品答
        /// </summary>
        public string Answer { get; set; }
        private DateTime _answerTime = DateTime.Now;
        /// <summary>
        /// 回答时间
        /// </summary>
        public DateTime Answertime 
        { 
            get { return _answerTime; }
            set { _answerTime = value; }
        }
        public int Praise { get; set; }
        public bool Isshow { get; set; }

        public virtual Products Products { get; set; }
    }
}
