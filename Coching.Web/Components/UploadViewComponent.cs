using Api.Params;
using App.Utils;
using Coching.Bll;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Coching.Web.Components
{
    public class UploadViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int imageMaxCount, int imageWidth, int imageHeight, int imagePreviewWidth, int imagePreviewHeight, String uploaderName)
        {
            var url = SignTool.toApiUrl(ThirdAccounts.FileUrl + "/ImageUpload", new ImageUploadData(ThirdAccounts.FileAppId, imageMaxCount, imageWidth, imageHeight, imagePreviewWidth, imagePreviewHeight, uploaderName), ThirdAccounts.FileAppSecret);
            using var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var result = await client.DownloadStringTaskAsync(url);
            return View<String>(result);
        }
    }
}
