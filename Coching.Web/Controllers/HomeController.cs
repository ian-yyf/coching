using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coching.Web.Models;
using Coching.Bll;
using Public.Mvc;
using Coching.Model.Data;
using Coching.Model.Front;
using Public.Containers;

namespace Coching.Web.Controllers
{
    public class HomeController : _Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(CochingWork work, ILogger<HomeController> logger)
            : base(work)
        {
            _logger = logger;
        }

        protected override bool loginRequired()
        {
            return true;
        }

        protected override List<string> loginRequiredExceptional()
        {
            return new List<string>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Coching()
        {
            var token = this.getUserToken();
            var roots = await _work.getUserRoots(token, token.ID, new NodeCondition(), PageSize, 1);
            if (!roots.Success)
            {
                return Error(roots.Message, AutoView("Error", new ErrorViewModel()));
            }
            return AutoView("Coching", new CochingViewModel(roots.Body.Items));
        }

        private dynamic[] toTreeData(FNode[] items)
        {
            return items.Select(i =>
            {
                return new
                {
                    id = i.ID,
                    name = i.getLable(),
                    children = toTreeData(i.Children ?? new FNode[] { }),
                    collapsed = false
                };
            }).ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> Tree(Guid id)
        {
            return await JsonActionAsync(async () =>
            {
                var result = await _work.getTree(this.getUserToken(), id);
                if (!result.Success)
                {
                    return Json(result);
                }

                return Json(new Result<dynamic>(toTreeData(new FNode[] { result.Body })));
            });
        }

        public IActionResult AddNode(Guid rootGuid, Guid parentGuid, string callback)
        {
            return AutoView("NodeItem", new NodeItemModel("AddNode", "添加节点", rootGuid, parentGuid, callback));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNode(NodeItemModel model)
        {
            if (!ModelState.IsValid)
            {
                return AutoView("NodeItem", model);
            }

            var token = this.getUserToken();
            var result = await _work.insertNode(token, new NodeData(model.RootGuid, model.ParentGuid, token.ID, model.Name, model.Description, model.Status));
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
                return Error(item.Message, AutoView("Error", new ErrorViewModel()));
            }

            return AutoView("NodeItem", new NodeItemModel(id, "ModifyNode", "修改节点", item.Body, callback));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyNode(NodeItemModel model)
        {
            if (!ModelState.IsValid)
            {
                return AutoView("NodeItem", model);
            }

            var oldData = model.OldData;
            var result = await _work.modifyNode(this.getUserToken(), model.KeyGuid, oldData, new NodeData(oldData, model.Name, model.Description, model.Status));
            if (!result.Success)
            {
                return Error(result.Message, AutoView("NodeItem", model));
            }

            model.Result = result.Body;
            return AutoView("NodeItem", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
