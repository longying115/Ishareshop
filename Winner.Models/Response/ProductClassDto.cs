using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.Models.Response
{
    public class ProductClassDto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ClassLevel { get; set; }
        public int Sort { get; set; }
        public string ClassName { get; set; }
        public string ClassRemark { get; set; }
        public string KeyTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string SmallPicture { get; set; }
        public bool IsShow { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
