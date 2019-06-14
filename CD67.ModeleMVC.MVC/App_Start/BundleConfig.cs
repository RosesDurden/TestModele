using System.Web;
using System.Web.Optimization;

namespace CD67.ModeleMVC.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/cd67").Include(
                      "~/Scripts/cd67-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/all.css", //Fontawesome
                      "~/Content/jquery.validate.css",
                      "~/Content/cd67-model.css",
                      "~/Content/cd67-custom.css"));

            // Jquery-ui
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                      "~/Scripts/jquery-ui-{version}.js",
                      "~/Scripts/jquery-ui-i18n.js",
                      "~/Scripts/modernizr-{version}.js",
                      "~/Scripts/jquery.are-you-sure.js"));
            bundles.Add(new StyleBundle("~/Content/jquery-ui-css").Include(
                      "~/Content/themes/base/*.css"));

            // Datatable
            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                      "~/Content/DataTables/datatables.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                      "~/Content/DataTables/datatables.min.js"));
        }
    }
}