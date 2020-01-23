using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Coching.Bll;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coching.Web.Controllers
{
    public class FileController : _Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileController(CochingWork work, IWebHostEnvironment hostEnvironment)
            : base(work)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Upload([FromForm]IFormCollection formData)
        {
            // 存储路径（绝对路径）
            string imgPath = _hostEnvironment.WebRootPath;
            imgPath = imgPath.Replace("\\", "/");

            if (!imgPath.EndsWith("/"))
            {
                imgPath += "/";
            }

            var code = new { success = 0, size = 1, type = 2 };
            var fileTypes = new List<string> { "png", "jpg", "jpeg", "bmp", "gif" };
            var result = new List<string>();

            // 设置上传目录
            var uploadDir = "upload_files/" + DateTime.Now.ToString("yyMMdd");
            var httpPath = "/" + uploadDir;
            imgPath += uploadDir;

            for (int i = 0; i < formData.Files.Count; i++)
            {
                var file = formData.Files[i];
                if (file.Length > 1024 * 1024)
                {
                    return Ok(new { errno = code.size });
                }

                var fileExt = Path.GetExtension(file.FileName);
                if (String.IsNullOrEmpty(fileExt) || !fileTypes.Exists(f => f.ToLower() == fileExt.Substring(1).ToLower()))
                {
                    return Ok(new { errno = code.type });
                }
            }

            for (int i = 0; i < formData.Files.Count; i++)
            {
                var file = formData.Files[i];

                if (file.Length < 1)
                {
                    continue;
                }

                var fileExt = Path.GetExtension(file.FileName);
                string fullFileName = Guid.NewGuid().ToString("N") + fileExt;

                if (!Directory.Exists(imgPath))
                {
                    Directory.CreateDirectory(imgPath);
                }

                using (var stream = new FileStream(imgPath + "/" + fullFileName, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                result.Add(httpPath + "/" + fullFileName);
            }

            return Ok(new { errno = code.success, data = result });
        }
    }
}