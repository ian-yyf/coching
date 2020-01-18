using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coching.Bll;
using Coching.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Public.Model.Data;
using Public.Mvc;

namespace Coching.Web.Controllers
{
    public class UserController : _Controller
    {
        public UserController(CochingWork work)
            : base(work)
        {

        }

        [HttpPost]
        public async Task<ActionResult> GetCurrentUser()
        {
            return await JsonActionAsync(async () =>
            {
                var token = this.getUserToken();
                return Json(await _work.getUser(token, token.ID));
            });
        }

        public async Task<IActionResult> Modify(string callback)
        {
            var token = this.getUserToken();
            var user = await _work.getUser(token, token.ID);

            if (!user.Success)
            {
                return Error(user.Message, AutoView("Error", new ErrorViewModel()));
            }

            return AutoView("UserItem", new UserItemViewModel(user.Body.ID, "Modify", "修改资料", user.Body, callback));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modify(UserItemViewModel model)
        {
            var token = this.getUserToken();
            var oldData = model.OldData;
            var result = await _work.modifyUser(token, model.KeyGuid, oldData, new UserData(oldData) { Name = model.Name, Header = model.Header }, null, null);
            if (!result.Success)
            {
                return Error(result.Message, AutoView("UserItem", model));
            }

            model.Result = result.Body;
            return AutoView("UserItem", model);
        }
    }
}