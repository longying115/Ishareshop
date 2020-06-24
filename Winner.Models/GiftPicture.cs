using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class GiftPicture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int sort { get; set; }
        public int giftid { get; set; }
        public string smallpicture { get; set; }
        public virtual Gifts Gifts { get; set; }
    }
}
