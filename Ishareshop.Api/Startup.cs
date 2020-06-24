using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Winner.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.WebEncoders;
using System.Text.Unicode;
using System.Text.Encodings.Web;

using Microsoft.OpenApi.Models;
using System.IO;

using Winner.IRepository;
using Winner.Repository;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ishareshop.Api
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
            services.AddControllers();
            services.AddDbContext<AccountContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IProductClassService, ProductClassService>();
            services.AddTransient<INewsTypeService, NewsTypeService>();
            services.AddTransient<IBannerService, BannerService>();
            services.AddTransient<INewsService, NewsService>();
            //services.AddTransient<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();

            //string conn = Configuration.GetConnectionString("DefaultConnection");
            //Winner.Models.AccountContext.Con
            //设置自定义配置信息  
            services.Configure<MailUser>(Configuration.GetSection("MailUser"));
            services.Configure<SmsUser>(Configuration.GetSection("SmsUser"));
            services.Configure<KuaidiUser>(Configuration.GetSection("KuaidiUser"));

            services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));

            // Add framework services.  
            services.AddMvc();

            //添加cache支持 
            services.AddDistributedMemoryCache();
            //memorycache支持
            services.AddMemoryCache();
            //添加session支持
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
            //依赖注入的生命周期
            services.AddCors();
            //添加Cookie 支持
            //services.AddAuthentication("MyShopCart").AddCookie("MyShopCart", options =>
            //{
            //    //options.AccessDeniedPath = "/Account/Forbidden/";//拒绝访问地址
            //    //options.LoginPath = "/Home/Login/";//登录地址
            //    //options.CookieDomain = "awei.com";
            //});

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

            //添加Swagger支持
            #region swagger
            services.AddControllers().AddNewtonsoftJson();
            var basePath = AppContext.BaseDirectory;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "IshareshopApi文档",
                    Description = "这是一个简单的NetCore Api项目",
                    Contact = new OpenApiContact
                    {
                        Name = "Ishareshopcore"
                    },
                    Version = "v1.0"
                });
                c.IncludeXmlComments(Path.Combine(basePath, "Ishareshop.NetCore.WebApi.xml"));
            });
            #endregion
            //添加jwt支持
            var jwtSetting = new JwtSetting();
            Configuration.Bind("JwtSetting", jwtSetting);
            services.AddAuthentication(options =>
            {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSetting.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSetting.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey)),

                    // 默认允许 300s  的时间偏移量，设置为0
                    //ValidateLifetime = true,
                    //ClockSkew = TimeSpan.Zero
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //当然,我们也可以不在 注册 Mvc 中间件的时候添加路由, 还是像官方推荐的那样, 在控制器上利用路由特性, 这种方式就支持 Restful 风格的请求方式了.
                //endpoints.MapControllerRoute(
                // name: "default",
                // pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapAreaControllerRoute(
                // name: "areas", "areas",
                // pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute("apidefault","api/[controller]/");
                endpoints.MapControllers();
            });

            //swagger支持
            #region
            app.UsePathBase("/Ishareshopcore");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/Ishareshopcore/swagger/v1/swagger.json", "Ishareshop.NetCore.WebApi");
                c.RoutePrefix = string.Empty;
            });
            #endregion
        }
    }
}
