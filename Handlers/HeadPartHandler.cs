using Cascade.Head.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using System.Linq;

namespace Cascade.Head.Handlers
{
    public class HeadPartHandler : ContentHandler
    {
        public HeadPartHandler(IRepository<HeadPartRecord> headRepository, IRepository<HeadElementRecord> elementsRepository, IContentManager contentManager)
        {
            Filters.Add(StorageFilter.For(headRepository));

            OnLoading<HeadPart>((context, head) =>
            {
                head.Record.HeadElementRecords = elementsRepository.Fetch(e=>e.HeadPartRecord.Id == head.Id).ToList();
            });

            OnRemoving<HeadPart>((context, head) =>
            {
                foreach (var element in elementsRepository.Fetch(e=>e.HeadPartRecord.Id == head.Id).ToList())
                {
                    elementsRepository.Delete(element);
                }
            });

        }
    }
}