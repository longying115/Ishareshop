using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class WebSite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string webname { get; set; }
        public string webkeywords { get; set; }
        public string webdescription { get; set; }
        public string webphone { get; set; }
        public string webtelephone { get; set; }
        public string webemail { get; set; }
        public string webtencent { get; set; }
        public string webweixin { get; set; }
        public string webcopyright { get; set; }
        public string webicp { get; set; }
        public string webfax { get; set; }
        public string webaddress { get; set; }
    }
}
