using System.Web;
using System.Web.Optimization;

namespace EkRishta
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/CustomJs").Include(
                        "~/Scripts/CustomJs/jquery-3.3.1.min.js",
                        "~/Scripts/CustomJs/jquery.slicknav.min.js",
                        "~/Scripts/CustomJs/swiper.min.js",
                        "~/Scripts/CustomJs/wow.min.js",
                        "~/Scripts/CustomJs/main.js"));


            ///Style Bundle
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/CustomCss").Include(
                      "~/Content/CustomCss/themify-icons.css",
                      "~/Content/CustomCss/bootstrap.min.css",
                      "~/Content/CustomCss/style.css",
                      "~/Content/CustomCss/style1.css",
                      "~/Content/CustomCss/swiper.min.css",
                      "~/Content/CustomCss/main.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            BundleTable.EnableOptimizations = true;
        }
    }
}
