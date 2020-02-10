using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Coching.Model.Data
{
    public class NodeData
    {
        public NodeData()
        {

        }

        public NodeData(Guid projectGuid, Guid rootGuid, Guid parentGuid, Guid creatorGuid, string name, string description, string htmlDescription, bool coching)
        {
            ProjectGuid = projectGuid;
            RootGuid = rootGuid;
            ParentGuid = parentGuid;
            CreatorGuid = creatorGuid;
            Name = name;
            Description = description;
            HtmlDescription = htmlDescription;
            EstimatedManHour = 0;
            ActualManHour = 0;
            StartTime = null;
            EndTime = null;
            WorkerGuid = Guid.Empty;
            Status = (int)NodeStatus.未进行;
            Coching = coching;
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
            HtmlDescription = rhs.HtmlDescription;
            EstimatedManHour = rhs.EstimatedManHour;
            ActualManHour = rhs.ActualManHour;
            StartTime = rhs.StartTime;
            EndTime = rhs.EndTime;
            WorkerGuid = rhs.WorkerGuid;
            Status = rhs.Status;
            Coching = rhs.Coching;
            CreatedTime = rhs.CreatedTime;
        }

        public NodeData(NodeData rhs, string name, string description, string htmlDescription)
            : this(rhs)
        {
            Name = name;
            Description = description;
            HtmlDescription = htmlDescription;
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
        [Column(TypeName = "text")]
        public string HtmlDescription { get; set; }
        [Column(TypeName = "decimal(10, 1)")]
        public decimal EstimatedManHour { get; set; }
        [Column(TypeName = "decimal(10, 1)")]
        public decimal ActualManHour { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid WorkerGuid { get; set; }
        public int Status { get; set; }
        public bool Coching { get; set; }
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

        public virtual string Label
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
                if (EstimatedManHour == 0)
                {
                    return "请根据预报工时确定";
                }
                return $"{EstimatedManHour.ToString("0.#")}工时";
            }
        }

        public string getResult()
        {
            if (ActualManHour == 0 || EstimatedManHour == 0)
            {
                return "尚无结果";
            }

            if (ActualManHour > EstimatedManHour)
            {
                return "超时";
            }

            if ((EstimatedManHour - ActualManHour) / EstimatedManHour < 0.1M)
            {
                return "准时";
            }
            else
            {
                return "未超时";
            }
        }

        public string ActualTime
        {
            get
            {
                if (ActualManHour == 0)
                {
                    return "任务完成后自动计算";
                }
                var time = $"{ActualManHour.ToString("0.#")}工时";
                if (EstimatedManHour != 0)
                {
                    time += $"（{getResult()}）";
                }
                return time;
            }
        }

        public string TimeInfo
        {
            get
            {
                if (ActualManHour == 0)
                {
                    if (EstimatedManHour != 0)
                    {
                        return $"预估{EstimatedManHour.ToString("0.#")}工时";
                    }
                    return null;
                }

                return $"{ActualManHour.ToString("0.#")}/{EstimatedManHour.ToString("0.#")}工时 {getResult()}";
            }
        }
    }
}
