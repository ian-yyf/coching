using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Public.Utils;

namespace Coching.Model.Data
{
    public class ActionLogLink
    {
        public ActionLogLink()
        {

        }

        public ActionLogLink(string action, string controller, KeyValuePair<string, string>[] @params, string content)
        {
            Action = action;
            Controller = controller;
            Params = @params;
            Content = content;
        }

        public string Action { get; set; }
        public string Controller { get; set; }
        public KeyValuePair<string, string>[] Params { get; set; }
        public string Content { get; set; }

        public string toString()
        {
            return "$$" + this.jsonEncode() + "$$";
        }

        public static ActionLogLink fromString(string str)
        {
            return str.Substring(2, str.Length - 4).jsonDecode<ActionLogLink>();
        }

        public static string toHtml(string content, Func<string, string, string> url)
        {
            var regex = new Regex("\\$\\$\\{.+?\\}\\$\\$");
            var matches = regex.Matches(content);
            foreach (Match match in matches)
            {
                var link = fromString(match.Value);
                var i = url(link.Action, link.Controller);
                if (link.Params != null && link.Params.Length > 0)
                {
                    i += "?" + link.Params.toQueryString();
                }
                content = content.Replace(match.Value, $"<a href=\"{i}\">{link.Content}</a>") ;
            }
            return content;
        }
    }
}
