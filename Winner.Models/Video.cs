using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Video
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int tid { get; set; }
        private int _sort = 0;
        public int sort
        {
            get { return _sort; }
            set { _sort = value; }
        }
        public string title { get; set; }
        public string keytitle { get; set; }
        public string keywords { get; set; }
        public string keydescription { get; set; }
        public string textcontent { get; set; }
        public string smallpicture { get; set; }
        public string videourl { get; set; }
        public string videoname { get; set; }
        public string source { get; set; }
        private int _hits = 0;
        public int hits
        {
            get { return _hits; }
            set { _hits = value; }
        }
        private DateTime _addtime = DateTime.Now;
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        public bool isshow { get; set; }
        public bool ishead { get; set; }

        public virtual WebColumn WebColumn { get; set; }
    }
}
