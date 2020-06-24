using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.Models.Response
{
    public class ResponseModel
    {
        public int code { get; set; }
        public string result { get; set; }
        public dynamic data { get; set; }
    }
}
