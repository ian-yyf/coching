using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coching.Bll;
using Coching.Model.Front;
using Coching.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Public.Mvc;

namespace Coching.Web.Controllers
{
    public class OrganizationController : _Controller
    {
        public OrganizationController(CochingWork work)
            : base(work)
        {

        }

        public async Task<IActionResult> Index(string kind = null)
        {
            var token = this.getUserToken();
            var projects = await _work.getProjectsOfUser(token, token.ID, new ProjectCondition(), 4, 1);
            if (!projects.Success)
            {
                return Error(projects.Message);
            }

            var users = await _work.getRelatedUsers(token, token.ID);
            if (!users.Success)
            {
                return Error(users.Message);
            }

            return AutoView("Index", new OrganizationIndexViewModel(kind, projects.Body, users.Body));
        }

        public async Task<IActionResult> ActionLogs()
        {
            var token = this.getUserToken();
            var logs = await _work.getActionLogsOfUser(token, token.ID, PageSize, 1);
            if (!logs.Success)
            {
                return Error(logs.Message);
            }

            return AutoView("ActionLogs", new ActionLogsViewModel(logs.Body));
        }
    }
}