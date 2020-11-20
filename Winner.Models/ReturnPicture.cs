using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ReturnPicture
    {
        [Key]
        public string ReturnCode { get; set; }
        public string Picture { get; set; }

        public virtual ReturnGoods ReturnGoods { get; set; }
    }
}
