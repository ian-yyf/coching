using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Coching.Web.Models
{
    public class TableWidgetModel
    {
        public delegate string UrlHandler(int page);

        public enum CommandKind
        {
            Href,
            Js
        }

        public enum CommandGrade
        {
            Default,
            EasyMatching,
            Warm,
            Danger,
            Prohibit
        }

        public class Command
        {
            public Command()
            {

            }

            public Command(string name, string functionName, string value, CommandKind kind, CommandGrade grade)
            {
                Name = name;
                FunctionName = functionName;
                Value = value;
                Kind = kind;
                Grade = grade;
            }

            public string Name { get; set; }
            public string FunctionName { get; set; }
            public string Value { get; set; }
            public CommandKind Kind { get; set; }
            public CommandGrade Grade { get; set; }
        }

        public class Item
        {
            public Item()
            {

            }

            public Item(string id, string[] values, Command[] commands)
            {
                Id = id;
                Values = values;
                Commands = commands;
            }

            public string Id { get; set; }
            public string[] Values { get; set; }
            public Command[] Commands { get; set; }
        }

        public TableWidgetModel()
        {

        }

        public TableWidgetModel(string[] headers, IEnumerable<Item> datas, IPagedList page, UrlHandler url)
        {
            Headers = headers;
            Datas = datas;
            Page = page;
            Url = url;
        }

        public string[] Headers { get; set; }
        public IEnumerable<Item> Datas { get; set; }
        public IPagedList Page { get; set; }
        public UrlHandler Url { get; set; }
    }

    public class HtmlEditorWidgetModel
    {
        public HtmlEditorWidgetModel()
        {

        }

        public HtmlEditorWidgetModel(string id, string content, string textChanged, string htmlChanged)
        {
            Id = id;
            Content = content;
            TextChanged = textChanged;
            HtmlChanged = htmlChanged;
        }

        public string Id { get; set; }
        public string Content { get; set; }
        public string TextChanged { get; set; }
        public string HtmlChanged { get; set; }
    }
}
