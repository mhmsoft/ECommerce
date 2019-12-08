using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;

namespace ECommerce.Services
{
    public  class MailService
    {

        public static string Link { get; set; }
        public static string title { get; set; }
        public static string Subject { get; set; }
        public static string Body { get; set; }

        //send a mail method
        public static void sendEmail(string To)
        {   // web.config'ten mail ayarlarını al
            SmtpSection network = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            try
            {
                
                var link = Link;
                var fromEmail = new MailAddress(network.Network.UserName, title);
                var toEmail = new MailAddress(To);

                string subject =Subject;
                string body = Body;
                // Gönderici bilgileri
                var smtp = new SmtpClient
                {
                    Host = network.Network.Host,
                    Port = network.Network.Port,
                    EnableSsl = network.Network.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = network.Network.DefaultCredentials,
                    Credentials = new NetworkCredential(network.Network.UserName, network.Network.Password)
                };
                // mail mesajı
                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    
}