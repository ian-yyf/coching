using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Bll
{
    class AppConfigurtaion
    {
        public static IConfiguration Configuration { get; set; }
        static AppConfigurtaion()
        {
            var path = "thirdaccounts.release.json";
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = path, ReloadOnChange = true })
            .Build();
        }
    }
}
