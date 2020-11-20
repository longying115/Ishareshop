using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.Models.Response
{
    public class ProductClassDto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ClassLevel { get; set; }
        public int Sort { get; set; }
        public string ClassName { get; set; }
        public string ClassRemark { get; set; }
        public string KeyTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureTag { get; set; }
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
        private DateTime _lastHitTime = DateTime.Now;
        /// <summary>
        /// 最后点击时间
        /// </summary>
        public DateTime GMTLastHit
        {
            get { return _lastHitTime; }
            set { _lastHitTime = value; }
        }
        public bool IsShow { get; set; }
        public bool IsHead { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
