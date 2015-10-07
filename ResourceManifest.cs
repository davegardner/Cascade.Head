using Orchard.UI.Resources;

namespace Cascade.FlipCard {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            manifest.DefineStyle("Head").SetUrl("Head.min.css", "Head.css");

            manifest.DefineScript("Head").SetVersion("1.0.1").SetUrl("Head.js").SetDependencies("jQuery");
            manifest.DefineScript("Bootstrap").SetUrl("bootstrap.min.js", "bootstrap.js").SetDependencies("jQuery");
        }
    }
}
