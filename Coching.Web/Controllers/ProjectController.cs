using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coching.Bll;
using Coching.Model.Data;
using Coching.Model.Front;
using Coching.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Public.Model.Front;
using Public.Mvc;

namespace Coching.Web.Controllers
{
    public class ProjectController : _Controller
    {
        public ProjectController(CochingWork work)
            : base(work)
        {

        }

        public async Task<IActionResult> Index()
        {
            var token = this.getUserToken();
            var result = await _work.getProjectsOfUser(token, token.ID, new ProjectCondition());
            if (!result.Success)
            {
                return Error(result.Message);
            }
            return AutoView("Index", new ProjectViewModel(result.Body));
        }

        public IActionResult Add(string callback)
        {
            return AutoView("ProjectItem", new ProjectItemViewModel("Add", "添加新项目", callback));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProjectItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return AutoView("ProjectItem", model);
            }

            var token = this.getUserToken();
            var result = await _work.insertProject(token, new ProjectData(token.ID, model.Name, model.Header, model.Description));
            if (!result.Success)
            {
                return Error(result.Message, AutoView("ProjectItem", model));
            }

            model.Result = result.Body;
            return AutoView("ProjectItem", model);
        }

        public async Task<IActionResult> Modify(Guid id, string callback)
        {
            var token = this.getUserToken();
            var project = await _work.getProject(token, id);

            if (!project.Success)
            {
                return Error(project.Message);
            }

            return AutoView("ProjectItem", new ProjectItemViewModel(project.Body.ID, "Modify", "修改项目", project.Body, callback));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(ProjectItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return AutoView("ProjectItem", model);
            }

            var token = this.getUserToken();
            var oldData = model.OldData;
            var result = await _work.modifyProject(token, model.KeyGuid, oldData, new ProjectData(oldData, model.Name, model.Header, model.Description));
            if (!result.Success)
            {
                return Error(result.Message, AutoView("ProjectItem", model));
            }

            model.Result = result.Body;
            return AutoView("ProjectItem", model);
        }

        public async Task<IActionResult> Partners(Guid projectGuid, string notify, string key)
        {
            FUser[] users = null;
            if (key != null)
            {
                var us = await _work.getUsers(key);
                if (!us.Success)
                {
                    return Error(us.Message);
                }
                users = us.Body;
            }

            var result = await _work.getPartnersOfProject(this.getUserToken(), projectGuid, new PartnerCondition());
            if (!result.Success)
            {
                return Error(result.Message);
            }

            return AutoView("Partners", new PartnersViewModel(projectGuid, notify, key, users, result.Body));
        }

        [HttpPost]
        public async Task<IActionResult> SetPartner(Guid projectGuid, Guid userGuid, PartnerRole role)
        {
            return await JsonActionAsync(async () =>
            {
                var result = await _work.setPartner(this.getUserToken(), projectGuid, userGuid, role);
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePartner(Guid id)
        {
            return await JsonActionAsync(async () =>
            {
                var result = await _work.deletePartner(this.getUserToken(), id);
                return Json(result);
            });
        }
    }
}