using Cascade.Head.Helpers;
using Cascade.Head.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;
using Orchard.Data;

namespace Cascade.Head.Handlers
{
    public class HeadPartHandler : ContentHandler
    {
        public HeadPartHandler(IRepository<HeadPartRecord> headRepository, IContentManager contentManager)
        {
            Filters.Add(StorageFilter.For(headRepository));

            OnLoaded<HeadPart>((context, part) =>
            {
                if (part == null)
                    return;

                // Deserialize Elements
                part.Elements = HeadElementSerializer.Deserialize(part.RawElements);

            });

            OnUpdated<HeadPart>((context, part) =>
            {
                if (part == null)
                    return;

                // Serialize Elements
                part.RawElements = HeadElementSerializer.Serialize(part.Elements);
            });
        }
    }
}