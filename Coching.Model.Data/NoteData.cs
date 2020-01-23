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

        public NoteData(Guid nodeGuid, Guid creatorGuid, string content, string htmlContent)
        {
            NodeGuid = nodeGuid;
            CreatorGuid = creatorGuid;
            Content = content;
            HtmlContent = htmlContent;
            CreatedTime = DateTime.Now;
        }

        public NoteData(NoteData rhs)
        {
            NodeGuid = rhs.NodeGuid;
            CreatorGuid = rhs.CreatorGuid;
            Content = rhs.Content;
            HtmlContent = rhs.HtmlContent;
            CreatedTime = rhs.CreatedTime;
        }

        public NoteData(NoteData rhs, string content, string htmlContent)
            : this(rhs)
        {
            Content = content;
            HtmlContent = htmlContent;
        }

        public Guid NodeGuid { get; set; }
        public Guid CreatorGuid { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public String Content { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string HtmlContent { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
