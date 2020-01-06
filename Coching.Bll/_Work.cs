using App.Utils;
using Coching.Dal;
using Public.Bll;
using Public.Containers;
using Public.Dal;
using Public.Model.Data;
using Public.Model.Front;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coching.Bll
{
    public class _Work : Work
    {
        protected readonly CochingModels _models;

        public _Work(CochingModels models)
        {
            _models = models;
        }

        public override Models createModels()
        {
            return _models;
        }

        public override AppAccount getCaptchaAccount()
        {
            return new AppAccount(ThirdAccounts.CaptchaUrl, ThirdAccounts.CaptchaAppId, ThirdAccounts.CaptchaAppSecret);
        }

        public override AppAccount getQrCodeAccount(string authorizerAppid)
        {
            if (authorizerAppid == null)
            {
                return new AppAccount(ThirdAccounts.QrCodeUrl, ThirdAccounts.QrCodeAppId, ThirdAccounts.QrCodeAppSecret);
            }
            else
            {
                return new AppAccount(ThirdAccounts.AuthorizerQrCodeUrl, ThirdAccounts.AuthorizerQrCodeAppId, ThirdAccounts.AuthorizerQrCodeAppSecret);
            }
        }

        public override AppAccount getShortMessageAccount()
        {
            return new AppAccount(ThirdAccounts.ShortMessageUrl, ThirdAccounts.ShortMessageAppId, ThirdAccounts.ShortMessageAppSecret);
        }

        public override string getShortMessageSign()
        {
            return "与你客服";
        }

        public override string getShortMessageTemplate()
        {
            return "SMS_152175081";
        }

        public override AppAccount getWechatAccount(string authorizerAppid)
        {
            if (authorizerAppid == null)
            {
                return new AppAccount(ThirdAccounts.WechatUrl, ThirdAccounts.WechatAppId, ThirdAccounts.WechatAppSecret);
            }
            else
            {
                return new AppAccount(ThirdAccounts.AuthorizerWechatUrl, ThirdAccounts.AuthorizerWechatAppId, ThirdAccounts.AuthorizerWechatAppSecret);
            }
        }

        public async Task<bool> checkPermission(FUserToken token, PermissionKind kind)
        {
            return await checkPermission(token, (int)kind);
        }

        public async override Task<Result<FPermission>> insertPermission(FUserToken token, PermissionData data)
        {
            return await insertPermission(token, data, u =>
            {
                return checkPermission(token, PermissionKind.权限管理);
            });
        }

        public async override Task<Result<FPermission>> modifyPermission(FUserToken token, Guid permissionGuid, PermissionData oldData, PermissionData newData)
        {
            return await modifyPermission(token, permissionGuid, oldData, newData, u =>
            {
                return checkPermission(token, PermissionKind.权限管理);
            });
        }

        public async override Task<Result<bool>> deletePermission(FUserToken token, Guid permissionGuid)
        {
            return await deletePermission(token, permissionGuid, u =>
            {
                return checkPermission(token, PermissionKind.权限管理);
            });
        }

        public async override Task<Result<FPermission[]>> getUserPermissions(FUserToken token, Guid userGuid)
        {
            return await getUserPermissions(token, userGuid, u =>
            {
                return checkPermission(token, PermissionKind.用户权限管理);
            });
        }

        public async override Task<Result<bool>> setUserPermissions(FUserToken token, Guid userGuid, Guid[] permissions)
        {
            return await setUserPermissions(token, userGuid, permissions, u =>
            {
                return checkPermission(token, PermissionKind.用户权限管理);
            });
        }
    }
}
