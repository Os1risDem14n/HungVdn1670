using System.Web;
using System.Web.Optimization;

namespace HungVdn1670
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*",
                        "~/lib/jquery/jquery.min.js",
                        "~/lib/boostrap/dist/js/bootstrap.bundle.min.js",
                        "~/lib/jquery-easing/jquery.easing.min.js",
                        "~/sbadmin/js/sb-admin-2.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/lib/font-awesome/css/all.min.css",
                      "~/sbadmin/css/sb-admin-2.min.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/Site.css"));
        }
    }
}
