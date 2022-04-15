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
            //������log����serilog���������
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build())
                //.WriteTo.MSSqlServer("Data Source=DESKTOP-4TU9A6M;Initial Catalog=CoreFrame;User ID=sa;Password=123456", "logs", autoCreateSqlTable: true, restrictedToMinimumLevel: LogEventLevel.Information)//���������ĸ������ֱ������ݿ������ַ���������������������Ƿ񴴽�����͵ȼ���Serilog��Ĭ�ϴ���һЩ�С�                
                //.WriteTo.Email(new EmailConnectionInfo()
                //{
                //    EmailSubject = "ϵͳ����,�����ٲ鿴!",//�ʼ�����
                //    FromEmail = "291***@qq.com",//����������
                //    MailServer = "smtp.qq.com",//smtp��������ַ
                //    NetworkCredentials = new NetworkCredential("291***@qq.com", "###########"),//���������ֱ��Ƿ�����������ͻ�����Ȩ��
                //    Port = 587,//�˿ں�
                //    ToEmail = "188***@163.com"//�ռ���
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
            .UseSerilog();//��������������ʱ������serilog,��΢��ILogger��������
    }
}
