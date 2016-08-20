using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PersonalFinance.Common.Model;
using PersonalFinance.Common.Providers;
using PersonalFinance.PublicWeb.Models;

namespace PersonalFinance.PublicWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVersionProvider _versionProvider;
        private readonly IConfigurationManagerProvider _configurationManagerProvider;

        public HomeController(
            IVersionProvider versionProvider,
            IConfigurationManagerProvider configurationManagerProvider
        )
        {
            _versionProvider = versionProvider;
            _configurationManagerProvider = configurationManagerProvider;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.ConfigSettings = await LoadSettingsAsync();
            return View();
        }

        private Task<PublicSettings> LoadSettingsAsync()
        {
            var output = new PublicSettings();
            output.BuildVersion = _versionProvider.BuildVersion;
            output.Environment = _configurationManagerProvider.GetConfigValue("Environment");
            output.AuthClientId = Constants.AuthClientId;
            return Task.Run(() => output);
        }
    }
}