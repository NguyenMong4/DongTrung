using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace WebDongTrung.Helper
{
    public class CustomUrlHelper : IUrlHelper
    {
        private readonly IUrlHelper _originalUrlHelper;

        public ActionContext ActionContext { get; }

        public CustomUrlHelper(ActionContext actionContext, IUrlHelper originalUrlHelper)
        {
            ActionContext = actionContext;
            _originalUrlHelper = originalUrlHelper;
        }

        public string Action(UrlActionContext urlActionContext)
        {
            return _originalUrlHelper.Action(urlActionContext);
        }

        public string Content(string contentPath)
        {
            return _originalUrlHelper.Content(contentPath);
        }

        public bool IsLocalUrl(string url)
        {
            return _originalUrlHelper.IsLocalUrl(url);
        }

        public string Link(string routeName, object values)
        {
            return _originalUrlHelper.Link(routeName, values);
        }

        public string RouteUrl(UrlRouteContext routeContext)
        {
            return _originalUrlHelper.RouteUrl(routeContext);
        }
    }

    public class CustomUrlHelperFactory : IUrlHelperFactory
    {
        public IUrlHelper GetUrlHelper(ActionContext context)
        {
            var originalUrlHelperFactory = new UrlHelperFactory();
            var originalUrlHelper = originalUrlHelperFactory.GetUrlHelper(context);
            return new CustomUrlHelper(context, originalUrlHelper);
        }
    }
}