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
        public int Id { get; set; }
        public int ColumnId { get; set; }
        private int _sort = 0;
        public int Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }
        public string Title { get; set; }
        public string KeyTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string TextContent { get; set; }
        public string Picture { get; set; }
        public string VideoUrl { get; set; }
        public string VideoName { get; set; }
        public string Source { get; set; }
        private int _hits = 0;
        public int Hits
        {
            get { return _hits; }
            set { _hits = value; }
        }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        public bool IsShow { get; set; }
        public bool IsHead { get; set; }

        public virtual WebColumn WebColumn { get; set; }
    }
}
