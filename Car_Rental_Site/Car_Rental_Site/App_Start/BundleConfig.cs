using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Hosting;

namespace Car_Rental_Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts

            #region jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap*"));
            #endregion

            #region ManagerScripts
            bundles.Add(new ScriptBundle("~/bundles/ManagerPageIndex").Include(
                "~/Areas/Manage/Scripts/ManagerPageScript.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/ManagerUsersPage").Include(
                "~/Areas/Manage/Scripts/UserPageScript.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/GetVehiclesTypes").Include(
                "~/Areas/Manage/Scripts/VehiclesTypesScript.js"
            ));
            #endregion

            #region EmployeeScripts
            bundles.Add(new ScriptBundle("~/bundles/EmployeePage").Include(
            "~/Areas/Employee/Scripts/ReturnPage.js"));
            #endregion

            #region AllUsers
            bundles.Add(new ScriptBundle("~/bundles/SearchPage").Include(
            "~/Scripts/SearchPage.js",
            "~/Scripts/localStorageSearch.js"));

            bundles.Add(new ScriptBundle("~/bundles/UserManagePage").Include(
            "~/Scripts/UserManagePage.js"));

            bundles.Add(new ScriptBundle("~/bundles/ErrorMessages").Include(
            "~/Scripts/ErrorMessages.js"));
            #endregion

            #endregion

            #region Styles

            #region jquery
            bundles.Add(new StyleBundle("~/Content/themes/smoothness/css").Include(
                "~/Content/themes/smoothness/jquery-ui-{version}.custom.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css",
                        "~/Content/themes/smoothness/jquery-ui-1.10.3.custom.css"));
            #endregion

            #region ManagerStyles
            bundles.Add(new StyleBundle("~/Content/ManagerStyle").Include("~/Areas/Manage/Content/ManagerStyle.css"));
            #endregion

            #region EmployeeStyles
            bundles.Add(new StyleBundle("~/Content/EmployeeReturnedStyle").Include(
            "~/Areas/Employee/Content/ReturnStyle.css"));

            bundles.Add(new StyleBundle("~/Content/EmployeeReturnVehiclesStyle").Include(
            "~/Areas/Employee/Content/ReturnVehicleStyle.css"));
            #endregion

            #region AllUsers
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/SearchPageStyle").Include(
            "~/Content/SearchStyle.css",
            "~/Content/SearchResultStyle.css",
            "~/Content/FilterMenuStyle.css"));

            bundles.Add(new StyleBundle("~/Content/UserManagePageStyle").Include("~/Content/UserManageStyle.css"));
            #endregion

            #endregion

        }
    }
    public class BundleRelaxed : Bundle
    {
        public BundleRelaxed(string virtualPath)
            : base(virtualPath)
        {
        }

        public new BundleRelaxed IncludeDirectory(string directoryVirtualPath, string searchPattern, bool searchSubdirectories)
        {
            var truePath = HostingEnvironment.MapPath(directoryVirtualPath);
            if (truePath == null) return this;

            var dir = new System.IO.DirectoryInfo(truePath);
            if (!dir.Exists || dir.GetFiles(searchPattern).Length < 1) return this;

            base.IncludeDirectory(directoryVirtualPath, searchPattern);
            return this;
        }

        public new BundleRelaxed IncludeDirectory(string directoryVirtualPath, string searchPattern)
        {
            return IncludeDirectory(directoryVirtualPath, searchPattern, false);
        }
    }
}
