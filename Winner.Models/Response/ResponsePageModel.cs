using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.Models.Response
{
    public class ResponsePageModel
    {
        public int code { get; set; }
        public string result { get; set; }
        public int total { get; set; }
        public dynamic data { get; set; }
    }
}
