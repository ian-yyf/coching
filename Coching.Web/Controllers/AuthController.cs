using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Utils;
using Coching.Bll;
using Coching.Model.Data;
using Coching.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Public.Bll;
using Public.Mvc;

namespace Coching.Web.Controllers
{
    public class AuthController : _AuthController<PermissionKind, PermissionAction>
    {
        private readonly CochingWork _work;
        public override string Name
        {
            get
            {
                return "考成簿";
            }
        }

        public override string SessionRoot
        {
            get
            {
                return "coching";
            }
        }

        public override AppAccount AsApiAppAcount
        {
            get
            {
                return new AppAccount(Host, ThirdAccounts.AsApiAppId, ThirdAccounts.AsApiAppSecret);
            }
        }

        public override int[] AsApiCategoryAccountKinds
        {
            get
            {
                return new int[] { (int)CategoryAccountKind.余额 };
            }
        }

        public override string ViewLayout
        {
            get
            {
                return "~/Views/Shared/_Layout.cshtml";
            }
        }

        public override string MobileViewLayout
        {
            get
            {
                return "~/Views/Shared/_LayoutMobile.cshtml";
            }
        }

        public AuthController(CochingWork work)
        {
            _work = work;
        }

        public override List<FunctionTree<PermissionKind, PermissionAction>> getTotalFunctions()
        {
            return new List<FunctionTree<PermissionKind, PermissionAction>>
            {
                {
                    PermissionAction.首页.ToString(),
                    "layui-icon-home",
                    Url.Action("Index", "Admin"),
                    PermissionKind.nil,
                    PermissionAction.首页
                },
                {
                    "权限管理",
                    "layui-icon-vercode",
                    null,
                    new List<FunctionUrl<PermissionKind, PermissionAction>>
                    {
                        { PermissionAction.权限管理.ToString(), Url.Action("Permissions", "Permission"), PermissionKind.权限管理, PermissionAction.权限管理 },
                        { PermissionAction.用户权限管理.ToString(), Url.Action("PermissionUserConfig", "Permission"), PermissionKind.用户权限管理, PermissionAction.用户权限管理 },
                    }
                },
            };
        }

        public override AppAccount getLoginAppAccount(string authorizerAppid)
        {
            if (authorizerAppid == null)
            {
                return new AppAccount(ThirdAccounts.ThirdLoginUrl, ThirdAccounts.ThirdLoginAppId, ThirdAccounts.ThirdLoginAppSecret);
            }
            else
            {
                return new AppAccount(ThirdAccounts.AuthorizerThirdLoginUrl, ThirdAccounts.AuthorizerThirdLoginAppId, ThirdAccounts.AuthorizerThirdLoginAppSecret);
            }
        }

        public override Work createWork()
        {
            return _work;
        }

        public override int getDefaultUserKind()
        {
            return (int)KindKey.普通用户;
        }
    }
}