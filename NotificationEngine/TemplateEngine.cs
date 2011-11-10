using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NotificationEngine
{
    public class TemplateEngine
    {
        public static String EvaluateTemplate(IList<IRecord> records, String template)
        {
            string result = null;            

            try
            {
                foreach (IRecord r in records)
                {
                    string merged = template;
                    var regex = new Regex("(<%.*%>)");
                    var v = regex.Matches(template);
                    foreach (Match match in v)
                    {
                        if (match.Value.Contains(":"))
                        {
                            string replacement = null;
                            var literal = match.Value;
                            literal = literal.Replace("<%", "");
                            literal = literal.Replace("%>", "");


                            switch (literal)
                            {
                                case ":ISARE":
                                    if (records.Count == 1)
                                    {
                                        replacement = "is";
                                    }
                                    else
                                    {
                                        replacement = "are";
                                    }
                                    break;
                                case ":HASHAVE":
                                    if (records.Count == 1)
                                    {
                                        replacement = "has";
                                    }
                                    else
                                    {
                                        replacement = "have";
                                    }
                                    break;
                                case ":ITEMCOUNT":
                                    replacement = records.Count.ToString();
                                    break;
                            }

                            merged = merged.Replace(match.Value, replacement);
                        }
                        else
                        {
                            var property = match.Value;
                            property = property.Replace("<%", "");
                            property = property.Replace("%>", "");
                            if (r.Get(property) != null)
                                merged = merged.Replace(string.Format("{0}", match.Value), r.Get(property));
                            else
                                merged = merged.Replace(string.Format("{0}", match.Value), "N/A");
                        }
                    }

                    if (result == null)
                        result = merged;
                    else
                        result = String.Format("{0}\n{1}", result, merged);
                }
            }
            catch { }

            return result;
        }
    }
}
