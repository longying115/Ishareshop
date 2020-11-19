using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Texts
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
        public string SmallPicture { get; set; }
        public string KeyTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        public int CreateAdminId { get; set; }
        private DateTime _modifiedTime = DateTime.Now;
        public DateTime GMTModified
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }
        public int ModifiedAdminId { get; set; }
        public string ModifiedIp { get; set; }
        private int _hits = 0;
        public int Hits
        {
            get { return _hits; }
            set { _hits = value; }
        }
        private DateTime _lastHitTime = DateTime.Now;
        public DateTime GMTLastHit
        {
            get { return _lastHitTime; }
            set { _lastHitTime = value; }
        }
        private int _praise = 0;
        public int Praise
        {
            get { return _praise; }
            set { _praise = value; }
        }
        public string TextContent { get; set; }
        public bool IsShow { get; set; }
        public bool IsHead { get; set; }

        public virtual WebColumn WebColumn { get; set; }
    }
}
