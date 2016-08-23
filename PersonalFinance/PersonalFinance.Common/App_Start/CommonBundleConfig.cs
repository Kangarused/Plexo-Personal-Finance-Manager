using System.Web.Hosting;
using System.Web.Optimization;
using System.Web.Routing;
using PersonalFinance.Common;
using PersonalFinance.Common.Utils.EmbeddedResourcesUtils;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(CommonBundleConfig), "Start")]

namespace PersonalFinance.Common
{
   
    public class CommonBundleConfig
    {
        public const string SiteCommonScripts = "~/PersonalFinanceCommon/Embedded/Js";
        public const string SiteCommonAngularSettings = "~/PersonalFinanceCommon/Embedded/As";
        public const string SiteCommonStyles = "~/PersonalFinanceCommon/Embedded/Styles";

        public static void Start()
        {
            ConfigureRoutes();
            ConfigureBundles();
        }

        private static void ConfigureBundles()
        {
            //used to debug issues with resource embedding, uncomment bellow and inpect test to see what is embedded and the resource name
            //var test = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

            BundleTable.VirtualPathProvider = new EmbeddedVirtualPathProvider(HostingEnvironment.VirtualPathProvider);
            BundleTable.Bundles.Add(new ScriptBundle(SiteCommonScripts)
                .Include(
                    "~/PersonalFinanceCommon/Embedded/scripts/jquery/jquery-1.11.3.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/angular/angular.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/angular-case/angular-case.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/angular-permission/angular-permission.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/angular-ui-router/angular-ui-router.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/angular/angular-sanitize.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/angular-pubsub/angular-pubsub.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/angular/angular-cookies.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/angular-validation/angular-validation.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/angular-validation/angular-validation-schema.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/angular-breadcrumb/angular-breadcrumb.js",
                    
                    "~/PersonalFinanceCommon/Embedded/scripts/smart-table/smart-table.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/bootstrap/bootstrap.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/angular-bootstrap/ui-bootstrap-tpls-1.3.3.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/custom-angular/show-when-loading.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/custom-angular/track-changes.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/custom-angular/ng-include-replace.js",
                   
                    "~/PersonalFinanceCommon/Embedded/scripts/moment/moment.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/linqjs/linq.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/utils/email-addresses.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.min.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/bootstrap-datetimepicker/bootstrap-datetimepicker-directive.js",

                    "~/PersonalFinanceCommon/Embedded/scripts/smart-table/smart-table.js",
                    "~/PersonalFinanceCommon/Embedded/scripts/smart-table/st-server-pagination.js"
            ));

            BundleTable.Bundles.Add(new ScriptBundle(SiteCommonAngularSettings)
                .Include(
                    "~/PersonalFinanceCommon/Embedded/scripts/common-angular-settings/angularCommonInitSettings.js"));


            BundleTable.Bundles.Add(new StyleBundle(SiteCommonStyles)
               .Include(
                    "~/PersonalFinanceCommon/Embedded/styles/bootstrap-datetimepicker/bootstrap-datetimepicker.css"
                )
           );
        }

        private static void ConfigureRoutes()
        {
            RouteTable.Routes.Insert(0,
                new Route("PersonalFinanceCommon/Embedded/scripts/{folder}/{file}.{extension}",
                    new RouteValueDictionary(new { }),
                    new RouteValueDictionary(new { extension = "js" }),
                    new EmbeddedResourceRouteHandler()
                ));

            RouteTable.Routes.Insert(0,
                new Route("PersonalFinanceCommon/Embedded/styles/{folder}/{file}.{extension}",
                    new RouteValueDictionary(new { }),
                    new RouteValueDictionary(new { extension = "css" }),
                    new EmbeddedResourceRouteHandler()
                ));

        }
    }
}