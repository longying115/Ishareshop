using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Partner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public int Sort { get; set; }
        public string LinkName { get; set; }
        public string LinkPicture { get; set; }
        public string LinkUrl { get; set; }
        public bool IsShow { get; set; }
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
    }
}
