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
        public string returnnum { get; set; }
        public string smallpicture { get; set; }

        public virtual ReturnGoods ReturnGoods { get; set; }
    }
}
