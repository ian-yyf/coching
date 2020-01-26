using Public.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coching.Model.Data
{
    public class ActionLogData
    {
        public ActionLogData()
        {

        }

        public ActionLogData(Guid projectGuid, Guid userGuid, ActionLogKind kind, string content)
        {
            ProjectGuid = projectGuid;
            UserGuid = userGuid;
            Kind = (int)kind;
            Content = content;
            CreatedTime = DateTime.Now;
        }

        public ActionLogData(ActionLogData rhs)
        {
            ProjectGuid = rhs.ProjectGuid;
            UserGuid = rhs.UserGuid;
            Kind = rhs.Kind;
            Content = rhs.Content;
            CreatedTime = rhs.CreatedTime;
        }

        public Guid ProjectGuid { get; set; }
        public Guid UserGuid { get; set; }
        public int Kind { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }

        public ActionLogKind getActionLogKind()
        {
            return (ActionLogKind)Kind;
        }

        public string getHtmlContent(Func<string, string, string> url)
        {
            return ActionLogLink.toHtml(Content, url);
        }

        public string DisplayCreatedTime
        {
            get
            {
                return CreatedTime.formatDisplayTime();
            }
        }
    }
}
