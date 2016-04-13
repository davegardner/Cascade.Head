using Cascade.Head.Helpers;
using Cascade.Head.Models;
using Cascade.Head.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;
using Orchard.Localization;
using System.Linq;

namespace Cascade.Head.Handlers
{
    public class HeadSettingsPartHandler : ContentHandler
    {
        private readonly IHeadServices _headServices;

        public HeadSettingsPartHandler(IHeadServices headServices)
        {
            _headServices = headServices;

            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<HeadSettingsPart>("Site"));

            Filters.Add(new TemplateFilterForPart<HeadSettingsPart>("HeadSettingsPart", "Parts/HeadSettings", "HeadElements"));


            OnLoaded<HeadSettingsPart>((context, part) =>
            {
                if (part == null)
                    return;

                // Deserialize 
                part.Elements = HeadElementSerializer.Deserialize(part.RawElements);

            });

            OnUpdated<HeadSettingsPart>((context, part) =>
            {
                if (part == null)
                    return;

                // eliminate deleted elements
                part.Elements = part.Elements.Where(e => e.Deleted != true).ToList();

                // Serialize
                part.RawElements = HeadElementSerializer.Serialize(part.Elements);
            });

        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;

            base.GetItemMetadata(context);

            // Constructor for GroupInfo is nuts
            var group = new GroupInfo(T("HeadElements"));
            group.Name = T("Head Elements");

            context.Metadata.EditorGroupInfo.Add(group);
        }
    }

}