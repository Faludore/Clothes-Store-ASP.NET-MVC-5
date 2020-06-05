using System.Web;
using System.Web.Optimization;

namespace StoreIdent
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Content/Adminka/js/jquery.min.js",
                        "~/Content/js/jquery.min.js",
                        "~/Content/img/mdb-favicon.ico",
                        "~/Content/js/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Content/img/mdb-favicon.ico",
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Content/img/mdb-favicon.ico",
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/js/vendor/modernizr-2.8.3.min.js",
                      "~/Content/img/mdb-favicon.ico",
                      "~/Content/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
         

                       "~/Content/css/bootstrap.min.css",
                       "~/Content/css/animate.css",
                       "~/Content/css/pe-icon-7-stroke.min.css",
                       "~/Content/css/jquery-ui.min.css",
                       "~/Content/css/img-zoom/jquery.simpleLens.css",
                       "~/Content/css/bootstrap.min.css",
                       "~/Content/css/meanmenu.min.css",

                       "~/Content/lib/css/nivo-slider.css",
                       "~/Content/lib/css/preview.css",
                       "~/Content/css/owl.carousel.css",
                       "~/Content/css/font-awesome.min.css",
                       "~/Content/style.css",
                       "~/Content/css/responsive.css"));


        }
    }
}
