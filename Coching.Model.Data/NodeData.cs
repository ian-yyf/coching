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

        public NodeData(Guid rootGuid, Guid parentGuid, Guid creatorGuid, string name, string description, int totalMinutes, DateTime? startTime, DateTime? endTime, Guid? workerGuid, int status)
        {
            RootGuid = rootGuid;
            ParentGuid = parentGuid;
            CreatorGuid = creatorGuid;
            Name = name;
            Description = description;
            TotalMinutes = totalMinutes;
            StartTime = startTime;
            EndTime = endTime;
            WorkerGuid = workerGuid;
            Status = status;
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
    }
}
