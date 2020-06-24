using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class News
    {
        public News()
        {
            this.NewsComment = new HashSet<NewsComment>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int tid { get; set; }
        public string title { get; set; }
        public string keytitle { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public string author { get; set; }
        public string source { get; set; }
        public string smallpicture { get; set; }
        public string picturetag { get; set; }
        private DateTime _addtime = DateTime.Now;
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private int _hits = 0;
        public int hits
        {
            get { return _hits; }
            set { _hits = value; }
        }
        private DateTime _lasthittime = DateTime.Now;
        public DateTime lasthittime
        {
            get { return _lasthittime; }
            set { _lasthittime = value; }
        }
        private int _praise = 0;
        public int praise
        {
            get { return _praise; }
            set { _praise = value; }
        }
        public string textcontent { get; set; }
        public bool ispicture { get; set; }
        public bool isshow { get; set; }
        public bool ishead { get; set; }

        public virtual NewsType NewsType { get; set; }
        public virtual ICollection<NewsComment> NewsComment { get; set; }
    }
}
