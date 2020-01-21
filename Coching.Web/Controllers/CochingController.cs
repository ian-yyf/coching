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
            var roots = await _work.getRootsOfProject(token, projectGuid, new NodeCondition(), PageSize, 1);
            if (!roots.Success)
            {
                return Error(roots.Message);
            }

            var partners = await _work.getPartnersOfProject(token, projectGuid, PartnerCondition.modify());
            if (!partners.Success)
            {
                return Error(partners.Message);
            }

            return AutoView("Index", new CochingViewModel(projectGuid, rootGuid, roots.Body.Items, partners.Body));
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

        public IActionResult AddNode(Guid projectGuid, Guid rootGuid, Guid parentGuid, string callback)
        {
            return AutoView("NodeItem", new NodeItemViewModel("AddNode", "添加节点", projectGuid, rootGuid, parentGuid, callback));
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
            var result = await _work.insertNode(token, new NodeData(model.ProjectGuid, model.RootGuid, model.ParentGuid, token.ID, model.Name, model.Description));
            if (!result.Success)
            {
                return Error(result.Message, AutoView("NodeItem", model));
            }

            model.Result = result.Body;
            return AutoView("NodeItem", model);
        }

        public async Task<IActionResult> ModifyNode(Guid id, string callback)
        {
            var item = await _work.getNode(this.getUserToken(), id);
            if (!item.Success)
            {
                return Error(item.Message);
            }

            return AutoView("NodeItem", new NodeItemViewModel(id, "ModifyNode", "修改节点", item.Body, callback));
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
            var result = await _work.modifyNode(this.getUserToken(), model.KeyGuid, oldData, new NodeData(oldData, model.Name, model.Description));
            if (!result.Success)
            {
                return Error(result.Message, AutoView("NodeItem", model));
            }

            model.Result = result.Body;
            return AutoView("NodeItem", model);
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
                var result = await _work.modifyNode(token, id, new NodeData() { Status = node.Body.Status }, new NodeData() { Status = status });
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
                var result = await _work.modifyNode(token, id, new NodeData() { WorkerGuid = node.Body.WorkerGuid }, new NodeData() { WorkerGuid = userGuid });
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> ModifyStartTime(Guid id, DateTime startTime)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var node = await _work.getNode(token, id);
                if (!node.Success)
                {
                    return Json(new Result<FNode>(false, null, node.Message));
                }
                var result = await _work.modifyNode(token, id, new NodeData() { StartTime = node.Body.StartTime }, new NodeData() { StartTime = startTime });
                return Json(result);
            });
        }

        [HttpPost]
        public async Task<IActionResult> ModifyEndTime(Guid id, DateTime endTime)
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                var node = await _work.getNode(token, id);
                if (!node.Success)
                {
                    return Json(new Result<FNode>(false, null, node.Message));
                }
                var result = await _work.modifyNode(token, id, new NodeData() { EndTime = node.Body.EndTime }, new NodeData() { EndTime = endTime });
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
                var result = await _work.modifyNode(token, id, new NodeData() { EstimatedManHour = node.Body.EstimatedManHour }, new NodeData() { EstimatedManHour = estimatedManHour });
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
            var result = await _work.insertNote(token, new NoteData(model.NodeGuid, token.ID, model.Content));
            if (!result.Success)
            {
                return Error(result.Message);
            }

            model.Result = result.Body;
            return AutoView("NoteItem", model);
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
    }
}