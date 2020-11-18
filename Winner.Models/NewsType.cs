using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class NewsType
    {
        public NewsType()
        {
            this.News = new HashSet<News>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 分类排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [Required(ErrorMessage ="分类名称不能为空")]
        public string TypeName { get; set; }
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
        /// <summary>
        /// 分类页面tag标题
        /// </summary>
        public string KeyTitle { get; set; }
        /// <summary>
        /// 分类页面关键词
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 分类页面简要描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否前端显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 是否推荐显示
        /// </summary>
        public bool IsHead { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
