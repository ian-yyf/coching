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

    public class NodeItemViewModel : PopupItemViewModel<NodeData, FNode>
    {
        public NodeItemViewModel()
        {

        }

        public NodeItemViewModel(Guid keyGuid, string actionName, string actionTitle, NodeData oldData, string callback)
           : base(keyGuid, actionName, actionTitle, oldData, callback)
        {
            ParentGuid = oldData.ParentGuid;
            RootGuid = oldData.RootGuid;
            Name = oldData.Name;
            Description = oldData.Description;
            Status = oldData.getStatus();
        }

        public NodeItemViewModel(string actionName, string actionTitle, Guid rootGuid, Guid parentGuid, string callback)
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

    public class NodeDetailViewModel
    {
        public NodeDetailViewModel()
        {

        }

        public NodeDetailViewModel(FNodeDetail data)
        {
            Data = data;
        }

        public FNodeDetail Data { get; set; }

        public List<KeyValuePair<int, string>> StatusList
        {
            get
            {
                return Public.Mvc.ExtendUtils.toKeyValues<NodeStatus>();
            }
        }
    }

    public class NoteItemViewModel : PopupItemViewModel<NoteData, FNote>
    {
        public NoteItemViewModel()
        {

        }

        public NoteItemViewModel(Guid keyGuid, string actionName, string actionTitle, NoteData oldData, string callback)
           : base(keyGuid, actionName, actionTitle, oldData, callback)
        {
            NodeGuid = oldData.NodeGuid;
            Content = oldData.Content;
        }

        public NoteItemViewModel(string actionName, string actionTitle, Guid nodeGuid, string callback)
            : base(actionName, actionTitle, callback)
        {
            NodeGuid = nodeGuid;
        }

        public Guid NodeGuid { get; set; }
        [Required]
        [Display(Name = "批注内容")]
        public String Content { get; set; }
    }
}
