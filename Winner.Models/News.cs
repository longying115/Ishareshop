using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class News
    {
        public News()
        {
            this.NewsComment = new HashSet<NewsComment>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 新闻所属分类
        /// </summary>
        [Required(ErrorMessage ="新闻分类不能为空")]
        [RegularExpression(@"[0-9]*$", ErrorMessage = "请选择新闻分类")]
        public int ClassId { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        [Required(ErrorMessage ="新闻标题不能为空")]
        public string Title { get; set; }
        /// <summary>
        /// 详情页tag显示标题
        /// </summary>
        public string KeyTitle { get; set; }
        /// <summary>
        /// 详情页关键词
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 详情页简要描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 新闻展示小图
        /// </summary>
        public string SmallPicture { get; set; }
        /// <summary>
        /// 图片标签
        /// </summary>
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
        private int _hits = 0;
        /// <summary>
        /// 点击率
        /// </summary>
        public int Hits
        {
            get { return _hits; }
            set { _hits = value; }
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
        private int _praise = 0;
        /// <summary>
        /// 点赞次数
        /// </summary>
        public int Praise
        {
            get { return _praise; }
            set { _praise = value; }
        }
        /// <summary>
        /// 新闻内容--富文本
        /// </summary>
        [Required(ErrorMessage ="新闻内容不能为空")]
        public string TextContent { get; set; }
        /// <summary>
        /// 是否前端展示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 是否首页展示
        /// </summary>
        public bool IsHome { get; set; }
        /// <summary>
        /// 是否推荐展示
        /// </summary>
        public bool IsHead { get; set; }
        /// <summary>
        /// 新闻分类
        /// </summary>
        public virtual NewsType NewsType { get; set; }
        /// <summary>
        /// 新闻评论
        /// </summary>
        public virtual ICollection<NewsComment> NewsComment { get; set; }
    }
}
