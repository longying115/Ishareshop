using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace Winner.Models
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> option) : base(option)
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<AdminLoginLog> AdminLoginLog { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<CashFlowLog> CashFlowLog { get; set; }
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
            //modelBuilder.Entity<Admin>().ToTable("T_Admin");
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("T_Admin");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.UserName)
                .HasColumnName("user_name")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.PassString)
               .HasColumnName("pass_string")
               .HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
               .HasColumnName("password")
               .HasColumnType("varchar(500)");

                entity.Property(e => e.NickName)
               .HasColumnName("nick_name")
               .HasColumnType("varchar(50)");

                entity.Property(e => e.Telephone)
               .HasColumnName("telephone")
               .HasColumnType("varchar(50)");

                entity.Property(e => e.QQNumber)
               .HasColumnName("qq_number")
               .HasColumnType("varchar(50)");

                entity.Property(e => e.WeChat)
               .HasColumnName("wechat")
               .HasColumnType("varchar(50)");

                entity.Property(e => e.Power)
               .HasColumnName("power")
               .HasColumnType("int");

                entity.Property(e => e.Flag)
               .HasColumnName("flag")
               .HasColumnType("varchar(50)");

                entity.Property(e => e.IsAdd)
               .HasColumnName("is_add")
               .HasColumnType("bit");

                entity.Property(e => e.IsDeleted)
               .HasColumnName("is_deleted")
               .HasColumnType("bit");

                entity.Property(e => e.IsEdit)
               .HasColumnName("is_edit")
               .HasColumnType("bit");

                entity.Property(e => e.Status)
               .HasColumnName("is_status")
               .HasColumnType("bit");

                entity.Property(e => e.GMTCreate)
               .HasColumnName("gmt_create")
               .HasColumnType("datetime");

                entity.Property(e => e.GMTModified)
               .HasColumnName("gmt_modified")
               .HasColumnType("datetime");

                entity.Property(e => e.GMTLastLogin)
               .HasColumnName("gmt_last_login")
               .HasColumnType("datetime");

                entity.Property(e => e.LastLoginIp)
                .HasColumnName("last_login_ip")
                .HasColumnType("varchar(50)");

            });
            modelBuilder.Entity<AdminLoginLog>(entity =>
            {
                entity.ToTable("T_AdminLoginLog");

                entity.HasOne(d => d.Admin)
                .WithMany(p => p.AdminLoginLog)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_AdminLoginLog_To_Admin");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.AdminId)
                .HasColumnName("admin_id")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Ips)
                .HasColumnName("ips")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.Remark)
                .HasColumnName("remark")
                .HasColumnType("varchar(500)");

            });
            //modelBuilder.Entity<Banner>().ToTable("T_Banner");
            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("T_Banner");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.BannerName)
                .HasColumnName("banner_name")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.Picture)
                .HasColumnName("picture")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.BackgroundImg)
                .HasColumnName("background_Img")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.LinkUrl)
                .HasColumnName("link_url")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.ColumnArea)
                .HasColumnName("column_area")
                .HasColumnType("int");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");

                entity.Property(e => e.IsMobile)
                .HasColumnName("is_mobile")
                .HasColumnType("bit");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.CreateAdminId)
                .HasColumnName("create_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.GMTModified)
                .HasColumnName("gmt_modified")
                .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAdminId)
                .HasColumnName("modified_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.ModifiedIp)
                .HasColumnName("modified_ip")
                .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<CashFlowLog>(entity =>
            {
                entity.ToTable("T_CashFlowLog");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.CashFlowLog)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_CashValueLog_To_Member");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.MemberId)
                .HasColumnName("member_id")
                .HasColumnType("int");

                entity.Property(e => e.Money)
                .HasColumnName("money")
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Remark)
                .HasColumnName("remark")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.CreateIp)
                .HasColumnName("create_ip")
                .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<ColumnType>(entity=>
            {
                entity.ToTable("T_ColumnType");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.TypeName)
                .HasColumnName("type_name")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.TypeLink)
                .HasColumnName("type_link")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.OpenWay)
                .HasColumnName("open_way")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.TypeState)
                .HasColumnName("type_state")
                .HasColumnType("bit");
            });
            modelBuilder.Entity<Contact>(entity=>
            {
                entity.ToTable("T_Contact");

                entity.Property(e=>e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e=>e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.ContactName)
                .HasColumnName("contact_name")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Telephone)
                .HasColumnName("telephone")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.QQNumber)
                .HasColumnName("qq_number")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.IsShow)
                .HasColumnName("isshow")
                .HasColumnType("bit");
            });
            modelBuilder.Entity<Down>(entity =>
            {
                entity.ToTable("T_Down");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Down)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Down_To_WebColumn");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.ColumnId)
                .HasColumnName("column_id")
                .HasColumnType("int");

                entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.KeyTitle)
                .HasColumnName("key_title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Keywords)
                .HasColumnName("keywords")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Desciption)
                .HasColumnName("desciption")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.TextContent)
                .HasColumnName("text_content")
                .HasColumnType("text");

                entity.Property(e => e.FileName)
                .HasColumnName("file_name")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Source)
                .HasColumnName("source")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Hits)
                .HasColumnName("hits")
                .HasColumnType("int");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");

                entity.Property(e => e.IsHead)
                .HasColumnName("is_head")
                .HasColumnType("bit");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.CreateAdminId)
                .HasColumnName("create_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.GMTModified)
                .HasColumnName("gmt_modified")
                .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAdminId)
                .HasColumnName("modified_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.ModifiedIp)
                .HasColumnName("modified_ip")
                .HasColumnType("varchar(50)");

            });
            modelBuilder.Entity<Express>(entity=>
            {
                entity.ToTable("T_Express");

                entity.Property(e => e.ExpressCode)
                .HasColumnName("express_code")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.ExpressName)
                .HasColumnName("express_name")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.ExpressTelephone)
                .HasColumnName("express_telephone")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");
            });
            modelBuilder.Entity<Favorites>(entity =>
            {
                entity.ToTable("T_Favorites");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.Favorites)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Favorites_To_Member");

                entity.HasOne(d => d.Products)
                .WithMany(p => p.Favorites)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Favorites_To_Products");

                entity.Property(e=>e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e=>e.MemberId)
                .HasColumnName("member_id")
                .HasColumnType("int");

                entity.Property(e => e.ProductId)
                .HasColumnName("product_id")
                .HasColumnType("int");

                entity.Property(e => e.ProductTitle)
                 .HasColumnName("product_title")
                 .HasColumnType("varchar(500)");

                entity.Property(e => e.ProductSmallTitle)
                .HasColumnName("product_small_title")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.ProductPrice)
                .HasColumnName("product_price")
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");
            });
            modelBuilder.Entity<GetMoneyLog>(entity =>
            {
                entity.ToTable("T_GetMoneyLog");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.GetMoneyLog)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_GetMoneyLog_To_Member");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.MemberId)
                .HasColumnName("member_id")
                .HasColumnType("int");

                entity.Property(e => e.Money)
                .HasColumnName("money")
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Remark)
                .HasColumnName("remark")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.OrderCode)
                .HasColumnName("order_code")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Mid)
                .HasColumnName("mid")
                .HasColumnType("int");

                entity.Property(e => e.AllMoney)
                .HasColumnName("all_money")
                .HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<GetPointLog>(entity =>
            {
                entity.ToTable("T_GetPointLog");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.GetPointLog)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_GetPointLog_To_Member");

                entity.Property(e=>e.Id)
                .HasColumnName("id")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.MemberId)
                .HasColumnName("member_id")
                .HasColumnType("int");

                entity.Property(e => e.Score)
                .HasColumnName("score")
                .HasColumnType("int");

                entity.Property(e => e.Remark)
                .HasColumnName("remark")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.OrderCode)
                .HasColumnName("order_code")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.AllMoney)
                .HasColumnName("all_money")
                .HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<GiftClass>(entity=>
            {
                entity.ToTable("T_GiftClass");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.ParentId)
                .HasColumnName("parent_id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.ClassName)
                .HasColumnName("class_name")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Remark)
                .HasColumnName("remark")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.KeyTitle)
                .HasColumnName("key_title")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Keywords)
                .HasColumnName("keywords")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Picture)
                .HasColumnName("picture")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");
            });
            modelBuilder.Entity<GiftPicture>(entity =>
            {
                entity.ToTable("T_GiftPicture");

                entity.HasOne(d => d.Gifts)
                .WithMany(p => p.GiftPicture)
                .HasForeignKey(d => d.GiftClassId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_GiftPicture_To_Gifts");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.GiftClassId)
                .HasColumnName("gift_class_id")
                .HasColumnType("int");

                entity.Property(e => e.Picture)
                .HasColumnName("picture")
                .HasColumnType("varchar(500)");
            });
            modelBuilder.Entity<Gifts>(entity =>
            {
                entity.ToTable("T_Gifts");

                entity.HasOne(d => d.GiftClass)
                .WithMany(p => p.Gifts)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Gifts_To_GiftClass");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.ClassId)
                .HasColumnName("class_id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.SmallTitle)
                .HasColumnName("small_title")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Remark)
                .HasColumnName("remark")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.KeyTitle)
                .HasColumnName("key_title")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Keywords)
                .HasColumnName("keywords")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.SmallPicture)
                .HasColumnName("small_picture")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Bigpicture)
                .HasColumnName("big_picture")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Sales)
                .HasColumnName("sales")
                .HasColumnType("int");

                entity.Property(e => e.Score)
                .HasColumnName("score")
                .HasColumnType("int");

                entity.Property(e => e.TextContent)
                .HasColumnName("text_content")
                .HasColumnType("text");

                entity.Property(e => e.Parameter)
                .HasColumnName("parameter")
                .HasColumnType("text");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.Hits)
                .HasColumnName("hits")
                .HasColumnType("int");

                entity.Property(e => e.GMTHit)
                .HasColumnName("gmt_hit")
                .HasColumnType("datetime");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");

                entity.Property(e => e.IsHome)
                .HasColumnName("is_home")
                .HasColumnType("bit");
            });
            
            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("T_Job");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Job)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Job_To_WebColumn");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.ColumnId)
                .HasColumnName("column_id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.KeyTitle)
                .HasColumnName("key_title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Keywords)
                .HasColumnName("keywords")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.BeginDate)
                .HasColumnName("begin_date")
                .HasColumnType("datetime");

                entity.Property(e => e.EndDate)
                .HasColumnName("end_date")
                .HasColumnType("datetime");

                entity.Property(e => e.Count)
                .HasColumnName("count")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Money)
                .HasColumnName("money")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Condition)
                .HasColumnName("condition")
                .HasColumnType("text");

                entity.Property(e => e.Duty)
                .HasColumnName("duty")
                .HasColumnType("text");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");

                entity.Property(e => e.IsHead)
                .HasColumnName("is_head")
                .HasColumnType("bit");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.CreateAdminId)
                .HasColumnName("create_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.GMTModified)
                .HasColumnName("gmt_modified")
                .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAdminId)
                .HasColumnName("modified_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.ModifiedIp)
                .HasColumnName("modified_ip")
                .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<Link>(entity=>
            {
                entity.ToTable("T_Link");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.LinkName)
                .HasColumnName("link_name")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.LinkPicture)
                .HasColumnName("link_picture")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.LinkUrl)
                .HasColumnName("link_url")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.CreateAdminId)
                .HasColumnName("create_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.GMTModified)
                .HasColumnName("gmt_modified")
                .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAdminId)
                .HasColumnName("modified_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.ModifiedAdminId)
                .HasColumnName("modified_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.ModifiedIp)
                .HasColumnName("modified_ip")
                .HasColumnType("varchar(50)");
            });

            //modelBuilder.Entity<Member>().ToTable("T_Member");
            modelBuilder.Entity<Member>(entity=>
            {
                entity.ToTable("T_Member");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.UserName)
                .HasColumnName("user_name")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.PassString)
                .HasColumnName("pass_string")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.NickName)
                .HasColumnName("nick_name")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.QQCode)
                .HasColumnName("qq_code")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.WeChatCode)
                .HasColumnName("wechat_code")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.WeiboCode)
                .HasColumnName("weibo_code")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.UserPicture)
                .HasColumnName("user_picture")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.IsPass)
                .HasColumnName("is_pass")
                .HasColumnType("bit");

                entity.Property(e => e.Account)
                .HasColumnName("account")
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.AllBrokerage)
                .HasColumnName("all_brokerage")
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Score)
                .HasColumnName("score")
                .HasColumnType("int");

                entity.Property(e => e.MemberLevel)
                .HasColumnName("member_level")
                .HasColumnType("int");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.GMTModified)
                .HasColumnName("gmt_modified")
                .HasColumnType("datetime");

                entity.Property(e => e.GMTLastLogin)
                .HasColumnName("gmt_last_login")
                .HasColumnType("datetime");

                entity.Property(e => e.LastLoginIp)
                .HasColumnName("last_login_ip")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.Sex)
                .HasColumnName("sex")
                .HasColumnType("int");

                entity.Property(e => e.Birthday)
                .HasColumnName("varchar(50)")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Phone)
                .HasColumnName("phone")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Telephone)
                .HasColumnName("telephone")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Fax)
                .HasColumnName("fax")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Post)
                .HasColumnName("post")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Area)
                .HasColumnName("area")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.Address)
                .HasColumnName("address")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Industry)
                .HasColumnName("industry")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.ParentId)
                .HasColumnName("parentid")
                .HasColumnType("int");

                entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasColumnType("bit");

                entity.Property(e => e.Question1)
                .HasColumnName("question1")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Answer1)
                .HasColumnName("answer1")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Question2)
                .HasColumnName("question2")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Answer2)
                .HasColumnName("answer2")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Question3)
                .HasColumnName("question3")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Answer3)
                .HasColumnName("answer3")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Question4)
                .HasColumnName("question4")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Answer4)
                .HasColumnName("answer4")
                .HasColumnType("varchar(200)");
            });
            modelBuilder.Entity<MemberLog>(entity =>
            {
                entity.ToTable("T_MemberLog");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.MemberLog)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MemberLog_To_Member");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.MemberId)
                .HasColumnName("member_id")
                .HasColumnType("int");

                entity.Property(e => e.UseType)
                .HasColumnName("use_type")
                .HasColumnType("int");

                entity.Property(e => e.Ips)
                .HasColumnName("ips")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.Remark)
                .HasColumnName("remark")
                .HasColumnType("varchar(500)");
            });
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("T_Message");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Message)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Message_To_WebColumn");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.ColumnId)
                .HasColumnName("column_id")
                .HasColumnType("int");

                entity.Property(e => e.MsgName)
                .HasColumnName("msg_name")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.MsgAddress)
                .HasColumnName("msg_address")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.MsgPhone)
                .HasColumnName("msg_phone")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.MsgEmail)
                .HasColumnName("msg_email")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.MsgTitle)
                .HasColumnName("msg_title")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.MsgContent)
                .HasColumnName("msg_content")
                .HasColumnType("text");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.BackContent)
                .HasColumnName("back_content")
                .HasColumnType("text");

                entity.Property(e => e.GMTBack)
                .HasColumnName("gmt_back")
                .HasColumnType("datetime");

                entity.Property(e => e.IsBack)
                .HasColumnName("is_back")
                .HasColumnType("bit");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");
            });
            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("T_News");

                entity.HasOne(d => d.NewsType)
                .WithMany(p => p.News)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_News_To_NewsType");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.ClassId)
                .HasColumnName("class_id")
                .HasColumnType("int");

                entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.KeyTitle)
                .HasColumnName("key_title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Keywords)
                .HasColumnName("keywords")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Author)
                .HasColumnName("author")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.Source)
                .HasColumnName("source")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.Picture)
                .HasColumnName("picture")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.PictureTag)
                .HasColumnName("picture_tag")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.GMTModified)
                .HasColumnName("gmt_modified")
                .HasColumnType("datetime");

                entity.Property(e => e.Hits)
                .HasColumnName("hits")
                .HasColumnType("int");

                entity.Property(e => e.GMTLastHit)
                .HasColumnName("gmt_last_hit")
                .HasColumnType("datetime");

                entity.Property(e => e.Praise)
                .HasColumnName("praise")
                .HasColumnType("int");

                entity.Property(e => e.TextContent)
                .HasColumnName("text_content")
                .HasColumnType("text");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");

                entity.Property(e => e.IsHome)
                .HasColumnName("is_home")
                .HasColumnType("bit");

                entity.Property(e => e.IsHead)
                .HasColumnName("is_head")
                .HasColumnType("bit");
            });

            modelBuilder.Entity<NewsType>(entity=>
            {
                entity.ToTable("T_NewsType");

                entity.Property(e=>e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.TypeName)
                .HasColumnName("type_name")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Picture)
                .HasColumnName("picture")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.PictureTag)
                .HasColumnName("picture_tag")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.KeyTitle)
                .HasColumnName("key_title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Keywords)
                .HasColumnName("keywords")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");

                entity.Property(e => e.IsHead)
                .HasColumnName("is_head")
                .HasColumnType("bit");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.GMTModified)
                .HasColumnName("gmt_modified")
                .HasColumnType("datetime");

                entity.Property(e => e.GMTLastHit)
                .HasColumnName("gmt_last_hit")
                .HasColumnType("datetime");
            });
            modelBuilder.Entity<OnlyText>(entity =>
            {
                entity.ToTable("T_OnlyText");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.OnlyText)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_OnlyText_To_WebColumn");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.ColumnId)
                .HasColumnName("column_id")
                .HasColumnType("int");

                entity.Property(e => e.TextContent)
                .HasColumnName("text_content")
                .HasColumnType("text");

                entity.Property(e => e.Hits)
                .HasColumnName("hits")
                .HasColumnType("int");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.CreateAdminId)
                .HasColumnName("create_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.GMTModified)
                .HasColumnName("gmt_modified")
                .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAdminId)
                .HasColumnName("modified_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.ModifiedIp)
                .HasColumnName("modified_ip")
                .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("T_Order");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.Order)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Order_To_Member");

                entity.HasOne(d => d.Express)
                .WithMany(p => p.Order)
                .HasForeignKey(d => d.ExpressType)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Order_To_Express");
            });
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("T_OrderItem");

                entity.HasOne(d => d.Order)
                .WithMany(p => p.OrderItem)
                .HasForeignKey(d => d.OrderCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_OrderItem_To_Order");
            });
            modelBuilder.Entity<Partner>().ToTable("T_Partner");
            modelBuilder.Entity<PhoneCode>(entity=>
            {
                entity.ToTable("T_PhoneCode");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Phone)
                .HasColumnName("phone")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.MachineCode)
                .HasColumnName("machine_code")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Code)
                .HasColumnName("code")
                .HasColumnType("varchar(10)");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");
            });
            modelBuilder.Entity<Picture>(entity =>
            {
                entity.ToTable("T_Picture");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Picture)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Picture_To_WebColumn");

                entity.Property(e=>e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.ColumnId)
               .HasColumnName("column_id")
               .HasColumnType("int");

                entity.Property(e => e.Sort)
               .HasColumnName("sort")
               .HasColumnType("int");

                entity.Property(e => e.SmallPicture)
               .HasColumnName("small_picture")
               .HasColumnType("varchar(500)");

                entity.Property(e => e.PictureName)
               .HasColumnName("picture_name")
               .HasColumnType("varchar(100)");

                entity.Property(e => e.PictureRemark)
               .HasColumnName("picture_remark")
               .HasColumnType("varchar(1000)");

                entity.Property(e => e.IsShow)
               .HasColumnName("is_show")
               .HasColumnType("bit");

                entity.Property(e => e.IsHead)
               .HasColumnName("is_head")
               .HasColumnType("bit");

                entity.Property(e => e.GMTCreate)
               .HasColumnName("gmt_create")
               .HasColumnType("datetime");

                entity.Property(e => e.CreateAdminId)
               .HasColumnName("create_admin_id")
               .HasColumnType("int");

                entity.Property(e => e.GMTModified)
               .HasColumnName("gmt_modified")
               .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAdminId)
               .HasColumnName("modified_admin_id")
               .HasColumnType("int");

                entity.Property(e => e.ModifiedIp)
               .HasColumnName("modified_ip")
               .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<ProductClass>(entity=>
            {
                entity.ToTable("T_ProductClass");

                entity.Property(e => e.Id)
               .HasColumnName("id")
               .HasColumnType("int");

                entity.Property(e => e.ParentId)
               .HasColumnName("parent_id")
               .HasColumnType("int");

                entity.Property(e => e.ClassLevel)
               .HasColumnName("class_level")
               .HasColumnType("int");

                entity.Property(e => e.Sort)
               .HasColumnName("sort")
               .HasColumnType("int");

                entity.Property(e => e.ClassName)
               .HasColumnName("class_name")
               .HasColumnType("varchar(200)");

                entity.Property(e => e.ClassRemark)
               .HasColumnName("class_remark")
               .HasColumnType("varchar(500)");

                entity.Property(e => e.KeyTitle)
               .HasColumnName("key_title")
               .HasColumnType("varchar(500)");

                entity.Property(e => e.Keywords)
               .HasColumnName("keywords")
               .HasColumnType("varchar(500)");

                entity.Property(e => e.Description)
               .HasColumnName("description")
               .HasColumnType("varchar(500)");

                entity.Property(e => e.Picture)
               .HasColumnName("picture")
               .HasColumnType("varchar(200)");

                entity.Property(e => e.PictureTag)
               .HasColumnName("picture_tag")
               .HasColumnType("varchar(50)");

                entity.Property(e => e.GMTCreate)
               .HasColumnName("gmt_create")
               .HasColumnType("datetime");

                entity.Property(e => e.GMTModified)
               .HasColumnName("gmt_modified")
               .HasColumnType("datetime");

                entity.Property(e => e.GMTLastHit)
               .HasColumnName("gmt_last_hit")
               .HasColumnType("datetime");

                entity.Property(e => e.IsShow)
               .HasColumnName("is_show")
               .HasColumnType("bit");

                entity.Property(e => e.IsHead)
               .HasColumnName("is_head")
               .HasColumnType("bit");
            });
            modelBuilder.Entity<ProductColor>(entity =>
            {
                entity.ToTable("T_ProductColor");

                entity.HasOne(d => d.Products)
                .WithMany(p => p.ProductColor)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductColor_To_Products");

                entity.Property(e => e.Id)
               .HasColumnName("id")
               .HasColumnType("int");

                entity.Property(e => e.Sort)
               .HasColumnName("sort")
               .HasColumnType("int");

                entity.Property(e => e.ProductId)
               .HasColumnName("product_id")
               .HasColumnType("int");

                entity.Property(e => e.Name)
               .HasColumnName("name")
               .HasColumnType("varchar(50)");

                entity.Property(e => e.Picture)
               .HasColumnName("picture")
               .HasColumnType("varchar(500)");

                entity.Property(e => e.Counts)
               .HasColumnName("counts")
               .HasColumnType("int");

                entity.Property(e => e.Price)
               .HasColumnName("price")
               .HasColumnType("decimal(18,2)");

                entity.Property(e => e.GMTCreate)
               .HasColumnName("gmt_create")
               .HasColumnType("datetime");

                entity.Property(e => e.CreateAdminId)
               .HasColumnName("create_admin_id")
               .HasColumnType("int");

                entity.Property(e => e.GMTModified)
               .HasColumnName("gmt_modified")
               .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAdminId)
               .HasColumnName("modified_admin_id")
               .HasColumnType("int");

                entity.Property(e => e.ModifiedIp)
               .HasColumnName("modified_ip")
               .HasColumnType("varchar(50)");
            });
            modelBuilder.Entity<ProductDiscuss>(entity =>
            {
                entity.ToTable("T_ProductDiscuss");

                entity.HasOne(d => d.Products)
                .WithMany(p => p.ProductDiscuss)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductDiscuss_To_Products");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.ProductDiscuss)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductDiscuss_To_Member");
            });
            modelBuilder.Entity<ProductPicture>(entity =>
            {
                entity.ToTable("T_ProductPicture");

                entity.HasOne(d => d.ProductColor)
                .WithMany(p => p.ProductPicture)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductPicture_To_ProductColor");

                entity.HasOne(d => d.Products)
                .WithMany(p => p.ProductPicture)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductPicture_To_Products");

                entity.Property(e => e.Id)
               .HasColumnName("id")
               .HasColumnType("int");

                entity.Property(e => e.Sort)
               .HasColumnName("sort")
               .HasColumnType("int");

                entity.Property(e => e.ColorId)
               .HasColumnName("color_id")
               .HasColumnType("int");

                entity.Property(e => e.ProductId)
               .HasColumnName("product_id")
               .HasColumnType("int");

                entity.Property(e => e.Picture)
               .HasColumnName("picture")
               .HasColumnType("varchar(500)");

            });
            modelBuilder.Entity<ProductPrice>(entity=>
            {
                entity.ToTable("T_ProductPrice");

                entity.Property(e=>e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.PriceName)
                .HasColumnName("price_name")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.MinPrice)
                .HasColumnName("min_price")
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.MaxPrice)
                .HasColumnName("max_price")
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");
            });
            modelBuilder.Entity<ProductQuestion>(entity =>
            {
                entity.ToTable("T_ProductQuestion");

                entity.HasOne(d => d.Products)
                .WithMany(p => p.ProductQuestion)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ProductQuestion_To_Products");
            });
            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("T_Products");

                entity.HasOne(d => d.ProductClass)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.FistClassId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Products_To_ProductClass");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.FistClassId)
                .HasColumnName("fist_class_id")
                .HasColumnType("int");

                entity.Property(e => e.SecondClassId)
                .HasColumnName("second_class_id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.SmallTitle)
                .HasColumnName("small_title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Remark)
                .HasColumnName("remark")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.KeyTitle)
                .HasColumnName("key_title")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Keywords)
                .HasColumnName("keywords")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Picture)
                .HasColumnName("picture")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.PictureTag)
                .HasColumnName("picture_tag")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Sales)
                .HasColumnName("sales")
                .HasColumnType("int");

                entity.Property(e => e.Score)
                .HasColumnName("score")
                .HasColumnType("int");

                entity.Property(e => e.TextContent)
                .HasColumnName("text_content")
                .HasColumnType("text");

                entity.Property(e => e.Parameter)
                .HasColumnName("parameter")
                .HasColumnType("text");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.GMTModified)
                .HasColumnName("gmt_modified")
                .HasColumnType("datetime");

                entity.Property(e => e.Hits)
                .HasColumnName("hits")
                .HasColumnType("int");

                entity.Property(e => e.GMTLastHit)
                .HasColumnName("gmt_last_hit")
                .HasColumnType("datetime");

                entity.Property(e => e.Praise)
                .HasColumnName("praise")
                .HasColumnType("int");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");

                entity.Property(e => e.IsHome)
                .HasColumnName("is_home")
                .HasColumnType("bit");

                entity.Property(e => e.IsNew)
                .HasColumnName("is_new")
                .HasColumnType("bit");

                entity.Property(e => e.IsBest)
                .HasColumnName("is_best")
                .HasColumnType("bit");

                entity.Property(e => e.IsHot)
                .HasColumnName("is_hot")
                .HasColumnType("bit");

                entity.Property(e => e.IsSale)
                .HasColumnName("is_sale")
                .HasColumnType("bit");

                entity.Property(e => e.IsFocus)
                .HasColumnName("is_focus")
                .HasColumnType("bit");
            });
            modelBuilder.Entity<Province>(entity=>
            {
                entity.ToTable("T_Province");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.ParentId)
                .HasColumnName("parent_id")
                .HasColumnType("int");

                entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.NameEn)
                .HasColumnName("name_en")
                .HasColumnType("varchar(100)");
            });
            modelBuilder.Entity<ReturnGoods>(entity =>
            {
                entity.ToTable("T_ReturnGoods");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.ReturnGoods)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ReturnGoods_To_Member");

                entity.HasOne(d => d.Express)
                .WithMany(p => p.ReturnGoods)
                .HasForeignKey(d => d.ExpressType)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ReturnGoods_To_Express");
            });
            modelBuilder.Entity<ReturnPicture>(entity =>
            {
                entity.ToTable("T_ReturnPicture");

                entity.HasOne(d => d.ReturnGoods)
                .WithMany(p => p.ReturnPicture)
                .HasForeignKey(d => d.ReturnCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ReturnPicture_To_ReturnGoods");
            });
            modelBuilder.Entity<SafeQuestion>(entity=>
            {
                entity.ToTable("T_SafeQuestion");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.Question)
                .HasColumnName("question")
                .HasColumnType("varchar(200)");

                entity.Property(e => e.Type)
                .HasColumnName("type")
                .HasColumnType("int");
            });
            modelBuilder.Entity<ShippingAddress>(entity =>
            {
                entity.ToTable("T_ShippingAddress");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.ShippingAddress)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ShippingAddress_To_Member");


            });
            modelBuilder.Entity<ShopCart>(entity =>
            {
                entity.ToTable("T_ShopCart");

                entity.HasOne(d => d.Member)
                .WithMany(p => p.ShopCart)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ShopCart_To_Member");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.BuyerId)
                .HasColumnName("buyer_id")
                .HasColumnType("int");

                entity.Property(e => e.ProductId)
                .HasColumnName("product_id")
                .HasColumnType("int");

                entity.Property(e => e.ProductName)
                .HasColumnName("product_name")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.ProductColor)
                .HasColumnName("product_color")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.ProductPrice)
                .HasColumnName("product_price")
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.ProductCount)
                .HasColumnName("product_count")
                .HasColumnType("int");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.Area)
                .HasColumnName("area")
                .HasColumnType("varchar(200)");
            });
            modelBuilder.Entity<Tencent>(entity=>
            {
                entity.ToTable("T_Tencent");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.QQNumber)
                .HasColumnName("qq_number")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.QQName)
                .HasColumnName("qq_name")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");
            });
            modelBuilder.Entity<Texts>(entity =>
            {
                entity.ToTable("T_Texts");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Texts)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Texts_To_WebColumn");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.ColumnId)
                .HasColumnName("column_id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Picture)
                .HasColumnName("picture")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.KeyTitle)
                .HasColumnName("key_title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Keywords)
                .HasColumnName("keywords")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Author)
                .HasColumnName("author")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.Source)
                .HasColumnName("source")
                .HasColumnType("varchar(100)");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.CreateAdminId)
                .HasColumnName("create_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.GMTModified)
                .HasColumnName("gmt_modified")
                .HasColumnType("datetime");

                entity.Property(e => e.ModifiedAdminId)
                .HasColumnName("modified_admin_id")
                .HasColumnType("int");

                entity.Property(e => e.ModifiedIp)
                .HasColumnName("modified_ip")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Hits)
                .HasColumnName("hits")
                .HasColumnType("int");

                entity.Property(e => e.GMTLastHit)
                .HasColumnName("gmt_last_hit")
                .HasColumnType("datetime");

                entity.Property(e => e.Praise)
                .HasColumnName("praise")
                .HasColumnType("int");

                entity.Property(e => e.TextContent)
                .HasColumnName("text_content")
                .HasColumnType("text");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");

                entity.Property(e => e.IsHead)
                .HasColumnName("is_head")
                .HasColumnType("bit");
            });
            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("T_Video");

                entity.HasOne(d => d.WebColumn)
                .WithMany(p => p.Video)
                .HasForeignKey(d => d.ColumnId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Video_To_WebColumn");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int");

                entity.Property(e => e.ColumnId)
                .HasColumnName("column_id")
                .HasColumnType("int");

                entity.Property(e => e.Sort)
                .HasColumnName("sort")
                .HasColumnType("int");

                entity.Property(e => e.Title)
                .HasColumnName("title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.KeyTitle)
                .HasColumnName("key_title")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Keywords)
                .HasColumnName("keywords")
                .HasColumnType("varchar(300)");

                entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.TextContent)
                .HasColumnName("text_content")
                .HasColumnType("text");

                entity.Property(e => e.Picture)
                .HasColumnName("picture")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.VideoUrl)
                .HasColumnName("video_url")
                .HasColumnType("varchar(1000)");

                entity.Property(e => e.VideoName)
                .HasColumnName("video_name")
                .HasColumnType("varchar(500)");

                entity.Property(e => e.Source)
                .HasColumnName("source")
                .HasColumnType("varchar(50)");

                entity.Property(e => e.Hits)
                .HasColumnName("hits")
                .HasColumnType("int");

                entity.Property(e => e.GMTCreate)
                .HasColumnName("gmt_create")
                .HasColumnType("datetime");

                entity.Property(e => e.IsShow)
                .HasColumnName("is_show")
                .HasColumnType("bit");

                entity.Property(e => e.IsHead)
                .HasColumnName("is_head")
                .HasColumnType("bit");
            });
            modelBuilder.Entity<WebColumn>(entity =>
            {
                entity.ToTable("T_WebColumn");

                entity.HasOne(d => d.ColumnType)
                .WithMany(p => p.WebColumn)
                .HasForeignKey(d => d.ColumnTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_WebColumn_To_ColumnType");

                entity.Property(e => e.Id)
               .HasColumnName("id")
               .HasColumnType("int");

                entity.Property(e => e.ColumnName)
               .HasColumnName("column_name")
               .HasColumnType("varchar(200)");

                entity.Property(e => e.Banner)
               .HasColumnName("banner")
               .HasColumnType("varchar(500)");

                entity.Property(e => e.BackgroundImg)
               .HasColumnName("background_img")
               .HasColumnType("varchar(500)");

                entity.Property(e => e.KeyTitle)
               .HasColumnName("key_title")
               .HasColumnType("varchar(300)");

                entity.Property(e => e.Keywords)
               .HasColumnName("keywords")
               .HasColumnType("varchar(300)");

                entity.Property(e => e.Description)
               .HasColumnName("description")
               .HasColumnType("varchar(500)");

                entity.Property(e => e.ColumnLevel)
               .HasColumnName("column_level")
               .HasColumnType("int");

                entity.Property(e => e.ColumnTypeId)
               .HasColumnName("column_type_id")
               .HasColumnType("int");

                entity.Property(e => e.ParentNode)
               .HasColumnName("parent_node")
               .HasColumnType("int");

                entity.Property(e => e.Sort)
               .HasColumnName("sort")
               .HasColumnType("int");

                entity.Property(e => e.Linkurl)
               .HasColumnName("link_url")
               .HasColumnType("varchar(500)");
            });
            modelBuilder.Entity<WebSite>(entity=>
            {
                entity.ToTable("T_WebSite");

                entity.Property(e => e.QQNumber)
               .HasColumnName("qq_number")
               .HasColumnType("string");

            });
            modelBuilder.Entity<User>().ToTable("T_User");

            

            

            base.OnModelCreating(modelBuilder);
        }
    }
}
