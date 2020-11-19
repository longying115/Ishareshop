using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.Models.Request.Commands
{
    public class BannerSave
    {
        public int Id { get; set; }
        public int Sort { get; set; }
        public string BannerName { get; set; }
        public string SmallPicture { get; set; }
        public string BigPicture { get; set; }
        public string LinkUrl { get; set; }
        public int ColumnArea { get; set; }
        public bool IsShow { get; set; }
        public bool IsMobile { get; set; }
    }
}
