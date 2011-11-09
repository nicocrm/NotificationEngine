using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine
{
    public class EmailAction : IWorkItemAction
    {
        private String _body, _subject;

        private SmtpDeliverySystem _delivery;


        public void Execute(List<IRecord> records, List<IWorkItemTarget> targets)
        {
            String body = TemplateEngine.EvaluateTemplate(records, _body);
            String subject = TemplateEngine.EvaluateTemplate(records.Take(1), _subject);
            String recipient = String.Join(", ", targets.Select(x => x.Evaluate(records[0])).ToArray());

            _delivery.SendEmail(recipient, subject, body);
        }

        /// <summary>
        /// Load configuration containing the SMTP host, username, password, email subject & body
        /// </summary>
        /// <param name="configBlob"></param>
        public void LoadConfiguration(string configBlob)
        {
            throw new NotImplementedException();
        }
    }
}
