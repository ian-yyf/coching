﻿using Coching.Model.Data;
using Coching.Model.Front;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coching.Web.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel()
        {

        }

        public ProjectViewModel(FProject[] projects)
        {
            Projects = projects;
        }

        public FProject[] Projects { get; set; }
    }

    public class ProjectItemViewModel : PopupItemViewModel<ProjectData, FProject>
    {
        public ProjectItemViewModel()
        {

        }

        public ProjectItemViewModel(Guid keyGuid, string actionName, string actionTitle, ProjectData oldData, string callback)
           : base(keyGuid, actionName, actionTitle, oldData, callback)
        {
            Name = oldData.Name;
            Header = oldData.Header;
            Description = oldData.Description;
        }

        public ProjectItemViewModel(string actionName, string actionTitle, string callback)
            : base(actionName, actionTitle, callback)
        {

        }

        [Required]
        [StringLength(32)]
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "LOGO")]
        public string Header { get; set; }
        [Display(Name = "详情")]
        public string Description { get; set; }
    }
}