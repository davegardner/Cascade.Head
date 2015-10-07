using Cascade.Head.Helpers;
using Cascade.Head.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Mvc.Filters;
using System.Linq;
using System.Web.Mvc;

namespace Cascade.Head.Services
{
    public class HeadInjectorService: FilterProvider, IResultFilter 
    {
        private readonly IHeadServices _headServices;
        private readonly IWorkContextAccessor _wca;

        public HeadInjectorService(
            IHeadServices headServices, IWorkContextAccessor wca) {
            _headServices = headServices;
            _wca = wca;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext) {

            // should only run on a full view rendering result
            var isViewResult = filterContext.Result is ViewResult;
            var isAdminRequest = Orchard.UI.Admin.AdminFilter.IsApplied(filterContext.RequestContext);
            if (!isViewResult || isAdminRequest)
                return;

            // display elements from the site settings
            var headElementSettings = _wca.GetContext().CurrentSite.As<HeadSettingsPart>();
            headElementSettings.Elements = SimpleSerializer.Deserialize(headElementSettings.RawElements);
            var allElements = headElementSettings.Elements.Select(e => new HeadElementRecord { Type = e.Type, Content = e.Content, Name = e.Name });
            _headServices.WriteElements(allElements);
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) {}
    }
}