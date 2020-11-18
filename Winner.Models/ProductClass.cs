﻿using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ProductClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int ClassLevel { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        public string ClassName { get; set; }
        public string ClassRemark { get; set; }
        public string KeyTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string SmallPicture { get; set; }
        public string PictureTag { get; set; }
        private DateTime _addTime = DateTime.Now;
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _addTime; }
            set { _addTime = value; }
        }
        private DateTime _lastHitTime = DateTime.Now;
        /// <summary>
        /// 最后点击时间
        /// </summary>
        public DateTime LastHitTime
        {
            get { return _lastHitTime; }
            set { _lastHitTime = value; }
        }
        public bool IsShow { get; set; }
        public bool IsHead { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
