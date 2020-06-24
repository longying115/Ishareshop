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
        public int id { get; set; }
        public int sort { get; set; }
        public string bannername { get; set; }
        public string smallpicture { get; set; }
        public string bigpicture { get; set; }
        public string linkurl { get; set; }
        public int columnarea { get; set; }//枚举，1代表首页，2代表列表页，3代表详情页。。。。
        public bool issmallpicture { get; set; }
        public bool isbigpicture { get; set; }
        public bool isshow { get; set; }
        public bool ismobile { get; set; }
    }
}
