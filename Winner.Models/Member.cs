using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string passstring { get; set; }
        public string nickname { get; set; }
        public string qqcode { get; set; }
        public string weixincode { get; set; }
        public string weibocode { get; set; }
        public string userpicture { get; set; }
        public bool ispass { get; set; }
        public Decimal account { get; set; }
        public Decimal allbrokerage { get; set; }
        public int xinpoint { get; set; }
        public int memberlevel { get; set; }
        private DateTime _addtime = DateTime.Now;
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private DateTime _lastlogintime = DateTime.Now;
        public DateTime lastlogintime
        {
            get { return _lastlogintime; }
            set { _lastlogintime = value; }
        }
        public string lastloginip { get; set; }
        private int _sex = 0;
        public int sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        public string birthday { get; set; }
        public string phone { get; set; }
        public string telephone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string post { get; set; }
        public string area { get; set; }
        public string address { get; set; }
        public string industry { get; set; }
        public int parentid { get; set; }
        public bool status { get; set; }
        public string question1 { get; set; }
        public string question2 { get; set; }
        public string question3 { get; set; }
        public string question4 { get; set; }
        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public string answer4 { get; set; }

        public virtual ICollection<GetPointLog> GetPointLog { get; set; }
        public virtual ICollection<GetMoneyLog> GetMoneyLog { get; set; }
        public virtual ICollection<CashValueLog> CashValueLog { get; set; }
        public virtual ICollection<MemberLog> MemberLog { get; set; }

        public virtual ICollection<Favorites> Favorites { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<ShippingAddress> ShippingAddress { get; set; }
        public virtual ICollection<ShopCart> ShopCart { get; set; }

        public virtual ICollection<ProductDiscuss> ProductDiscuss { get; set; }

        public virtual ICollection<ReturnGoods> ReturnGoods { get; set; }
    }
}
