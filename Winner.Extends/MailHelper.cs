using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Winner.Extends
{
    public class MailHelper
    {
        /// <summary>
        /// 发送邮件(支持Html发送)
        /// </summary>
        /// <param name="subject">邮件标题</param>
        /// <param name="email">收件人地址</param>
        /// <param name="content">邮件内容</param>
        public static bool SendEmail(string Subject, string ToEmail, string Content, string WhereFrom, string WherePwd, string FromName, string Smtp)
        {
            bool resval = true;
            try
            {
                var message = new MimeMessage();
                //发信人
                message.From.Add(new MailboxAddress(FromName, WhereFrom));
                //收信人
                message.To.Add(new MailboxAddress("", ToEmail));
                //标题
                message.Subject = Subject;
                //产生一个支持Html的TextPart
                var body = new TextPart(TextFormat.Html)
                {
                    Text = Content
                };
                //先产生一个
                var multipart = new Multipart("mixed");
                //添加正文内容
                multipart.Add(body);
                //正文内容
                message.Body = multipart;
                using (var client = new SmtpClient())
                {
                    //连接到Smtp服务器
                    client.Connect(Smtp, 465, true);//之前之所以发布出去是由于阿里云屏蔽25端口，改为SSL模式发送用465端口
                    //登陆
                    client.Authenticate(WhereFrom, WherePwd);
                    //发送
                    client.Send(message);
                    //断开
                    client.Disconnect(true);
                }
            }
            catch
            {
                resval = false;
            }
            return resval;
        }
    }

}
