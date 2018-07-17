using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Senai.Chamados.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/DataTable/css").Include(

                "~/Content/DataTables/extensions/Buttons/css/buttons.dataTables.css",
                "~/Content/DataTables/extensions/Buttons/css/buttons.bootstrap.min.css"


                ));

            bundles.Add(new ScriptBundle("~/bundles/DataTable/js").Include(

                  "~/Scripts/DataTables/media/js/jquery.dataTables.min.js",
                    "~/Scripts/DataTables/media/js/dataTables.bootstrap.min.js",
                   "~/Scripts/DataTables/media/js/jquery.dataTables.min.js",
                   "~/Scripts/DataTables/media/js/dataTables.bootstrap.min.js",
                    "~/Scripts/DataTables/extensions/Buttons/js/dataTables.buttons.min.js",
                   "~/Scripts/DataTables/extensions/Buttons/js/buttons.html5.min.js",
                    "~/Scripts/DataTables/extensions/Buttons/js/buttons.print.min.js",
                   "~/Scripts/jszip.min.js"
                ));
        }
    }
}