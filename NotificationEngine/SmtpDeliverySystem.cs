using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Sage.Common.Syndication.Json;
using System.Net.Mail;

namespace NotificationEngine
{
    public class SmtpSettings
    {
        public string SmtpServer;

        [DefaultValue(25)]
        public int SmtpPort;
        public string SmtpUser;
        public string SmtpUserPassword;

        [DefaultValue(false)]
        public bool EnableSsl;
        public string EmailAddress;
        public string DisplayName;

        [DefaultValue(false)]
        public bool IsBodyHtml;
    }
    
    public class SmtpDeliverySystem : IDeliverySystem
    {

        private SmtpSettings _settings = new SmtpSettings();

        public SmtpDeliverySystem()
        {
        }

        public SmtpDeliverySystem(SmtpSettings settings)
        {
            _settings = settings;
        }

        public void LoadConfiguration(string configBlob)
        {

            _settings = JsonConvert.DeserializeObject<SmtpSettings>(configBlob);
        }

        public void SendEmail(String[] recipients, String subject, String body, String[] cc = null, String[] bcc = null)
        {
            using (SmtpClient smtp = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort))
            {
                smtp.Credentials = new System.Net.NetworkCredential(_settings.SmtpUser, _settings.SmtpUserPassword);
                smtp.EnableSsl = _settings.EnableSsl;

                // Create a blank email message
                MailMessage email = new MailMessage();

                // Set the proper properties based on the deliveryMethod
                email.From = new MailAddress(_settings.EmailAddress, _settings.DisplayName);
                email.Sender = email.From;
                email.IsBodyHtml = _settings.IsBodyHtml;

                // Add "To" recipients
                foreach (string recipient in recipients)
                    email.To.Add(recipient);

                // Add "cc" recipients
                if (cc != null)
                {
                    foreach (string recipient in cc)
                        email.CC.Add(recipient);
                }

                // Add "bcc" recipients
                if (bcc != null)
                {
                    foreach (string recipient in bcc)
                        email.Bcc.Add(recipient);
                }

                // Add Subject
                email.Subject = subject;

                // Add Body
                email.Body = body;

                // Delivery the email
                smtp.Send(email);
            }
        }
    }
}
