using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.Base.Tools
{
    public class EmailTools
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="fromName"></param>
        /// <param name="host"></param>
        /// <param name="from"></param>
        /// <param name="pwd"></param>
        public static void SendMail(string to, string title, string content, string fromName = "悦风网", string host = "smtp.qq.com", string from = "cgrjerry@qq.com", string pwd = "")
        {
            MailAddress fromAddress = new MailAddress(from, fromName); //邮件的发件人
            MailMessage mail = new MailMessage();
            //设置邮件的标题
            mail.Subject = title;
            //设置邮件的发件人
            //Pass:如果不想显示自己的邮箱地址，这里可以填符合mail格式的任意名称，真正发mail的用户不在这里设定，这个仅仅只做显示用
            mail.From = fromAddress;
            mail.To.Add(to);
            mail.Body = content;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            //设置邮件的发送级别
            mail.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient();
            //设置用于 SMTP 事务的主机的名称，填IP地址也可以了
            client.Host = host;
            //设置用于 SMTP 事务的端口，默认的是 25
            //client.Port = 25;
            client.UseDefaultCredentials = false;
            //这里才是真正的邮箱登陆名和密码，比如我的邮箱地址是 hbgx@hotmail， 我的用户名为 hbgx ，我的密码是 xgbh
            client.Credentials = new System.Net.NetworkCredential(from, pwd);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {//都定义完了，正式发送了，很是简单吧！
                client.Send(mail);
            }
            catch (Exception)
            {
            }

        }
    }
}
