using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.Models.Response
{
    public class BannerModel
    {
        public int id { get; set; }
        public int sort { get; set; }
        public string bannername { get; set; }
        public string smallpicture { get; set; }
        public string bigpicture { get; set; }
        public string linkurl { get; set; }
        public int columnarea { get; set; }
        public bool issmallpicture { get; set; }
        public bool isbigpicture { get; set; }
        public bool isshow { get; set; }
        public bool ismobile { get; set; }
    }
}
