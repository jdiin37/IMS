using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace IMS
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/locales/bootstrap-datepicker.zh-TW.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/jquery_setting").Include(
                "~/Scripts/base.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/datepicker/bootstrap-datepicker.min.css",
                      "~/Content/custom.css",
                      "~/Content/site.css"));
            

        }
    }
}