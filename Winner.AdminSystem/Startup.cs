using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Winner.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

using Microsoft.Extensions.WebEncoders;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace Winner.AdminSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<AccountContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //设置自定义配置信息  
            services.Configure<MailUser>(Configuration.GetSection("MailUser"));
            services.Configure<SmsUser>(Configuration.GetSection("SmsUser"));
            services.Configure<KuaidiUser>(Configuration.GetSection("KuaidiUser"));

            // Add framework services.  
            services.AddMvc();

            //添加cache支持 
            services.AddDistributedMemoryCache();
            //memorycache支持
            services.AddMemoryCache();
            //添加session支持
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
            //依赖注入的生命周期

            //添加Cookie 支持
            services.AddAuthentication("MyShopCart").AddCookie("MyShopCart", options =>
            {
                //options.AccessDeniedPath = "/Account/Forbidden/";//拒绝访问地址
                //options.LoginPath = "/Home/Login/";//登录地址
                //options.CookieDomain = "awei.com";
            });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = "MyShopCart";
            //    options.DefaultChallengeScheme = "MyShopCart";
            //}).AddCookie("MyShopCart");

            //文字被编码 https://github.com/aspnet/HttpAbstractions/issues/315
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
