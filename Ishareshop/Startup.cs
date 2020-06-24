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

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

using Microsoft.Extensions.WebEncoders;
using System.Text.Unicode;
using System.Text.Encodings.Web;

using Microsoft.OpenApi.Models;
using System.IO;

namespace Ishareshop
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AccountContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //�����Զ���������Ϣ  
            services.Configure<MailUser>(Configuration.GetSection("MailUser"));
            services.Configure<SmsUser>(Configuration.GetSection("SmsUser"));
            services.Configure<KuaidiUser>(Configuration.GetSection("KuaidiUser"));

            // Add framework services.  
            services.AddMvc();

            //���cache֧�� 
            services.AddDistributedMemoryCache();
            //memorycache֧��
            services.AddMemoryCache();
            //���session֧��
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
            //����ע�����������

            //���Cookie ֧��
            services.AddAuthentication("MyShopCart").AddCookie("MyShopCart", options =>
            {
                //options.AccessDeniedPath = "/Account/Forbidden/";//�ܾ����ʵ�ַ
                //options.LoginPath = "/Home/Login/";//��¼��ַ
                //options.CookieDomain = "awei.com";
            });

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
            services.AddSwaggerGen(c=>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "IshareshopAPI�ĵ�",
                    Description = "����һ���򵥵�NetCore Api��Ŀ",
                    Contact=new OpenApiContact
                    {
                        Name= "Ishareshopcore"
                    },
                    Version="v1.0"
                });
                c.IncludeXmlComments(Path.Combine(basePath,"Ishareshop.NetCore.WebApi.xml"));
            });
            #endregion
        }
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

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
            app.UseSwaggerUI(c=> 
            {
                c.SwaggerEndpoint("/Ishareshopcore/swagger/v1/swagger.json", "Ishareshop.NetCore.WebApi");
                c.RoutePrefix = string.Empty;
            });
            #endregion
        }
    }
}
