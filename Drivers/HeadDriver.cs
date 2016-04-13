using Cascade.Head.Helpers;
using Cascade.Head.Models;
using Cascade.Head.Services;
using Cascade.Head.Settings;
using Cascade.Head.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Cascade.Head.Drivers
{
    public class HeadPartDriver : ContentPartDriver<HeadPart>
    {
        private const string mtTitle = "Title", mtName = "Name", mtHdr = "Http-equiv", mtCharset = "Charset", mtLink = "Link";
        private string[] availableTypes = { mtTitle, mtName, mtHdr, mtCharset, mtLink };

        private readonly IHeadServices _headServices;
        private readonly IContentManager _contentManager;

        public HeadPartDriver(
            IHeadServices commentService,
            IContentManager contentManager
            )
        {
            _headServices = commentService;
            _contentManager = contentManager;
        }

        protected override string Prefix { get { return "Head"; } }

        protected override DriverResult Display(HeadPart part, string displayType, dynamic shapeHelper)
        {
            // Don't insert elements unless it's a detail display
            if (displayType != "Detail") 
                return null;

            // Get content-level elements
            var allElements = new List<Element>(part.Elements);

            // Merge with type-level elements (if any)
            if (part != null && part.Settings != null)
            {
                // The commented-out line below is how you're supposed to do it, but it did not work for me
                // so I access the settings collection directly instead.
                //var typeSettings = part.Settings.GetModel<HeadTypePartSettings>();
                var raw = part.Settings["HeadTypePartSettings.RawElements"];
                var typeSettings = new HeadTypePartSettings
                {
                    RawElements = raw,
                    Elements = HeadElementSerializer.Deserialize(raw)
                };
                allElements.AddRange(typeSettings.Elements);
            }

            _headServices.WriteElements(allElements);
            
            return null;
        }

        protected override DriverResult Editor(HeadPart part, dynamic shapeHelper)
        {
            var head = new HeadVM
            {
                Id = part.Id,
                AvailableTypes = availableTypes
            };

            head.Elements = part.Elements;

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

            // update the part
            part.Elements = head.Elements;

            return ContentShape("Parts_HeadPart", () =>
            {
                return shapeHelper.EditorTemplate(TemplateName: "Parts.HeadPart", Model: head, Prefix: Prefix);
            });
        }


        
        protected override void Importing(HeadPart part, ImportContentContext context)
        {
            // Don't do anything if the tag is not specified.
            if (context.Data.Element(part.PartDefinition.Name) == null)
            {
                return;
            }

            context.ImportAttribute(part.PartDefinition.Name, "RawElements", rawElements =>
                part.RawElements = rawElements
            );
        }

        protected override void Exporting(HeadPart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("RawElements", part.Record.RawElements);

        }
    }
}