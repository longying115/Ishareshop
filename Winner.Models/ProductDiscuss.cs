using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ProductDiscuss
    {
        [Key]
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductColor { get; set; }
        public decimal ProductPrice { get; set; }
        public int MemberId { get; set; }
        public string Discuss { get; set; }
        public bool Tag1 { get; set; }
        public bool Tag2 { get; set; }
        public bool Tag3 { get; set; }
        public bool Tag4 { get; set; }
        public bool Tag5 { get; set; }
        public bool Tag6 { get; set; }
        public bool Tag7 { get; set; }
        public bool Tag8 { get; set; }
        public int Score { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        /// <summary>
        /// 是否回复
        /// </summary>
        public bool IsReply { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int Likes { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyContent { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        public virtual Products Products { get; set; }
        public virtual Member Member { get; set; }
    }
}
