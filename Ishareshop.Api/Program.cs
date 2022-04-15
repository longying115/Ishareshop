using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Ishareshop.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //第三方log工具serilog的相关配置
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build())
                //.WriteTo.MSSqlServer("Data Source=DESKTOP-4TU9A6M;Initial Catalog=CoreFrame;User ID=sa;Password=123456", "logs", autoCreateSqlTable: true, restrictedToMinimumLevel: LogEventLevel.Information)//从左至右四个参数分别是数据库连接字符串、表名、如果表不存在是否创建、最低等级。Serilog会默认创建一些列。                
                //.WriteTo.Email(new EmailConnectionInfo()
                //{
                //    EmailSubject = "系统警告,请速速查看!",//邮件标题
                //    FromEmail = "291***@qq.com",//发件人邮箱
                //    MailServer = "smtp.qq.com",//smtp服务器地址
                //    NetworkCredentials = new NetworkCredential("291***@qq.com", "###########"),//两个参数分别是发件人邮箱与客户端授权码
                //    Port = 587,//端口号
                //    ToEmail = "188***@163.com"//收件人
                //})
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseSerilog();//在宿主机启动的时候配置serilog,与微软ILogger进行整合
    }
}
