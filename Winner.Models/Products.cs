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
        public int Id { get; set; }
        /// <summary>
        /// 产品所属一级分类
        /// </summary>
        [Required(ErrorMessage ="产品分类不能为空")]
        [RegularExpression(@"[0-9]*$", ErrorMessage = "请选择产品类别")]
        public int FistClassId { get; set; } = 0;
        /// <summary>
        /// 产品所属二级分类
        /// </summary>
        public int? SecondClassId { get; set; } = 0;
        /// <summary>
        /// 产品排序号
        /// </summary>
        public int Sort { get; set; } = 0;
        /// <summary>
        /// 产品标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        public string Title { get; set; }
        /// <summary>
        /// 产品小标题
        /// </summary>
        public string SmallTitle { get; set; }
        /// <summary>
        /// 产品备注说明
        /// </summary>
        public string Remark { get; set; }
        ///// <summary>
        ///// 每个颜色是一个SKU,作为一个单一产品，
        ///// </summary>
        ////public string color { get; set; }
        ////public decimal price { get; set; }
        /// <summary>
        /// 产品详情页tag显示标题
        /// </summary>
        public string KeyTitle { get; set; }
        /// <summary>
        /// 产品详情页关键词
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 产品详情页简要描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 产品展示小图
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 图片标签
        /// </summary>
        public string PictureTag { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        public int Sales
        {
            get;
            set;
        } = 0;
        /// <summary>
        /// 得分，积分
        /// </summary>
        public int Score
        {
            get;
            set;
        } = 0;
        /// <summary>
        /// 详情描述--富文本
        /// </summary>
        public string TextContent { get; set; }
        /// <summary>
        /// 参数--富文本
        /// </summary>
        public string Parameter { get; set; }

        private DateTime _addTime = DateTime.Now;
        /// <summary>
        /// 创建时间
        /// </summary>
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        private DateTime _modifiedTime = DateTime.Now;
        public DateTime GMTModified
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }
        /// <summary>
        /// 点击率
        /// </summary>
        public int Hits { get; set; } = 0;
        /// <summary>
        /// 最后点击时间
        /// </summary>
        public DateTime GMTLastHit{get;set;} = DateTime.Now;
        private int? _praise = 0;
        /// <summary>
        /// 点赞次数
        /// </summary>
        public int? Praise
        {
            get { return _praise; }
            set { _praise = value; }
        }
        /// <summary>
        /// 是否展示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 是否首页展示
        /// </summary>
        public bool IsHome { get; set; }
        /// <summary>
        /// 是否最新产品
        /// </summary>
        public bool IsNew { get; set; }
        /// <summary>
        /// 是否
        /// </summary>
        public bool IsBest { get; set; }
        /// <summary>
        /// 是否热销产品
        /// </summary>
        public bool IsHot { get; set; }
        /// <summary>
        /// 是否促销产品
        /// </summary>
        public bool IsSale { get; set; }
        /// <summary>
        /// 是否焦点产品
        /// </summary>
        public bool IsFocus { get; set; }

        /// <summary>
        /// 产品分类
        /// </summary>
        public virtual ProductClass ProductClass { get; set; }
        /// <summary>
        /// 产品颜色
        /// </summary>
        public virtual ICollection<ProductColor> ProductColor { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public virtual ICollection<ProductPicture> ProductPicture { get; set; }
        /// <summary>
        /// 产品评论
        /// </summary>
        public virtual ICollection<ProductDiscuss> ProductDiscuss { get; set; }
        /// <summary>
        /// 产品问答
        /// </summary>
        public virtual ICollection<ProductQuestion> ProductQuestion { get; set; }
        /// <summary>
        /// 产品收藏
        /// </summary>
        public virtual ICollection<Favorites> Favorites { get; set; }
    }
}
