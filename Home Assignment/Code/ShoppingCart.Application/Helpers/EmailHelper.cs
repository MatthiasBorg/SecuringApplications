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
        
        // Code to send email
        public void SendEmail() {
            
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