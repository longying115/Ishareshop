using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Picture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public int Sort { get; set; }
        public string SmallPicture { get; set; }
        public string PictureName { get; set; }
        public string PictureRemark { get; set; }
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
