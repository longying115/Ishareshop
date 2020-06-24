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
            //�����Զ���������Ϣ  
            services.Configure<MailUser>(Configuration.GetSection("MailUser"));
            services.Configure<SmsUser>(Configuration.GetSection("SmsUser"));
            services.Configure<KuaidiUser>(Configuration.GetSection("KuaidiUser"));

            services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));

            // Add framework services.  
            services.AddMvc();

            //���cache֧�� 
            services.AddDistributedMemoryCache();
            //memorycache֧��
            services.AddMemoryCache();
            //���session֧��
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
            //����ע�����������
            services.AddCors();
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

            //���Swagger֧��
            #region swagger
            services.AddControllers().AddNewtonsoftJson();
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

            //swagger֧��
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
