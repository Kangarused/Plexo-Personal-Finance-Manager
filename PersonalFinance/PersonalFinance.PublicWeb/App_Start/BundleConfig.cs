using System.Reflection;
using System.Web.Optimization;
using PersonalFinance.Common.Utils;

namespace PersonalFinance.PublicWeb
{
    public class BundleConfig
    {
        private static readonly string Version = "_" + Assembly.GetCallingAssembly().GetName().Version;
        public static readonly string SiteScripts = "~/bundles/scripts" + Version;
        public static readonly string SiteStyles = "~/bundles/styles" + Version;
        public static string SitePartials = "~/bundles/partials" + Version;

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle(SiteStyles)
                .Include("~/Content/styles/PersonalFinancePublic.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/angular-dt/angular-datatables.min.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/angular-dt/datatables.bootstrap.min.css", new CssRewriteUrlTransform())
           );
            
            bundles.Add(new ScriptBundle(SiteScripts)
                .Include("~/Scripts/Enums.js")
                .IncludeDirectory("~/Scripts", "*.js")
                .IncludeDirectory("~/App/models", "*.js")
                .IncludeDirectory("~/App/controllers", "*.js")
                .Include("~/App/services/baseDataService.js")
                .IncludeDirectory("~/App/services", "*.js")
                .IncludeDirectory("~/App/factories", "*.js")
                .IncludeDirectory("~/App/directives", "*.js")
                .Include("~/App/app.js")
                .Include("~/App/appValidationRules.js")
                .Include("~/App/appValidationSchemas.js")
                .Include("~/Scripts/angular-dt/datatables.min.js")
                .Include("~/Scripts/angular-dt/angular-datatables.min.js")
                .Include("~/Scripts/angular-dt/angular-datatables.bootstrap.min.js")
            );

            bundles.Add(new TemplateBundle("personalFinance", SitePartials)
                .IncludeDirectory("~/App/views", "*.html"));
        }
    }
}