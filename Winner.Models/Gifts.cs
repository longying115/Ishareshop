using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Gifts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ClassId { get; set; }
        private int _sort = 0;
        public int Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }
        public string Title { get; set; }
        public string SmallTitle { get; set; }
        public string Remark { get; set; }
        public Decimal Price { get; set; }
        public string KeyTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string SmallPicture { get; set; }
        public string Bigpicture { get; set; }
        private int _sales = 0;
        public int Sales
        {
            get { return _sales; }
            set { _sales = value; }
        }
        private int _score = 0;
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        public string TextContent { get; set; }
        public string Parameter { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        private int _hits = 0;
        public int Hits
        {
            get { return _hits; }
            set { _hits = value; }
        }
        public DateTime _hitTime = DateTime.Now;
        public DateTime GMTHit
        {
            get { return _hitTime; }
            set { _hitTime = value; }
        }
        public bool IsShow { get; set; }
        public bool IsHome { get; set; }

        public virtual GiftClass GiftClass { get; set; }
        public virtual ICollection<GiftPicture> GiftPicture { get; set; }
    }
}
