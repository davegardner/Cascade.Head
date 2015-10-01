using Cascade.Head.Models;
using Cascade.Head.Services;
using Cascade.Head.Settings;
using Cascade.Head.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.UI.Resources;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Cascade.Head.Drivers
{
    public class HeadPartDriver : ContentPartDriver<HeadPart>
    {
        private const string mtTitle = "Title", mtName = "Name", mtHdr = "Http-equiv", mtCharset = "Charset", mtLink = "Link";
        private string[] availableTypes = { mtTitle, mtName, mtHdr, mtCharset, mtLink };

        private readonly IHeadServices _headServices;
        private readonly IContentManager _contentManager;
        private readonly IWorkContextAccessor _wca;

        public HeadPartDriver(
            IHeadServices commentService,
            IContentManager contentManager,
            IWorkContextAccessor wca)
        {
            _wca = wca;
            _headServices = commentService;
            _contentManager = contentManager;
        }

        protected override string Prefix { get { return "Head"; } }

        protected override DriverResult Display(HeadPart part, string displayType, dynamic shapeHelper)
        {
            if (displayType != "Detail") return null;
            var resourceManager = _wca.GetContext().Resolve<IResourceManager>();

            // Merge content-level elements with type-level elements
            var allElements = new List<HeadElementRecord>(part.Elements);
            
            // The line below is how you're supposed to do it, but it did not work for me
            // so I access the settings collection directly instead.
            //var typeSettings = part.Settings.GetModel<HeadTypePartSettings>();
            var raw = part.Settings["HeadTypePartSettings.RawElements"];
            var typeSettings = new HeadTypePartSettings { RawElements = raw };

            var typeElements = typeSettings.Elements.Select(e => new HeadElementRecord { Type = e.Type, Content = e.Content, Name = e.Name });
            allElements.AddRange(typeElements);

            foreach (var element in allElements)
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
            return null;
        }

        protected override DriverResult Editor(HeadPart part, dynamic shapeHelper)
        {
            var head = new HeadVM
            {
                Id = part.Id,
                AvailableTypes = availableTypes
            };

            head.Elements = part.Elements.Select(e => new ElementVM { Id = e.Id, Deleted = false, Content = e.Content, Type = e.Type, Name = e.Name, HeadPartRecord_Id = e.HeadPartRecord.Id }).ToList();

            return ContentShape("Parts_HeadPart", () =>
            {
                return shapeHelper.EditorTemplate(TemplateName: "Parts.HeadPart", Model: head, Prefix: Prefix);
            });
        }

        protected override DriverResult Editor(HeadPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var head = new HeadVM();
            updater.TryUpdateModel(head, Prefix, null, null);

            // eliminate deleted elements
            head.Elements = head.Elements.Where(e => e.Deleted != true).ToList();

            if (part.Id != 0)
                _headServices.SynchronizeElements(part, head);

            return ContentShape("Parts_HeadPart", () =>
            {
                return shapeHelper.EditorTemplate(TemplateName: "Parts.HeadPart", Model: head, Prefix: Prefix);
            });
        }


        private string GetType(string content)
        {
            // Approximation that will work for common extensions png and ico
            string type = "image";
            var ext = Path.GetExtension(content);
            if(!string.IsNullOrEmpty(ext))
            {
                type += "/" + ext.TrimStart('.');
            }
            return type;
        }

        protected override void Importing(HeadPart part, ImportContentContext context)
        {

        }

        protected override void Exporting(HeadPart part, ExportContentContext context)
        {

        }
    }
}