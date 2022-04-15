using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
using AutoMapper;
using System.Reflection;
using Winner.Extends.Interfaces;
using Winner.Extends;
using Quartz;
using Quartz.Impl;
using Winner.Service;
using Microsoft.Extensions.Logging;
using Winner.Repository.Services.Interfaces;
using Winner.Repository.Services;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Ishareshop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            Env = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                //.AddMvcOptions(option=> {
                    //option.Filters.Add<GlobalExceptionFilter>();
                // })
                .AddNewtonsoftJson(opt=> {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;  // 设置时区为 UTC)
                });
            services.AddDbContext<AccountContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #region 注册业务服务
            services.AddTransient<IProductClassService, ProductClassService>();
            services.AddTransient<INewsTypeService, NewsTypeService>();
            services.AddTransient<IBannerService, BannerService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICaiService, CaiService>();

            services.AddScoped<IRedisHelper, RedisHelper>();
            #endregion
            //string conn = Configuration.GetConnectionString("DefaultConnection");
            //Winner.Models.AccountContext.Con
            //设置自定义配置信息  
            services.Configure<MailUser>(Configuration.GetSection("MailUser"));
            services.Configure<SmsUser>(Configuration.GetSection("SmsUser"));
            services.Configure<KuaidiUser>(Configuration.GetSection("KuaidiUser"));

            services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));
            services.Configure<RedisSection>(Configuration.GetSection("Redis"));
            // Add framework services.  
            services.AddMvc();

            //添加cache支持 
            services.AddDistributedMemoryCache();
            //memorycache支持
            services.AddMemoryCache();

            //添加session支持
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
            //依赖注入的生命周期
            services.AddCors(options=> {
                //一般采用这种方法
                options.AddPolicy("LimitRequests", policy =>
                {
                    // 支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    // 注意，http://127.0.0.1:5401 和 http://localhost:5401 是不一样的，尽量写两个
                    policy
                    .WithOrigins("http://127.0.0.1:5401", "http://localhost:5401", "http://127.0.0.1:5402", "http://localhost:5402")
                    .AllowAnyHeader()//允许任何标头
                    .AllowAnyMethod();//允许任何方法
                });
            });
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
            //添加对AutoMapper的支持
            //services.AddAutoMapper(Assembly.Load("目标类型所在的命名空间"), Assembly.Load("源类型所在的命名空间"));
            services.AddAutoMapper(Assembly.Load("Winner.Models"));//同在一个命名空间下
            //添加Swagger支持
            #region swagger查看路径：https://localhost:44359/index.html
            if (this.Env.IsDevelopment())
            {
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
            }
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

            //添加定时任务启动类
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, InitScheduleTask>();
            services.AddHostedService<InitScheduleTask>();//跟上面这个同样的
            //定时任务Quartz
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。
            #region 定时任务Quartz  依赖注入配置
            services.AddTransient<MyCronJob>();
            // 这里使用瞬时依赖注入
            services.AddSingleton<QuartzStartup>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("LimitRequests");//将 CORS 中间件添加到 web 应用程序管线中, 以允许跨域请求。

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

            #region //swagger支持
            if (env.IsDevelopment())
            {
                app.UsePathBase("/Ishareshopcore");
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/Ishareshopcore/swagger/v1/swagger.json", "Ishareshop.NetCore.WebApi");
                    c.RoutePrefix = string.Empty;
                });
            }
            #endregion
            #region // 绑定Quartz.Net
            var quartz = app.ApplicationServices.GetRequiredService<QuartzStartup>();
            lifetime.ApplicationStarted.Register(quartz.Start);
            lifetime.ApplicationStopped.Register(quartz.Stop);
            #endregion
        }
    }
}
