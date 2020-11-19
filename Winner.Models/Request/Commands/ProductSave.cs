using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.Models.Request.Commands
{
    public class ProductSave
    {
        public int Id { get; set; }
        public int FistClassId { get; set; } = 0;
        public int SecondClassId { get; set; } = 0;
        public int Sort { get; set; } = 0;
        public string Title { get; set; }
        public string SmallTitle { get; set; }
        public string Remark { get; set; }
        ///// <summary>
        ///// 每个颜色是一个SKU,作为一个单一产品，
        ///// </summary>
        ////public string color { get; set; }
        ////public decimal price { get; set; }
        public string KeyTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string SmallPicture { get; set; }
        public string PictureTag { get; set; }
        public int Sales
        {
            get;
            set;
        } = 0;
        public int Score
        {
            get;
            set;
        } = 0;
        public string TextContent { get; set; }
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
        public int Hits { get; set; } = 0;
        public DateTime GMTLastHit { get; set; } = DateTime.Now;
        private int _praise = 0;
        public int Praise
        {
            get { return _praise; }
            set { _praise = value; }
        }
        public bool IsShow { get; set; }
        public bool IsHome { get; set; }
        public bool IsNew { get; set; }
        public bool IsBest { get; set; }
        public bool IsHot { get; set; }
        public bool IsSale { get; set; }
        public bool IsFocus { get; set; }
    }
}
