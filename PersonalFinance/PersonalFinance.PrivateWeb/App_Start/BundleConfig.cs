using System.Reflection;
using System.Web.Optimization;
using PersonalFinance.Common.Utils;

namespace PersonalFinance.PrivateWeb
{
    public class BundleConfig
    {
        //private static readonly string Version = "_" + Assembly.GetCallingAssembly().GetName().Version;
        //public static string SiteScripts = "~/bundles/scripts" + Version;
        //public static string SiteStyles = "~/bundles/styles" + Version;
        //public static string SitePartials = "~/bundles/partials" + Version;

        //public static void RegisterBundles(BundleCollection bundles)
        //{
            //bundles.Add(new StyleBundle(SiteStyles)
            //    .Include("~/Content/styles/bootstrap/bootstrap.css")
            //    .Include("~/Content/styles/skilled-migration.css"));
            
            //bundles.Add(new ScriptBundle(SiteScripts)
            //    //.IncludeDirectory("~/App/models", "*.js")
            //    .IncludeDirectory("~/App/controllers", "*.js")
            //    .IncludeDirectory("~/App/controllers/applications", "*.js")
            //    .IncludeDirectory("~/App/services", "*.js")
            //    //.IncludeDirectory("~/App/factories", "*.js")
            //    .IncludeDirectory("~/App/directives", "*.js")
            //    .Include("~/App/app.js")
            //    .Include("~/App/appValidationRules.js")
            //    .Include("~/App/appValidationSchemas.js")
            //);
            
            //bundles.Add(new TemplateBundle("personalFinance", SitePartials)
            //    .IncludeDirectory("~/App/views", "*.html")
            //    .IncludeDirectory("~/App/views/application", "*.html"));
        //}
    }
}