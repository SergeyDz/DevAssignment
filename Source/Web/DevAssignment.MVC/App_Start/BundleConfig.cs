using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace DevAssignment.MVC.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/jquery")
                .IncludeDirectory("~/Scripts", "jquery*")
            );

            bundles.Add(new ScriptBundle("~/scripts/ajax")
               .Include("~/Scripts/jquery.unobtrusive-ajax.js")
           );

            bundles.Add(new ScriptBundle("~/scripts/bootstrap")
              .Include("~/Scripts/bootstrap.js")
            );

            bundles.Add(new ScriptBundle("~/scripts/app")
                .Include("~/Scripts/app.js")
            );

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                "~/Content/bootstrap.css")
            );

            bundles.Add(new StyleBundle("~/css/site").Include(
               "~/Content/Site.css")
           );
        }
    }
}