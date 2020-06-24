using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class WebColumn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string columnname { get; set; }
        public string banner { get; set; }
        public string bannerbg { get; set; }
        public string keytitle { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public int columnlevel { get; set; }
        public int columntype { get; set; }
        public int pernode { get; set; }
        public int sort { get; set; }
        public string linkurl { get; set; }

        public virtual ColumnType ColumnType { get; set; }
        public virtual ICollection<Down> Down { get; set; }
        public virtual ICollection<Job> Job { get; set; }
        public virtual ICollection<OnlyText> OnlyText { get; set; }
        public virtual ICollection<Texts> Texts { get; set; }
        public virtual ICollection<Video> Video { get; set; }
        public virtual ICollection<Message> Message { get; set; }
        public virtual ICollection<Picture> Picture { get; set; }
    }
}
