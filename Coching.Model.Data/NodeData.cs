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

        public NodeData(Guid rootGuid, Guid parentGuid, Guid creatorGuid, string name, string description, NodeStatus status)
        {
            RootGuid = rootGuid;
            ParentGuid = parentGuid;
            CreatorGuid = creatorGuid;
            Name = name;
            Description = description;
            TotalMinutes = 0;
            StartTime = null;
            EndTime = null;
            WorkerGuid = null;
            Status = (int)status;
        }

        public NodeData(NodeData rhs)
        {
            RootGuid = rhs.RootGuid;
            ParentGuid = rhs.ParentGuid;
            CreatorGuid = rhs.CreatorGuid;
            Name = rhs.Name;
            Description = rhs.Description;
            TotalMinutes = rhs.TotalMinutes;
            StartTime = rhs.StartTime;
            EndTime = rhs.EndTime;
            WorkerGuid = rhs.WorkerGuid;
            Status = rhs.Status;
        }

        public NodeData(NodeData rhs, string name, string description, NodeStatus status)
            : this(rhs)
        {
            Name = name;
            Description = description;
            Status = (int)status;
        }

        public Guid RootGuid { get; set; }
        public Guid ParentGuid { get; set; }
        public Guid CreatorGuid { get; set; }
        [Required]
        [StringLength(16)]
        public string Name { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        public int TotalMinutes { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? WorkerGuid { get; set; }
        public int Status { get; set; }

        public NodeStatus getStatus()
        {
            return (NodeStatus)Status;
        }

        public bool isRoot()
        {
            return ParentGuid == Guid.Empty;
        }

        public string getLabel()
        {
            if (Name.Length <= 10)
            {
                return Name;
            }

            return Name.Substring(0, 8) + "...";
        }
    }
}
