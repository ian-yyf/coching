﻿using Coching.Model.Front;
using Public.Model.Front;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coching.Web.Models
{
    public class OrganizationIndexViewModel
    {
        public OrganizationIndexViewModel()
        {

        }

        public OrganizationIndexViewModel(FProject[] projects, FUser[] users)
        {
            Projects = projects;
            Users = users;
        }

        public FProject[] Projects { get; set; }
        public FUser[] Users { get; set; }
    }

    public class ActionLogsViewModel
    {
        public ActionLogsViewModel()
        {

        }

        public ActionLogsViewModel(FActionLog[] logs)
        {
            Logs = logs;
        }

        public FActionLog[] Logs { get; set; }
    }
}
