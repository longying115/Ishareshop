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
        public int Id { get; set; }
        public string ColumnName { get; set; }
        public string Banner { get; set; }
        public string BackgroundImg { get; set; }
        public string KeyTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public int ColumnLevel { get; set; }
        /// <summary>
        /// 栏目类型ID
        /// </summary>
        public int ColumnTypeId { get; set; }
        public int ParentNode { get; set; }
        public int Sort { get; set; }
        public string Linkurl { get; set; }

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
