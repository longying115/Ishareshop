using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Down
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public string Title { get; set; }
        public string KeyTitle { get; set; }
        public string Keywords { get; set; }
        public string Desciption { get; set; }
        public string TextContent { get; set; }
        public string FileName { get; set; }
        public string Source { get; set; }
        public int Hits { get; set; }
        public bool IsShow { get; set; }
        public bool IsHead { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        public int CreateAdminId { get; set; }
        private DateTime _modifiedTime = DateTime.Now;
        public DateTime GMTModified
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }
        public int ModifiedAdminId { get; set; }
        public string ModifiedIp { get; set; }
       

        public virtual WebColumn WebColumn { get; set; }
    }
}
