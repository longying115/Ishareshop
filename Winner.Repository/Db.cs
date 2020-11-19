using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Winner.Models;

namespace Winner.Repository
{
    class Db:DbContext
    {
        public Db()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Ishareshop_db,User ID=sa;Password=tysy", b=>b.UseRowNumberForPaging());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<AdminLoginLog> AdminLoginLog { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<CashFlowLog> CashFlowLog { get; set; }
        public virtual DbSet<ColumnType> ColumnType { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Down> Down { get; set; }
        public virtual DbSet<Express> Express { get; set; }
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<GetMoneyLog> GetMoneyLog { get; set; }
        public virtual DbSet<GetPointLog> GetPointLog { get; set; }
        public virtual DbSet<Gifts> Gifts { get; set; }
        public virtual DbSet<GiftClass> GiftClass { get; set; }
        public virtual DbSet<GiftPicture> GiftPicture { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Link> Link { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<MemberLog> MemberLog { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsType> NewsType { get; set; }
        public virtual DbSet<OnlyText> OnlyText { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Partner> Partner { get; set; }
        public virtual DbSet<PhoneCode> PhoneCode { get; set; }
        public virtual DbSet<Picture> Picture { get; set; }
        public virtual DbSet<ProductClass> ProductClass { get; set; }
        public virtual DbSet<ProductColor> ProductColor { get; set; }
        public virtual DbSet<ProductDiscuss> ProductDiscuss { get; set; }
        public virtual DbSet<ProductPicture> ProductPicture { get; set; }
        public virtual DbSet<ProductPrice> ProductPrice { get; set; }
        public virtual DbSet<ProductQuestion> ProductQuestion { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<SafeQuestion> SafeQuestion { get; set; }
        public virtual DbSet<ShippingAddress> ShippingAddress { get; set; }
        public virtual DbSet<ShopCart> ShopCart { get; set; }
        public virtual DbSet<Tencent> Tencent { get; set; }
        public virtual DbSet<Texts> Texts { get; set; }
        public virtual DbSet<Video> Video { get; set; }
        public virtual DbSet<WebColumn> WebColumn { get; set; }
        public virtual DbSet<WebSite> WebSite { get; set; }
        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<ReturnGoods> ReturnGoods { get; set; }
        public virtual DbSet<ReturnPicture> ReturnPicture { get; set; }

    }
}
