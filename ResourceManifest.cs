using Orchard.UI.Resources;

namespace Cascade.FlipCard {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            manifest.DefineStyle("Head").SetUrl("Head.min.css", "Head.css");

            //manifest.DefineScript("Angular").SetVersion("1.3.3").SetUrl("angular.min.js", "angular.js").SetCdn("https://ajax.googleapis.com/ajax/libs/angularjs/1.3.3/angular.min.js");
            //manifest.DefineScript("AngularResource").SetVersion("1.3.3").SetDependencies("Angular").SetUrl("angular-resource.min.js", "angular-resource.js").SetCdn("https://ajax.googleapis.com/ajax/libs/angularjs/1.3.3/angular-resource.min.js");
            //manifest.DefineScript("AngularAnimate").SetVersion("1.3.3").SetDependencies("Angular").SetUrl("angular-animate.min.js", "angular-animate.js").SetCdn("https://ajax.googleapis.com/ajax/libs/angularjs/1.3.3/angular-animate.min.js");
            manifest.DefineScript("Head").SetVersion("1.0.1").SetUrl("Head.js").SetDependencies("jQuery");
            manifest.DefineScript("Bootstrap").SetUrl("bootstrap.min.js", "bootstrap.js").SetDependencies("jQuery");
            //manifest.DefineScript("AngularBootstrapUI").SetUrl("ui-bootstrap-tpls-0.13.3.min.js", "ui-bootstrap-tpls-0.13.3.js").SetDependencies("Angular");

            manifest.DefineScript("WebComponentsLite").SetUrl("/Modules/Cascade.Head/Contents/bower_components/webcomponentsjs/webcomponents-lite-min.js", "/Modules/Cascade.Head/Contents/bower_components/webcomponentsjs/webcomponents-lite.js");

        }
    }
}
