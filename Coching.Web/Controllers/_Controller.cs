using Coching.Bll;
using Coching.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Public.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Coching.Web.Controllers
{
    public class _Controller : _PageController<PermissionKind, PermissionAction>
    {
        protected readonly CochingWork _work;
        public override string SessionRoot
        {
            get
            {
                return "coching";
            }
        }

        public _Controller(CochingWork work)
        {
            _work = work;
        }

        protected IActionResult Error(string message)
        {
            return View(new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public override async Task<string> toAuthorizerAppid(Guid authorizerAppGuid)
        {
            return await Task.FromResult<string>(null);
        }

        public override IActionResult ToLogin(String authorizerAppid, String returnUrl)
        {
            return RedirectToAction("Login", "Auth", new { authorizerAppid, returnUrl });
        }

        public override IActionResult ToSilentLogin(String authorizerAppid, String returnUrl)
        {
            return RedirectToAction("SilentLogin", "Auth", new { authorizerAppid, returnUrl });
        }

        public override IActionResult ToLoginOrDefault(String authorizerAppid, String returnUrl)
        {
            return RedirectToAction("LoginOrDefault", "Auth", new { authorizerAppid, returnUrl });
        }
    }
}
