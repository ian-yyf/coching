using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coching.Model.Data
{
    public class NodeData
    {
        public NodeData()
        {

        }

        public NodeData(Guid projectGuid, Guid rootGuid, Guid parentGuid, Guid creatorGuid, string name, string description)
        {
            ProjectGuid = projectGuid;
            RootGuid = rootGuid;
            ParentGuid = parentGuid;
            CreatorGuid = creatorGuid;
            Name = name;
            Description = description;
            EstimatedManHour = 0;
            ActualManHour = 0;
            StartTime = null;
            EndTime = null;
            WorkerGuid = Guid.Empty;
            Status = (int)NodeStatus.未进行;
            CreatedTime = DateTime.Now;
        }

        public NodeData(NodeData rhs)
        {
            ProjectGuid = rhs.ProjectGuid;
            RootGuid = rhs.RootGuid;
            ParentGuid = rhs.ParentGuid;
            CreatorGuid = rhs.CreatorGuid;
            Name = rhs.Name;
            Description = rhs.Description;
            EstimatedManHour = rhs.EstimatedManHour;
            ActualManHour = rhs.ActualManHour;
            StartTime = rhs.StartTime;
            EndTime = rhs.EndTime;
            WorkerGuid = rhs.WorkerGuid;
            Status = rhs.Status;
            CreatedTime = rhs.CreatedTime;
        }

        public NodeData(NodeData rhs, string name, string description)
            : this(rhs)
        {
            Name = name;
            Description = description;
        }

        public Guid ProjectGuid { get; set; }
        public Guid RootGuid { get; set; }
        public Guid ParentGuid { get; set; }
        public Guid CreatorGuid { get; set; }
        [Required]
        [StringLength(16)]
        public string Name { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        [Column(TypeName = "decimal(10, 1)")]
        public decimal EstimatedManHour { get; set; }
        [Column(TypeName = "decimal(10, 1)")]
        public decimal ActualManHour { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid WorkerGuid { get; set; }
        public int Status { get; set; }
        public DateTime CreatedTime { get; set; }

        public NodeStatus getStatus()
        {
            return (NodeStatus)Status;
        }

        public string StatusTitle
        {
            get
            {
                return getStatus().ToString();
            }
        }

        public bool isRoot()
        {
            return ParentGuid == Guid.Empty;
        }

        public string Label
        {
            get
            {
                if (Name.Length <= 10)
                {
                    return Name;
                }

                return Name.Substring(0, 8) + "...";
            }
        }

        public string EstimatedTime
        {
            get
            {
                return $"{EstimatedManHour.ToString("0.#")}工时";
            }
        }

        public string ActualTime
        {
            get
            {
                return $"{ActualManHour.ToString("0.#")}工时";
            }
        }
    }
}
