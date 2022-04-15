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
                    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;  // ����ʱ��Ϊ UTC)
                });
            services.AddDbContext<AccountContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #region ע��ҵ�����
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
            //�����Զ���������Ϣ  
            services.Configure<MailUser>(Configuration.GetSection("MailUser"));
            services.Configure<SmsUser>(Configuration.GetSection("SmsUser"));
            services.Configure<KuaidiUser>(Configuration.GetSection("KuaidiUser"));

            services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));
            services.Configure<RedisSection>(Configuration.GetSection("Redis"));
            // Add framework services.  
            services.AddMvc();

            //���cache֧�� 
            services.AddDistributedMemoryCache();
            //memorycache֧��
            services.AddMemoryCache();

            //���session֧��
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
            //����ע�����������
            services.AddCors(options=> {
                //һ��������ַ���
                options.AddPolicy("LimitRequests", policy =>
                {
                    // ֧�ֶ�������˿ڣ�ע��˿ںź�Ҫ��/б�ˣ�����localhost:8000/���Ǵ��
                    // ע�⣬http://127.0.0.1:5401 �� http://localhost:5401 �ǲ�һ���ģ�����д����
                    policy
                    .WithOrigins("http://127.0.0.1:5401", "http://localhost:5401", "http://127.0.0.1:5402", "http://localhost:5402")
                    .AllowAnyHeader()//�����κα�ͷ
                    .AllowAnyMethod();//�����κη���
                });
            });
            //���Cookie ֧��
            //services.AddAuthentication("MyShopCart").AddCookie("MyShopCart", options =>
            //{
            //    //options.AccessDeniedPath = "/Account/Forbidden/";//�ܾ����ʵ�ַ
            //    //options.LoginPath = "/Home/Login/";//��¼��ַ
            //    //options.CookieDomain = "awei.com";
            //});

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = "MyShopCart";
            //    options.DefaultChallengeScheme = "MyShopCart";
            //}).AddCookie("MyShopCart");

            //���ֱ����� https://github.com/aspnet/HttpAbstractions/issues/315
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });
            //��Ӷ�AutoMapper��֧��
            //services.AddAutoMapper(Assembly.Load("Ŀ���������ڵ������ռ�"), Assembly.Load("Դ�������ڵ������ռ�"));
            services.AddAutoMapper(Assembly.Load("Winner.Models"));//ͬ��һ�������ռ���
            //���Swagger֧��
            #region swagger�鿴·����https://localhost:44359/index.html
            if (this.Env.IsDevelopment())
            {
                var basePath = AppContext.BaseDirectory;
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "IshareshopApi�ĵ�",
                        Description = "����һ���򵥵�NetCore Api��Ŀ",
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
            //���jwt֧��
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

                    // Ĭ������ 300s  ��ʱ��ƫ����������Ϊ0
                    //ValidateLifetime = true,
                    //ClockSkew = TimeSpan.Zero
                };
            });

            //��Ӷ�ʱ����������
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, InitScheduleTask>();
            services.AddHostedService<InitScheduleTask>();//���������ͬ����
            //��ʱ����Quartz
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//ע��ISchedulerFactory��ʵ����
            #region ��ʱ����Quartz  ����ע������
            services.AddTransient<MyCronJob>();
            // ����ʹ��˲ʱ����ע��
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

            app.UseCors("LimitRequests");//�� CORS �м����ӵ� web Ӧ�ó��������, �������������

            app.UseEndpoints(endpoints =>
            {
                //��Ȼ,����Ҳ���Բ��� ע�� Mvc �м����ʱ�����·��, ������ٷ��Ƽ�������, �ڿ�����������·������, ���ַ�ʽ��֧�� Restful ��������ʽ��.
                //endpoints.MapControllerRoute(
                // name: "default",
                // pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapAreaControllerRoute(
                // name: "areas", "areas",
                // pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute("apidefault","api/[controller]/");
                endpoints.MapControllers();
            });

            #region //swagger֧��
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
            #region // ��Quartz.Net
            var quartz = app.ApplicationServices.GetRequiredService<QuartzStartup>();
            lifetime.ApplicationStarted.Register(quartz.Start);
            lifetime.ApplicationStopped.Register(quartz.Stop);
            #endregion
        }
    }
}
