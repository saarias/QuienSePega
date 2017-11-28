using System.Web;
using System.Web.Optimization;

namespace QuienSePega
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/new/vendor/jquery/jquery.min.js",
                        "~/Scripts/new/vendor/bootstrap/js/bootstrap.bundle.min.js",
                        "~/Scripts/new/vendor/jquery-easing/jquery.easing.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //bootstrap.bundle.min.js.map
            bundles.Add(new ScriptBundle("~/bundles/map").Include(
            "~/Scripts/bootstrap.bundle.min.js.map"));

            /*bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap.bundle.min.js"));*/

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Scripts/new/vendor/bootstrap/css/bootstrap.min.css",
                      "~/Scripts/new/vendor/fonT-awesome/css/font-awesome.min.css",
                      "~/Content/new/css/sb-admin.css", "~/Content/Custom.css"));
        }
    }
}
