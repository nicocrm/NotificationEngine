using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace NotificationEngine
{
    public class EmailAction : IWorkItemAction
    {
        private String _body, _subject;

        private SmtpDeliverySystem _delivery;


        public void Execute(IList<IRecord> records, IList<IWorkItemTarget> targets)
        {
            String body = TemplateEngine.EvaluateTemplate(records, _body);
            String subject = TemplateEngine.EvaluateTemplate(records.Take(1).ToList(), _subject);
            String[] recipient = targets.Select(x => x.Evaluate(records[0])).ToArray();

            _delivery.SendEmail(recipient, subject, body);
        }

        /// <summary>
        /// Load configuration containing the email Subject and Body
        /// </summary>
        /// <param name="configBlob"></param>
        public void LoadConfiguration(string configBlob)
        {
            XDocument xml = XDocument.Parse(configBlob);
            XElement el = xml.Root.Element("Body");
            if (el == null)
                throw new Exception("Missing element body");
            _body = el.Value;
            el = xml.Root.Element("Subject");
            if (el == null)
                throw new Exception("Missing element subject");
            _subject = el.Value;

        }

        public IDeliverySystem DeliverySystem
        {
            get;
            set; 
        }
    }
}
