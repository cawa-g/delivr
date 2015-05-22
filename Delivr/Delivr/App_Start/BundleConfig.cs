using Forloop.HtmlHelpers;
using System.Web;
using System.Web.Optimization;

namespace Delivr
{
    public class BundleConfig
    {
        // Pour plus d'informations sur Bundling, accédez à l'adresse http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jasnyjs").Include(
                        "~/Scripts/jasny-bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/parallaxjs").Include(
                        "~/Scripts/parallax.js"));

            // Utilisez la version de développement de Modernizr pour développer et apprendre. Puis, lorsque vous êtes
            // prêt pour la production, utilisez l'outil de génération sur http://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                        "~/Content/bootstrap/bootstrap.css",
                        "~/Content/bootstrap/bootstrap-responsive.css"));

            bundles.Add(new StyleBundle("~/Content/jasnycss").Include(
                        "~/Content/jasny/jasny-bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/additions").Include(
                        "~/Content/Site.css"));
          
            ScriptContext.ScriptPathResolver = System.Web.Optimization.Scripts.Render;
        }
    }
}