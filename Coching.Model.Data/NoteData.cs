using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coching.Model.Data
{
    public class NoteData
    {
        public NoteData()
        {

        }

        public NoteData(Guid nodeGuid, Guid partnerGuid, string content, DateTime createdTime)
        {
            NodeGuid = nodeGuid;
            PartnerGuid = partnerGuid;
            Content = content;
            CreatedTime = createdTime;
        }

        public NoteData(NoteData rhs)
        {
            NodeGuid = rhs.NodeGuid;
            PartnerGuid = rhs.PartnerGuid;
            Content = rhs.Content;
            CreatedTime = rhs.CreatedTime;
        }

        public Guid NodeGuid { get; set; }
        public Guid PartnerGuid { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public String Content { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
