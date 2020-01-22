using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace Coching.Model.Data
{
    public class NoteData
    {
        public NoteData()
        {

        }

        public NoteData(Guid nodeGuid, Guid creatorGuid, string content)
        {
            NodeGuid = nodeGuid;
            CreatorGuid = creatorGuid;
            Content = content;
            CreatedTime = DateTime.Now;
        }

        public NoteData(NoteData rhs)
        {
            NodeGuid = rhs.NodeGuid;
            CreatorGuid = rhs.CreatorGuid;
            Content = rhs.Content;
            CreatedTime = rhs.CreatedTime;
        }

        public NoteData(NoteData rhs, string content)
            : this(rhs)
        {
            Content = content;
        }

        public Guid NodeGuid { get; set; }
        public Guid CreatorGuid { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public String Content { get; set; }
        public DateTime CreatedTime { get; set; }

        public string HtmlContent
        {
            get
            {
                if (Content == null)
                {
                    return Content;
                }
                var regex = new Regex("[\\r\\n]+");
                return regex.Replace(Content, "<br>");
            }
        }
    }
}
