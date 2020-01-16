using Microsoft.AspNetCore.Mvc;
using Public.Mvc.Models;
using Public.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Coching.Web.Models
{
    public static class ModelUtils
    {
        public static string header(this IUrlHelper url, string src)
        {
            if (string.IsNullOrEmpty(src))
            {
                return url.Content("~/res/userHead.png");
            }
            return src;
        }
    }

    public class PopupItemViewModel<T, R> : ItemViewModel<T>
    {
        public PopupItemViewModel()
        {

        }

        public PopupItemViewModel(string actionName, string actionTitle, string callback)
            : base(actionName, actionTitle)
        {
            Callback = callback;
        }

        public PopupItemViewModel(Guid keyGuid, string actionName, string actionTitle, T oldData, string callback)
            : base(keyGuid, actionName, actionTitle, oldData, null)
        {
            Callback = callback;
        }

        public R Result { get; set; }
        public string Callback { get; set; }

        public bool Success
        {
            get
            {
                return Result != null;
            }
        }

        public string getResult()
        {
            return Result.jsonEncode();
        }
    }
}
