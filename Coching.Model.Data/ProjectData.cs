using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace Coching.Model.Data
{
    public class ProjectData
    {
        public ProjectData()
        {

        }

        public ProjectData(Guid creatorGuid, string name, string header, string description, string htmlDescription)
        {
            CreatorGuid = creatorGuid;
            Name = name;
            Header = header;
            Description = description;
            HtmlDescription = htmlDescription;
            CreatedTime = DateTime.Now;
        }

        public ProjectData(ProjectData rhs)
        {
            CreatorGuid = rhs.CreatorGuid;
            Name = rhs.Name;
            Header = rhs.Header;
            Description = rhs.Description;
            HtmlDescription = rhs.HtmlDescription;
            CreatedTime = rhs.CreatedTime;
        }

        public ProjectData(ProjectData rhs, string name, string header, string description, string htmlDescription)
            : this(rhs)
        {
            Name = name;
            Header = header;
            Description = description;
            HtmlDescription = htmlDescription;
        }

        public Guid CreatorGuid { get; set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Header { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        [Column(TypeName = "text")]
        public string HtmlDescription { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
