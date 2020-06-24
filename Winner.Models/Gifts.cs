using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Gifts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int classid { get; set; }
        private int _sort = 0;
        public int sort
        {
            get { return _sort; }
            set { _sort = value; }
        }
        public string title { get; set; }
        public string smalltitle { get; set; }
        public string remark { get; set; }
        public Decimal price { get; set; }
        public string keytitle { get; set; }
        public string keywords { get; set; }
        public string keydescription { get; set; }
        public string smallpicture { get; set; }
        public string bigpicture { get; set; }
        private int _sales = 0;
        public int sales
        {
            get { return _sales; }
            set { _sales = value; }
        }
        private int _xinpoint = 0;
        public int xinpoint
        {
            get { return _xinpoint; }
            set { _xinpoint = value; }
        }
        public string textcontent { get; set; }
        public string parameter { get; set; }
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
        public DateTime _hitstime = DateTime.Now;
        public DateTime hitstime
        {
            get { return _hitstime; }
            set { _hitstime = value; }
        }
        public bool isshow { get; set; }
        public bool ishome { get; set; }
        public bool ispicture { get; set; }

        public virtual GiftClass GiftClass { get; set; }
        public virtual ICollection<GiftPicture> GiftPicture { get; set; }
    }
}
