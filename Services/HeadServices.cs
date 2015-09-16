using Cascade.Head.Models;
using Cascade.Head.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using System.Linq;

namespace Cascade.Head.Services
{
    // Interface
    public interface IHeadServices : IDependency
    {
        void Delete(int id);
        void Publish(int id);
        void Unpublish(int id);
        HeadPart GetHead(int id);
        void SynchronizeElements(HeadPart contentItem, HeadVM vm);
    }



    // Implementation
    public class HeadServices: IHeadServices
    {
        private readonly IOrchardServices _services;
        private readonly IRepository<HeadElementRecord> _elementsRepository;

        public HeadServices(IOrchardServices service, IRepository<HeadElementRecord> elementsRepo)
        {
            _elementsRepository = elementsRepo;
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

        private HeadElementRecord CreateElement(HeadPart part, ElementVM vm)
        {
            var element = new HeadElementRecord { HeadPartRecord = part.Record };
            part.Record.HeadElementRecords.Add(element);
            return UpdateElement(element, vm);
        }

        private HeadElementRecord UpdateElement(HeadElementRecord element, ElementVM vm)
        {
            element.Type = vm.Type;
            element.Name = vm.Name;
            element.Content = vm.Content;
            return element;
        }

        private void DeleteElement(HeadPart part, HeadElementRecord element)
        {
            //part.Record.HeadElementRecords.Remove(element);
            _elementsRepository.Delete(element);
        }

        public void SynchronizeElements(HeadPart part, HeadVM vm)
        {
            var newElements = vm.Elements;
            var existingElements = part.Elements;

            // delete elements
            foreach (var existingElement in existingElements)
                if (newElements == null || !newElements.Any(e => e.Id == existingElement.Id))
                    DeleteElement(part, existingElement);
            
            // add or update elements
            foreach(var newElement in newElements)
                if(newElement.Id == 0)
                    _elementsRepository.Create(CreateElement(part, newElement));
                else
                    _elementsRepository.Update(UpdateElement(existingElements.First(e=>e.Id == newElement.Id), newElement));
        }
    }
}