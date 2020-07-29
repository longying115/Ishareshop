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
        public int fistclassid { get; set; } = 0;
        public int secondclassid { get; set; } = 0;
        public int sort { get; set; } = 0;
        [Required(ErrorMessage = "标题不能为空")]
        public string title { get; set; }
        public string smalltitle { get; set; }
        public string remark { get; set; }
        /// <summary>
        /// 每个颜色是一个SKU,作为一个单一产品，
        /// </summary>
        //public string color { get; set; }
        //public decimal price { get; set; }
        public string keytitle { get; set; }
        public string keywords { get; set; }
        public string keydescription { get; set; }
        public string smallpicture { get; set; }
        public bool ispicture { get; set; }
        public string bigpicture { get; set; }
        public string hotpicture { get; set; }
        public int sales
        {
            get;
            set;
        } = 0;
        public int xinpoint
        {
            get;
            set;
        } = 0;
        public string textcontent { get; set; }
        public string parameter { get; set; }
        private DateTime _addtime = DateTime.Now;
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        public int hits { get; set; } = 0;
        public DateTime hitstime{get;set;} = DateTime.Now;
        public bool isshow { get; set; }
        public bool ishome { get; set; }
        public bool isnew { get; set; }
        public bool isbest { get; set; }
        public bool ishot { get; set; }
        public bool issale { get; set; }
        public bool isfocus { get; set; }


        public virtual ProductClass ProductClass { get; set; }
        public virtual ICollection<ProductColor> ProductColor { get; set; }
        public virtual ICollection<ProductPicture> ProductPicture { get; set; }
        public virtual ICollection<ProductDiscuss> ProductDiscuss { get; set; }
        public virtual ICollection<ProductQuestion> ProductQuestion { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
    }
}
