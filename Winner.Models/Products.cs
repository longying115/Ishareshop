using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [RegularExpression(@"[1-9]*$", ErrorMessage = "请选择产品类别")]
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
        public decimal price { get; set; }
        public string keytitle { get; set; }
        public string keywords { get; set; }
        public string keydescription { get; set; }
        public string smallpicture { get; set; }
        public string bigpicture { get; set; }
        public string hotpicture { get; set; }
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
        public bool isnew { get; set; }
        public bool isbest { get; set; }
        public bool ishot { get; set; }
        public bool issale { get; set; }
        public bool isfocus { get; set; }
        public bool ispicture { get; set; }

        public virtual ProductClass ProductClass { get; set; }
        public virtual ICollection<ProductColor> ProductColor { get; set; }
        public virtual ICollection<ProductPicture> ProductPicture { get; set; }
        public virtual ICollection<ProductDiscuss> ProductDiscuss { get; set; }
        public virtual ICollection<ProductQuestion> ProductQuestion { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
    }
}
