using Coching.Model.Data;
using Coching.Model.Front;
using Public.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coching.Web.Models
{
    public class CochingViewModel
    {
        public CochingViewModel()
        {

        }

        public CochingViewModel(FNode[] roots)
        {
            Roots = roots;
        }

        public FNode[] Roots { get; set; }
    }

    public class NodeItemModel : PopupItemViewModel<NodeData, FNode>
    {
        public NodeItemModel()
        {

        }

        public NodeItemModel(Guid keyGuid, string actionName, string actionTitle, NodeData oldData, string callback)
           : base(keyGuid, actionName, actionTitle, oldData, callback)
        {
            ParentGuid = oldData.ParentGuid;
            RootGuid = oldData.RootGuid;
            Name = oldData.Name;
            Description = oldData.Description;
            Status = oldData.getStatus();
        }

        public NodeItemModel(string actionName, string actionTitle, Guid rootGuid, Guid parentGuid, string callback)
            : base(actionName, actionTitle, callback)
        {
            ParentGuid = parentGuid;
            RootGuid = rootGuid;
            Status = NodeStatus.未进行;
        }

        public Guid RootGuid { get; set; }
        public Guid ParentGuid { get; set; }
        [Required]
        [Display(Name = "名称")]
        [StringLength(16)]
        public string Name { get; set; }
        [Display(Name = "详情")]
        public string Description { get; set; }
        public NodeStatus Status { get; set; }
    }
}
