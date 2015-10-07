using Orchard;
using Orchard.Caching;
using System.Web;

namespace Cascade.Head.Services
{
    public interface IPageHeadService : IDependency
    {
        string GetPageTitle();
    }

    public class PageHeadService : IPageHeadService
    {
        private readonly IWorkContextAccessor _wca;
        private readonly ICacheManager _cacheManager;
        private readonly ISignals _signals;

        public PageHeadService(IWorkContextAccessor wca, ICacheManager cacheManager, ISignals signals)
        {
            _wca = wca;
            _cacheManager = cacheManager;
            _signals = signals;
        }

        public PageHeadService(WorkContext workContext)
        {
            _wca = workContext.Resolve<IWorkContextAccessor>();
            _cacheManager = new DefaultCacheManager(this.GetType(), new DefaultCacheHolder(new DefaultCacheContextAccessor()));
            _signals = workContext.Resolve<ISignals>();
        }

        public string GetPageTitle()
        {
            string pageTitleOverride = "";
            try
            {
                if (HttpContext.Current.Cache["Cascade.Head.PageTitle"] != null)
                {
                    pageTitleOverride = HttpContext.Current.Cache["Cascade.Head.PageTitle"].ToString();
                    HttpContext.Current.Cache["Cascade.Head.PageTitle"] = "";
                }
            }
            catch { }
            return pageTitleOverride;

        }
    }
}
