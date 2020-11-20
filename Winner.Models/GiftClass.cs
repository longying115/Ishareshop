using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class GiftClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int Sort { get; set; }
        public string ClassName { get; set; }
        public string Remark { get; set; }
        public string KeyTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public bool IsShow { get; set; }

        public virtual ICollection<Gifts> Gifts { get; set; }
    }
}
