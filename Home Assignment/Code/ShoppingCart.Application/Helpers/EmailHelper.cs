using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace ShoppingCart.Application.Helpers
{
    public class EmailHelper
    {
        private string _receiverEmail;
        private string _emailSubject;
        private string _emailBody;
        
        public EmailHelper(string receiverEmail, string emailSubject, string emailBody)
        {
            this._receiverEmail = receiverEmail;
            this._emailSubject = emailSubject;
            this._emailBody = emailBody;
        }
        
        public void SendEmail() {
            // using (MailMessage mm = new MailMessage("applicationTestUser123@gmail.com", _receiverEmail))
            // {
            //     mm.Subject = _emailSubject;
            //     mm.Body = _emailBody;
            //     mm.IsBodyHtml = false;
            //     using (SmtpClient smtp = new SmtpClient())
            //     {
            //         smtp.Host = "smtp.gmail.com";
            //         smtp.EnableSsl = true;
            //         NetworkCredential networkCred = new NetworkCredential("applicationTestUser123@gmail.com", "HXQszjlQ8E2k");
            //         smtp.UseDefaultCredentials = true;
            //         smtp.Credentials = networkCred;
            //         smtp.Port = 465;
            //         smtp.Send(mm);
            //     }
            // } 
            
            // SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 465);
            //
            // smtpClient.Credentials = new System.Net.NetworkCredential("applicationTestUser123@gmail.com", "HXQszjlQ8E2k");
            // // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
            // smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            // smtpClient.EnableSsl = true;
            // MailMessage mail = new MailMessage();
            //
            // //Setting From , To and CC
            // mail.From = new MailAddress("applicationTestUser123@gmail.com", "Application Test User");
            // mail.To.Add(new MailAddress(_receiverEmail));
            // mail.Body = _emailBody;
            // mail.Subject = _emailSubject;
            //
            // smtpClient.Send(mail);
            
            SmtpClient smtpClient = new SmtpClient("smtp.live.com", 587);
                    
            smtpClient.Credentials = new System.Net.NetworkCredential("applicationtestuser123@outlook.com", "HXQszjlQ8E2k");
            // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
                    
            //Setting From , To and CC
            mail.From = new MailAddress("applicationtestuser123@outlook.com", "SA_Secure Website");
            mail.To.Add(new MailAddress(_receiverEmail));
            mail.Body = _emailBody;
            mail.Subject = _emailSubject;
                    
            smtpClient.Send(mail);
        }
    }
}