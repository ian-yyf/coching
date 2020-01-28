using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Bll
{
    public static class ThirdAccounts
    {
        public static String AsApiAppId => AppConfigurtaion.Configuration["AsApiAppId"];
        public static String AsApiAppSecret => AppConfigurtaion.Configuration["AsApiAppSecret"];
        public static String AuthorizerThirdLoginUrl => AppConfigurtaion.Configuration["AuthorizerThirdLoginUrl"];
        public static String AuthorizerThirdLoginAppId => AppConfigurtaion.Configuration["AuthorizerThirdLoginAppId"];
        public static String AuthorizerThirdLoginAppSecret => AppConfigurtaion.Configuration["AuthorizerThirdLoginAppSecret"];
        public static String ThirdLoginUrl => AppConfigurtaion.Configuration["ThirdLoginUrl"];
        public static String ThirdLoginAppId => AppConfigurtaion.Configuration["ThirdLoginAppId"];
        public static String ThirdLoginAppSecret => AppConfigurtaion.Configuration["ThirdLoginAppSecret"];
        public static String CaptchaUrl => AppConfigurtaion.Configuration["CaptchaUrl"];
        public static String CaptchaAppId => AppConfigurtaion.Configuration["CaptchaAppId"];
        public static String CaptchaAppSecret => AppConfigurtaion.Configuration["CaptchaAppSecret"];
        public static String ShortMessageUrl => AppConfigurtaion.Configuration["ShortMessageUrl"];
        public static String ShortMessageAppId => AppConfigurtaion.Configuration["ShortMessageAppId"];
        public static String ShortMessageAppSecret => AppConfigurtaion.Configuration["ShortMessageAppSecret"];
        public static String ShortMessageSign => AppConfigurtaion.Configuration["ShortMessageSign"];
        public static String ShortMessageTemplate => AppConfigurtaion.Configuration["ShortMessageTemplate"];
        public static String PayUrl => AppConfigurtaion.Configuration["PayUrl"];
        public static String EnterprisePayUrl => AppConfigurtaion.Configuration["EnterprisePayUrl"];
        public static String PayAppId => AppConfigurtaion.Configuration["PayAppId"];
        public static String PayAppSecret => AppConfigurtaion.Configuration["PayAppSecret"];
        public static String AuthorizerQrCodeUrl => AppConfigurtaion.Configuration["AuthorizerQrCodeUrl"];
        public static String AuthorizerQrCodeAppId => AppConfigurtaion.Configuration["AuthorizerQrCodeAppId"];
        public static String AuthorizerQrCodeAppSecret => AppConfigurtaion.Configuration["AuthorizerQrCodeAppSecret"];
        public static String QrCodeUrl => AppConfigurtaion.Configuration["QrCodeUrl"];
        public static String QrCodeAppId => AppConfigurtaion.Configuration["QrCodeAppId"];
        public static String QrCodeAppSecret => AppConfigurtaion.Configuration["QrCodeAppSecret"];
        public static String AuthorizerWechatUrl => AppConfigurtaion.Configuration["AuthorizerWechatUrl"];
        public static String AuthorizerWechatAppId => AppConfigurtaion.Configuration["AuthorizerWechatAppId"];
        public static String AuthorizerWechatAppSecret => AppConfigurtaion.Configuration["AuthorizerWechatAppSecret"];
        public static String WechatUrl => AppConfigurtaion.Configuration["WechatUrl"];
        public static String WechatAppId => AppConfigurtaion.Configuration["WechatAppId"];
        public static String WechatAppSecret => AppConfigurtaion.Configuration["WechatAppSecret"];
        public static String EventAppId => AppConfigurtaion.Configuration["EventAppId"];
        public static String EventAppSecret => AppConfigurtaion.Configuration["EventAppSecret"];
        public static String PlatformEventAppId => AppConfigurtaion.Configuration["PlatformEventAppId"];
        public static String PlatformEventAppSecret => AppConfigurtaion.Configuration["PlatformEventAppSecret"];
        public static String FileUrl => AppConfigurtaion.Configuration["FileUrl"];
        public static String FileAppId => AppConfigurtaion.Configuration["FileAppId"];
        public static String FileAppSecret => AppConfigurtaion.Configuration["FileAppSecret"];
        public static String ReverseGeocodingUrl => AppConfigurtaion.Configuration["ReverseGeocodingUrl"];
        public static String MapAppId => AppConfigurtaion.Configuration["MapAppId"];
        public static String MapAppSecret => AppConfigurtaion.Configuration["MapAppSecret"];
        public static String AuthorizeAppId => AppConfigurtaion.Configuration["AuthorizeAppId"];
        public static String AuthorizeAppSecret => AppConfigurtaion.Configuration["AuthorizeAppSecret"];
        public static String AuthorizeUrl => AppConfigurtaion.Configuration["AuthorizeUrl"];
    }
}
