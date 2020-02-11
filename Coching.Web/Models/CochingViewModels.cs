using Coching.Model.Data;
using Coching.Model.Front;
using Public.Model.Front;
using Public.Mvc.Models;
using Public.Utils;
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

        public CochingViewModel(Guid myId, Guid projectGuid, Guid? rootGuid, FRoot[] roots, FPartner[] partners)
        {
            MyId = myId;
            ProjectGuid = projectGuid;
            RootGuid = rootGuid;
            Roots = roots;
            Partners = partners;
        }

        public Guid MyId { get; set; }
        public Guid ProjectGuid { get; set; }
        public Guid? RootGuid { get; set; }
        public FRoot[] Roots { get; set; }
        public FPartner[] Partners { get; set; }

        public string getPartners()
        {
            return Partners.jsonEncode();
        }
    }

    public class NodeItemViewModel : PopupItemViewModel<FNodeModify, FNodeModify>
    {
        public NodeItemViewModel()
        {

        }

        public NodeItemViewModel(Guid keyGuid, string actionName, string actionTitle, FNodeModify oldData, bool isAdmin, string callback)
           : base(keyGuid, actionName, actionTitle, oldData, callback)
        {
            ProjectGuid = oldData.ProjectGuid;
            ParentGuid = oldData.ParentGuid;
            RootGuid = oldData.RootGuid;
            Coching = oldData.Coching;
            Name = oldData.Name;
            Description = oldData.Description;
            HtmlDescription = oldData.HtmlDescription;
            Documents = oldData.Documents.Select(d => d.Document.Src).jsonEncode();
            IsAdmin = isAdmin;
        }

        public NodeItemViewModel(string actionName, string actionTitle, Guid projectGuid, Guid rootGuid, Guid parentGuid, bool isAdmin, string callback)
            : base(actionName, actionTitle, callback)
        {
            ProjectGuid = projectGuid;
            ParentGuid = parentGuid;
            RootGuid = rootGuid;
            IsAdmin = isAdmin;
        }

        public Guid ProjectGuid { get; set; }
        public Guid RootGuid { get; set; }
        public Guid ParentGuid { get; set; }
        [Display(Name = "考成项")]
        public bool Coching { get; set; }
        [Required]
        [Display(Name = "名称")]
        [StringLength(16)]
        public string Name { get; set; }
        [Display(Name = "详情")]
        public string Description { get; set; }
        public string HtmlDescription { get; set; }
        public string Documents { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class NodeDetailViewModel
    {
        public NodeDetailViewModel()
        {

        }

        public NodeDetailViewModel(FPartner[] partners, FNodeDetail data, FUser me, string notify)
        {
            Partners = partners;
            Data = data;
            Me = me;
            Notify = notify;
        }

        public FPartner[] Partners { get; set; }
        public FNodeDetail Data { get; set; }
        public FUser Me { get; set; }
        public string Notify { get; set; }

        public FPartner PartnerMe
        {
            get
            {
                return Partners.First(p => p.UserGuid == Me.ID);
            }
        }

        public List<KeyValuePair<int, string>> StatusList
        {
            get
            {
                return Public.Mvc.ExtendUtils.toKeyValues<NodeStatus>();
            }
        }
    }

    public class NoteItemViewModel : PopupItemViewModel<FNote, FNote>
    {
        public NoteItemViewModel()
        {

        }

        public NoteItemViewModel(Guid keyGuid, string actionName, string actionTitle, FNote oldData, string callback)
           : base(keyGuid, actionName, actionTitle, oldData, callback)
        {
            NodeGuid = oldData.NodeGuid;
            Content = oldData.Content;
            HtmlContent = oldData.HtmlContent;
            Documents = oldData.Documents.Select(d => d.Document.Src).jsonEncode();
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
        [Required]
        public String HtmlContent { get; set; }
        public string Documents { get; set; }
    }
}
