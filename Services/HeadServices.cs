using Cascade.Head.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.UI.Resources;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Cascade.Head.Services
{
    // Interface
    public interface IHeadServices : IDependency
    {
        void Publish(int id);
        void Unpublish(int id);
        HeadPart GetHead(int id);
        void WriteElements(IEnumerable<Element> elements);
    }



    // Implementation
    public class HeadServices: IHeadServices
    {
        private readonly IOrchardServices _services;
        private readonly IWorkContextAccessor _wca;

        public HeadServices(IOrchardServices service, IWorkContextAccessor wca)
        {
            _wca = wca;
            _services = service;

        }

        public void Delete(int id)
        {
            var contentItem = _services.ContentManager.Get(id);
            _services.ContentManager.Remove(contentItem);
        }

        public void Publish(int id)
        {
            // if latest is draft then publish
            var contentItem = _services.ContentManager.Get(id, VersionOptions.Draft);
            if(contentItem != null && !contentItem.IsPublished())
                _services.ContentManager.Publish(contentItem);
        }

        public void Unpublish(int id)
        {
            var contentItem = _services.ContentManager.Get(id, VersionOptions.Published);
            if(contentItem != null && contentItem.IsPublished())
                _services.ContentManager.Unpublish(contentItem);
        }

        public HeadPart GetHead(int id)
        {
            return _services.ContentManager.Get<HeadPart>(id);
        }



        public void WriteElements(IEnumerable<Element> elements)
        {
            var resourceManager = _wca.GetContext().Resolve<IResourceManager>();
           
            foreach (var element in elements)
            {
                switch (element.Type.ToLower())
                {
                    case "title":
                        HttpContext.Current.Cache.Insert("Cascade.Head.PageTitle", element.Content ?? "");
                        break;
                    case "name":
                        resourceManager.SetMeta(new MetaEntry
                        {
                            Name = element.Name,
                            Content = element.Content,

                        });
                        break;
                    case "http-equiv":
                        resourceManager.SetMeta(new MetaEntry
                        {
                            HttpEquiv = element.Name,
                            Content = element.Content
                        });
                        break;
                    case "charset":
                        resourceManager.SetMeta(new MetaEntry
                        {
                            Charset = element.Content
                        });
                        break;
                    case "link":
                        resourceManager.RegisterLink(new LinkEntry
                        {
                            Rel = element.Name,
                            Type = GetType(element.Content),
                            Href = element.Content
                        });
                        break;
                }
            }
        }
        private string GetType(string content)
        {
            // Approximation that will work for common extensions png and ico
            string type = "image";
            var ext = Path.GetExtension(content);
            if (!string.IsNullOrEmpty(ext))
            {
                type += "/" + ext.TrimStart('.');
            }
            return type;
        }
    }
}