using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coching.Bll;
using Coching.Model.Data;
using Coching.Model.Front;
using Coching.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Public.Containers;
using Public.Mvc;
using Public.Utils;

namespace Coching.Web.Controllers
{
    public class CochingController : _Controller
    {
        public CochingController(CochingWork work)
            : base(work)
        {

        }

        public async Task<IActionResult> Index(Guid projectGuid, Guid? rootGuid)
        {
            var token = this.getUserToken();

            var project = await _work.getProject(token, projectGuid);
            if (!project.Success)
            {
                return Error(project.Message);
            }

            var roots = await _work.getRootsOfProject(token, projectGuid, PageSize, 1);
            if (!roots.Success)
            {
                return Error(roots.Message);
            }

            var partners = await _work.getPartnersOfProject(token, projectGuid, PartnerCondition.modify());
            if (!partners.Success)
            {
                return Error(partners.Message);
            }

            return AutoView("Index", new CochingViewModel(token.ID, projectGuid, rootGuid, project.Body, roots.Body.Items, partners.Body));
        }

        [HttpPost]
        public async Task<IActionResult> Root(Guid id)
        {
            return await JsonActionAsync(async () =>
            {
                var result = await _work.getRoot(this.getUserToken(), id);
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Tree(Guid id)
        {
            return await JsonActionAsync(async () =>
            {
                var result = await _work.getTree(this.getUserToken(), id);
                return Json(result);
            });
        }

        public async Task<IActionResult> AddNode(Guid projectGuid, Guid rootGuid, Guid parentGuid, string callback)
        {
            var token = this.getUserToken();
            var partner = await _work.getPartnerOfProject(token, projectGuid, token.ID);
            if (!partner.Success)
            {
                return Error(partner.Message);
            }
            return AutoView("NodeItem", new NodeItemViewModel("AddNode", "添加节点", projectGuid, rootGuid, parentGuid, partner.Body.IsAdmin, callback));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNode(NodeItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return AutoView("NodeItem", model);
            }

            var token = this.getUserToken();
            var data = new NodeData(model.ProjectGuid, model.RootGuid, model.ParentGuid, token.ID, model.Name, model.Description, model.HtmlDescription, model.Coching);
            var result = await _work.insertNode(token, data, model.Documents?.jsonDecode<string[]>());
            if (!result.Success)
            {
                return Error(result.Message, AutoView("NodeItem", model));
            }

            model.Result = result.Body;
            return AutoView("NodeItem", model);
        }

        public async Task<IActionResult> ModifyNode(Guid id, string callback)
        {
            var token = this.getUserToken();
            var item = await _work.getNodeModify(token, id);
            if (!item.Success)
            {
                return Error(item.Message);
            }

            var partner = await _work.getPartnerOfProject(token, item.Body.ProjectGuid, token.ID);
            if (!partner.Success)
            {
                return Error(partner.Message);
            }

            return AutoView("NodeItem", new NodeItemViewModel(id, "ModifyNode", "修改节点", item.Body, partner.Body.IsAdmin, callback));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyNode(NodeItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return AutoView("NodeItem", model);
            }

            var oldData = model.OldData;
            var newData = new NodeData(oldData, model.Name, model.Description, model.HtmlDescription, model.Coching);
            var result = await _work.modifyNode(this.getUserToken(), model.KeyGuid, new NodeData(oldData), newData, oldData.Documents, model.Documents?.jsonDecode<string[]>());
            if (!result.Success)
            {
                return Error(result.Message, AutoView("NodeItem", model));
            }

            model.Result = result.Body;
            return AutoView("NodeItem", model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNode(Guid id)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var result = await _work.deleteNode(token, id);
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> ModifyStatus(Guid id, int status)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var node = await _work.getNode(token, id);
                if (!node.Success)
                {
                    return Json(new Result<FNode>(false, null, node.Message));
                }
                var result = await _work.modifyNode(token, id, new NodeData() { Status = node.Body.Status }, new NodeData() { Status = status }, null, null);
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> ModifyWorker(Guid id, Guid userGuid)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var node = await _work.getNode(token, id);
                if (!node.Success)
                {
                    return Json(new Result<FNode>(false, null, node.Message));
                }
                var result = await _work.modifyNode(token, id, new NodeData() { WorkerGuid = node.Body.WorkerGuid }, new NodeData() { WorkerGuid = userGuid }, null, null);
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> ModifyStartTime(Guid id, DateTime? startTime)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var node = await _work.getNode(token, id);
                if (!node.Success)
                {
                    return Json(new Result<FNode>(false, null, node.Message));
                }
                var result = await _work.modifyNode(token, id, new NodeData() { StartTime = node.Body.StartTime }, new NodeData() { StartTime = startTime }, null, null);
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> ModifyEndTime(Guid id, DateTime? endTime)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var node = await _work.getNode(token, id);
                if (!node.Success)
                {
                    return Json(new Result<FNode>(false, null, node.Message));
                }
                var result = await _work.modifyNode(token, id, new NodeData() { EndTime = node.Body.EndTime }, new NodeData() { EndTime = endTime }, null, null);
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> ModifyEstimatedManHour(Guid id, decimal estimatedManHour)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var node = await _work.getNode(token, id);
                if (!node.Success)
                {
                    return Json(new Result<FNode>(false, null, node.Message));
                }
                var result = await _work.modifyNode(token, id, new NodeData() { EstimatedManHour = node.Body.EstimatedManHour }, new NodeData() { EstimatedManHour = estimatedManHour }, null, null);
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> ModifyActualManHour(Guid id, decimal actualManHour)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var node = await _work.getNode(token, id);
                if (!node.Success)
                {
                    return Json(new Result<FNode>(false, null, node.Message));
                }
                var result = await _work.modifyNode(token, id, new NodeData() { ActualManHour = node.Body.ActualManHour }, new NodeData() { ActualManHour = actualManHour }, null, null);
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Offer(Guid id, decimal estimatedManHour)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var result = await _work.offerToNode(token, id, token.ID, estimatedManHour);
                return Json(result);
            });
        }

        public async Task<IActionResult> NodeDetail(Guid id, string notify)
        {
            var token = this.getUserToken();
            var detail = await _work.getNodeDetail(token, id);
            if (!detail.Success)
            {
                return Error(detail.Message);
            }
            var me = await _work.getUser(token, token.ID);
            if (!me.Success)
            {
                return Error(me.Message);
            }
            var partners = await _work.getPartnersOfProject(token, detail.Body.Node.ProjectGuid, PartnerCondition.modify());
            if (!partners.Success)
            {
                return Error(partners.Message);
            }

            return AutoView("NodeDetail", new NodeDetailViewModel(partners.Body, detail.Body, me.Body, notify));
        }

        public IActionResult AddNote(Guid nodeGuid, string callback)
        {
            return AutoView("NoteItem", new NoteItemViewModel("AddNote", "添加批注", nodeGuid, callback));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNote(NoteItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return AutoView("NoteItem", model);
            }

            var token = this.getUserToken();
            var result = await _work.insertNote(token, new NoteData(model.NodeGuid, token.ID, model.Content, model.HtmlContent), model.Documents?.jsonDecode<string[]>());
            if (!result.Success)
            {
                return Error(result.Message, AutoView("NoteItem", model));
            }

            model.Result = result.Body;
            return AutoView("NoteItem", model);
        }

        public async Task<IActionResult> ModifyNote(Guid id, string callback)
        {
            var token = this.getUserToken();
            var item = await _work.getNote(token, id);
            if (!item.Success)
            {
                return Error(item.Message);
            }

            return AutoView("NoteItem", new NoteItemViewModel(id, "ModifyNote", "修改批注", item.Body, callback));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyNote(NoteItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return AutoView("NoteItem", model);
            }

            var token = this.getUserToken();
            var oldData = model.OldData;
            var newData = new NoteData(oldData, model.Content, model.HtmlContent);
            var result = await _work.modifyNote(token, model.KeyGuid, new NoteData(oldData), newData, oldData.Documents, model.Documents?.jsonDecode<string[]>());
            if (!result.Success)
            {
                return Error(result.Message, AutoView("NoteItem", model));
            }

            model.Result = result.Body;
            return AutoView("NoteItem", model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var result = await _work.deleteNote(token, id);
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChartsData(string ids)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var charts = await _work.charts(token, ids.Split(',').Select(id => Guid.Parse(id)).ToArray());
                return Json(charts);
            });
        }

        public async Task<IActionResult> Charts(string ids)
        {
            var token = this.getUserToken();

            var projects = await _work.getProjectsOfUser(token, token.ID, new ProjectCondition(), PageSize, 1);
            if (!projects.Success)
            {
                return Error(projects.Message);
            }

            return AutoView("Charts", new ChartsViewModel(ids, projects.Body));
        }

        [HttpPost]
        public async Task<IActionResult> CalcNodeTime(Guid id)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var time = await _work.calcNodeTime(token, id);
                return Json(time);
            });
        }

        public async Task<IActionResult> Nodes(NodeCondition model)
        {
            var token = this.getUserToken();
            var nodes = await _work.getNodes(token, token.ID, model, PageSize, 1);
            if (!nodes.Success)
            {
                return Error(nodes.Message);
            }
            return AutoView("Nodes", new NodesViewModel(nodes.Body));
        }
    }
}