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
using Microsoft.AspNetCore.Hosting;

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

        public IActionResult Index()
        {
            if (IsMobileDevice)
            {
                return RedirectToAction("Index", "Project");
            }
            else
            {
                return RedirectToAction("Index", "Organization");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public async Task<IActionResult> Test([FromServices]IWebHostEnvironment hostEnvironment)
        //{
        //    var result = await _work.downlaodUserHeaders(hostEnvironment.WebRootPath, "headers", file => file.StartsWith("http://thirdwx.qlogo.cn"));
        //    return Json(result);
        //}
    }
}
