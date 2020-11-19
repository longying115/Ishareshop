using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Banner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Sort { get; set; }
        public string BannerName { get; set; }
        public string Picture { get; set; }
        public string BackgroundImg { get; set; }
        public string LinkUrl { get; set; }
        public int ColumnArea { get; set; }//枚举，1代表首页，2代表列表页，3代表详情页。。。。
        public bool IsShow { get; set; }
        public bool IsMobile { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        public int CreateAdminId { get; set; }
        private DateTime _modifiedTime = DateTime.Now;
        public DateTime GMTModified
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }
        public int ModifiedAdminId { get; set; }
        public string ModifiedIp { get; set; }
    }
}
