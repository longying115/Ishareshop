using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace Winner.Models
{
    public class AccountContext:DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> option) : base(option)
        {
            
        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<AdminLoginLog> AdminLoginLog { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<CashValueLog> CashValueLog { get; set; }
        public DbSet<ColumnType> ColumnType { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Down> Down { get; set; }
        public DbSet<Express> Express { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<GetMoneyLog> GetMoneyLog { get; set; }
        public DbSet<GetPointLog> GetPointLog { get; set; }
        public DbSet<Gifts> Gifts { get; set; }
        public DbSet<GiftClass> GiftClass { get; set; }
        public DbSet<GiftPicture> GiftPicture { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Link> Link { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<MemberLog> MemberLog { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsType> NewsType { get; set; }
        public DbSet<OnlyText> OnlyText { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<PhoneCode> PhoneCode { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<ProductClass> ProductClass { get; set; }
        public DbSet<ProductColor> ProductColor { get; set; }
        public DbSet<ProductDiscuss> ProductDiscuss { get; set; }
        public DbSet<ProductPicture> ProductPicture { get; set; }
        public DbSet<ProductPrice> ProductPrice { get; set; }
        public DbSet<ProductQuestion> ProductQuestion { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<SafeQuestion> SafeQuestion { get; set; }
        public DbSet<ShippingAddress> ShippingAddress { get; set; }
        public DbSet<ShopCart> ShopCart { get; set; }
        public DbSet<Tencent> Tencent { get; set; }
        public DbSet<Texts> Texts { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<WebColumn> WebColumn { get; set; }
        public DbSet<WebSite> WebSite { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<ReturnGoods> ReturnGoods { get; set; }
        public DbSet<ReturnPicture> ReturnPicture { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().ToTable("T_Admin");
            modelBuilder.Entity<AdminLoginLog>(entity =>
            {
                entity.ToTable("T_AdminLoginLog");

                entity.HasOne(d => d.Admin)
                .WithMany(p => p.AdminLoginLog)
                .HasForeignKey(d => d.adminid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdminLoginLog_To_Admin");
            });
            modelBuilder.Entity<Banner>().ToTable("T_Banner");
            modelBuilder.Entity<CashValueLog>(entity =>
            {
                entity.ToTable("T_CashValueLog");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.CashValueLog)
                .HasForeignKey(d => d.uid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_CashValueLog_To_Member");
            });
            modelBuilder.Entity<ColumnType>().ToTable("T_ColumnType");
            modelBuilder.Entity<Contact>().ToTable("T_Contact");
            modelBuilder.Entity<Down>(entity =>
            {
                entity.ToTable("T_Down");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Down)
                .HasForeignKey(d => d.tid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Down_To_WebColumn");
            });
            modelBuilder.Entity<Express>().ToTable("T_Express");
            modelBuilder.Entity<Favorites>(entity =>
            {
                entity.ToTable("T_Favorites");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.Favorites)
                .HasForeignKey(d => d.uid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Favorites_To_Member");

                entity.HasOne(d => d.Products)
                .WithMany(p => p.Favorites)
                .HasForeignKey(d => d.productid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Favorites_To_Products");
            });
            modelBuilder.Entity<GetMoneyLog>(entity =>
            {
                entity.ToTable("T_GetMoneyLog");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.GetMoneyLog)
                .HasForeignKey(d => d.uid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_GetMoneyLog_To_Member");
            });
            modelBuilder.Entity<GetPointLog>(entity =>
            {
                entity.ToTable("T_GetPointLog");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.GetPointLog)
                .HasForeignKey(d => d.uid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_GetPointLog_To_Member");
            });
            modelBuilder.Entity<Gifts>(entity =>
            {
                entity.ToTable("T_Gifts");

                entity.HasOne(d => d.GiftClass)
                .WithMany(p => p.Gifts)
                .HasForeignKey(d => d.classid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Gifts_To_GiftClass");
            });
            modelBuilder.Entity<GiftClass>().ToTable("T_GiftClass");
            modelBuilder.Entity<GiftPicture>(entity =>
            {
                entity.ToTable("T_GiftPicture");

                entity.HasOne(d => d.Gifts)
                .WithMany(p => p.GiftPicture)
                .HasForeignKey(d => d.giftid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_GiftPicture_To_Gifts");
            });
            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("T_Job");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Job)
                .HasForeignKey(d => d.tid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Job_To_WebColumn");
            });
            modelBuilder.Entity<Link>().ToTable("T_Link");
            modelBuilder.Entity<Member>().ToTable("T_Member");
            modelBuilder.Entity<MemberLog>(entity =>
            {
                entity.ToTable("T_MemberLog");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.MemberLog)
                .HasForeignKey(d => d.memberid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MemberLog_To_Member");
            });
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("T_Message");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Message)
                .HasForeignKey(d => d.tid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Message_To_WebColumn");
            });
            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("T_News");

                entity.HasOne(d => d.NewsType)
                .WithMany(p => p.News)
                .HasForeignKey(d => d.tid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_News_To_NewsType");
            });

            modelBuilder.Entity<NewsType>().ToTable("T_NewsType");
            modelBuilder.Entity<OnlyText>(entity =>
            {
                entity.ToTable("T_OnlyText");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.OnlyText)
                .HasForeignKey(d => d.tid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_OnlyText_To_WebColumn");
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("T_Order");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.Order)
                .HasForeignKey(d => d.buyuid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Order_To_Member");

                entity.HasOne(d => d.Express)
                .WithMany(p => p.Order)
                .HasForeignKey(d => d.expresstype)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Order_To_Express");
            });
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("T_OrderItem");

                entity.HasOne(d => d.Order)
                .WithMany(p => p.OrderItem)
                .HasForeignKey(d => d.ordernum)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_OrderItem_To_Order");
            });
            modelBuilder.Entity<Partner>().ToTable("T_Partner");
            modelBuilder.Entity<PhoneCode>().ToTable("T_PhoneCode");
            modelBuilder.Entity<Picture>(entity =>
            {
                entity.ToTable("T_Picture");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Picture)
                .HasForeignKey(d => d.tid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Picture_To_WebColumn");
            });
            modelBuilder.Entity<ProductClass>().ToTable("T_ProductClass");
            modelBuilder.Entity<ProductColor>(entity =>
            {
                entity.ToTable("T_ProductColor");

                entity.HasOne(d => d.Products)
                .WithMany(p => p.ProductColor)
                .HasForeignKey(d => d.productid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductColor_To_Products");
            });
            modelBuilder.Entity<ProductDiscuss>(entity =>
            {
                entity.ToTable("T_ProductDiscuss");

                entity.HasOne(d => d.Products)
                .WithMany(p => p.ProductDiscuss)
                .HasForeignKey(d => d.productid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductDiscuss_To_Products");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.ProductDiscuss)
                .HasForeignKey(d => d.memberid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductDiscuss_To_Member");
            });
            modelBuilder.Entity<ProductPicture>(entity =>
            {
                entity.ToTable("T_ProductPicture");

                entity.HasOne(d => d.ProductColor)
                .WithMany(p => p.ProductPicture)
                .HasForeignKey(d => d.colorid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductPicture_To_ProductColor");

                entity.HasOne(d => d.Products)
                .WithMany(p => p.ProductPicture)
                .HasForeignKey(d => d.productid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductPicture_To_Products");
            });
            modelBuilder.Entity<ProductPrice>().ToTable("T_ProductPrice");
            modelBuilder.Entity<ProductQuestion>(entity =>
            {
                entity.ToTable("T_ProductQuestion");

                entity.HasOne(d => d.Products)
                .WithMany(p => p.ProductQuestion)
                .HasForeignKey(d => d.productid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductQuestion_To_Products");
            });
            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("T_Products");

                entity.HasOne(d => d.ProductClass)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.fistclassid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Products_To_ProductClass");
            });
            modelBuilder.Entity<Province>().ToTable("T_Province");
            modelBuilder.Entity<SafeQuestion>().ToTable("T_SafeQuestion");
            modelBuilder.Entity<ShippingAddress>(entity =>
            {
                entity.ToTable("T_ShippingAddress");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.ShippingAddress)
                .HasForeignKey(d => d.uid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ShippingAddress_To_Member");
            });
            modelBuilder.Entity<ShopCart>(entity =>
            {
                entity.ToTable("T_ShopCart");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.ShopCart)
                .HasForeignKey(d => d.buyuid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ShopCart_To_Member");
            });
            modelBuilder.Entity<Tencent>().ToTable("T_Tencent");
            modelBuilder.Entity<Texts>(entity =>
            {
                entity.ToTable("T_Texts");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Texts)
                .HasForeignKey(d => d.tid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Texts_To_WebColumn");
            });
            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("T_Video");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Video)
                .HasForeignKey(d => d.tid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Video_To_WebColumn");
            });
            modelBuilder.Entity<WebColumn>(entity =>
            {
                entity.ToTable("T_WebColumn");

                entity.HasOne(d => d.ColumnType)
                .WithMany(p => p.WebColumn)
                .HasForeignKey(d => d.columntype)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_WebColumn_To_ColumnType");
            });
            modelBuilder.Entity<WebSite>().ToTable("T_WebSite");
            modelBuilder.Entity<User>().ToTable("T_User");

            modelBuilder.Entity<ReturnPicture>(entity =>
            {
                entity.ToTable("T_ReturnPicture");

                entity.HasOne(d => d.ReturnGoods)
                .WithMany(p => p.ReturnPicture)
                .HasForeignKey(d => d.returnnum)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ReturnPicture_To_ReturnGoods");
            });

            modelBuilder.Entity<ReturnGoods>(entity =>
            {
                entity.ToTable("T_ReturnGoods");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.ReturnGoods)
                .HasForeignKey(d => d.memberid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ReturnGoods_To_Member");

                entity.HasOne(d => d.Express)
                .WithMany(p => p.ReturnGoods)
                .HasForeignKey(d => d.expresstype)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ReturnGoods_To_Express");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
