using NUglify;
using NUglify.Css;
using NUglify.JavaScript;
using System;
using System.Web.Optimization;

namespace BSG.ADInventory.WebUI
{
    public class BundleConfig
    {



        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
           

            #region Scripts
            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/jquery",
               "~/Content/plugins/jquery/jquery.js"
               );

            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/bootstrap",
               "~/Content/plugins/bootstrap/js/bootstrap.bundle.js"
               );

            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/jqueryval",
               "~/Content/plugins/jquery-validation/jquery.validate*"
               );

            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/jqueryval-unobtrusive",
               "~/Content/plugins/jquery-validation/jquery.validate.unobtrusive.js",
                "~/Content/plugins/jquery-validation/jquery.unobtrusive-ajax.js"
               );

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.            
            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/modernizr",
               "~/Content/plugins/jquery/modernizr-*"
               );


            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/select2",
               "~/Content/plugins/select2/js/select2.full.js"
               );

            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/datatable",
                      "~/Content/plugins/datatables/jquery.dataTables.js",
                      "~/Content/plugins/datatables-bs4/js/dataTables.bootstrap4.js",
                      "~/Content/plugins/datatables-responsive/js/dataTables.responsive.js",
                      "~/Content/plugins/datatables-responsive/js/responsive.bootstrap4.js",
                      "~/Content/plugins/datatables-buttons/js/dataTables.buttons.js",
                      "~/Content/plugins/datatables-buttons/js/buttons.bootstrap4.js",
                      "~/Content/plugins/jszip/jszip.js",
                      "~/Content/plugins/pdfmake/pdfmake.js",
                      "~/Content/plugins/pdfmake/vfs_fonts.js",
                      "~/Content/plugins/datatables-buttons/js/buttons.html5.js",
                      "~/Content/plugins/datatables-buttons/js/buttons.print.js",
                      "~/Content/plugins/datatables-buttons/js/buttons.colVis.js"
               );

            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/datatableInit",
                     "~/Content/plugins/datatables/dataTableInit.js"
              );


            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/sweetalert2",
               "~/Content/plugins/sweetalert2/sweetalert2.js"
               );

            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/toastr",
                    "~/Content/plugins/toastr/toastr.js"
                    );

            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/persian-datetimepicker",
                              "~/Content/plugins/persian-datetimepicker/persian-date.js",
                              "~/Content/plugins/persian-datetimepicker/persian-datepicker.js"
                              );

            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/jquery-ui",
                    "~/Content/plugins/jquery-ui/jquery-ui.js"
                    );


            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/AdminPanel",
                      "~/Content/dist/js/adminlte.js",
                      "~/Content/plugins/bootstrap-fileupload/bootstrap-fileupload.js"
                      );


            AddScriptBundleWithNUglify(bundles, "~/bundlesScriptMaham/main",
                      "~/Content/plugins/mahamScripts/main.js"
                      );




            #endregion Scripts

            #region CSS

            AddBundleWithNUglify(bundles, "~/ContentMahamUI/datatable/css",
                                "~/Content/plugins/datatables-bs4/css/dataTables.bootstrap4.css",
                                "~/Content/plugins/datatables-responsive/css/responsive.bootstrap4.css",
                                "~/Content/plugins/datatables-buttons/css/buttons.bootstrap4.css"
                                );

            AddBundleWithNUglify(bundles, "~/ContentMahamUI/persian-datetimepicker/css",
                               "~/Content/plugins/persian-datetimepicker/persian-datepicker.css"
                               );

            AddBundleWithNUglify(bundles, "~/ContentMahamUI/select2/css",
                                "~/Content/plugins/select2-bootstrap4-theme/select2-bootstrap4.css",
                                "~/Content/plugins/select2/css/select2.css"
                                );

            AddBundleWithNUglify(bundles, "~/ContentMahamUI/sweetalert2/css",
                                "~/Content/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.css"
                                );

            AddBundleWithNUglify(bundles, "~/ContentMahamUI/toastr/css",
                                "~/Content/plugins/toastr/toastr.css"
                                );

            AddBundleWithNUglify(bundles, "~/ContentMahamUI/admin/css",
                                  "~/Content/dist/css/adminlte.css",
                                  "~/Content/dist/css/bootstrap-rtl/bootstrap.css",
                                  "~/Content/dist/css/adminlte-rtl.css",
                                  "~/Content/plugins/bootstrap-fileupload/bootstrap-fileupload.css"
                                );

            AddBundleWithNUglify(bundles, "~/ContentMahamUI/theme/css",
                                  "~/Content/dist/css/BSstyle.css",
                                  "~/Content/dist/css/Easyweb.css",
                                  "~/Content/dist/css/theme-dark-blue.css",
                                  "~/Content/dist/css/maham.css"
                                );

            AddBundleWithNUglify(bundles, "~/ContentMahamUI/fontAwesome/css",
                "~/Content/plugins/fontawesome-free/css/all.css"
                );


            bundles.Add(new StyleBundle("~/Content/themLogin").Include(
                "~/Content/plugins/fontawesome-free/css/all.css",
                "~/Content/plugins/fontawesome-free/css/duotone.css",
                "~/Content/plugins/fontawesome-free/css/light.css",
                      "~/Content/dist/css/adminlte.css",
                      "~/Content/dist/css/BSstyle.css",
                      "~/Content/dist/css/theme-dark-blue.css",
                      "~/Content/dist/css/login-page-dark-blue.css"
                      ));
            #endregion CSS

        

        

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif

        }


        private static void AddBundleWithNUglify(BundleCollection bundles, string bundlePath, params string[] includePaths)
        {
            var bundle = new StyleBundle(bundlePath).Include(includePaths);
            bundle.Transforms.Clear();
            bundle.Transforms.Add(new CssMinifyWithNUglify());
            bundles.Add(bundle);
        }

        private static void AddScriptBundleWithNUglify(BundleCollection bundles, string bundlePath, params string[] includePaths)
        {
            var bundle = new ScriptBundle(bundlePath).Include(includePaths);
            bundle.Transforms.Clear();
            bundle.Transforms.Add(new JsMinifyWithNUglify());
            bundles.Add(bundle);
        }
    }


    // Custom JsMinify
    public class JsMinifyWithNUglify : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            // Configure JavaScript minification settings
            var settings = new CodeSettings
            {
                OutputMode = OutputMode.SingleLine,  // Minifies to a single line, removing comments                
                PreserveImportantComments = false,   // Removes all comments
                RemoveUnneededCode = true,           // Further reduces code to a single line by removing unnecessary parts
                MinifyCode = true
            };
            // Minify JavaScript code using NUglify with settings
            var result = Uglify.Js(response.Content, settings);

            if (result.HasErrors)
            {
                // Log errors instead of throwing an exception
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Message} at Line: {error.StartLine}, Column: {error.StartColumn}");
                }
            }

            // Update response content
            response.Content = result.Code;
            response.ContentType = "text/javascript";

            //var result = Uglify.Js(response.Content);
            //if (result.HasErrors)
            //{
            //    // throw new Exception("NUglify encountered errors during JS minification.");
            //    foreach (var error in result.Errors)
            //    {
            //        Console.WriteLine($"Error: {error.Message} at Line: {error.StartLine}, Column: {error.StartColumn}");
            //    }
            //}
            //response.Content = result.Code;
            //response.ContentType = "text/javascript";
        }
    }

    // Custom CssMinify
    public class CssMinifyWithNUglify : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            // Configure CSS minification settings
            var settings = new CssSettings
            {
                CommentMode = CssComment.None,  // Removes all comments
                OutputMode = OutputMode.SingleLine,
                //TermSemicolons = true,
                //AllowEmbeddedAspNetBlocks = true,
                //IgnoreAllErrors = true,
                TermSemicolons = true
            };

            // Minify CSS code using NUglify with settings
            var result = Uglify.Css(response.Content, settings);

            if (result.HasErrors)
            {
                // Handle errors, e.g., logging or re-throwing
                throw new Exception("NUglify encountered errors during CSS minification.");
            }

            // Update response content
            response.Content = result.Code;
            response.ContentType = "text/css";
            //var result = Uglify.Css(response.Content);
            //if (result.HasErrors)
            //{
            //    throw new Exception("NUglify encountered errors during CSS minification.");
            //}
            //response.Content = result.Code;
            //response.ContentType = "text/css";

        }
    }


}
