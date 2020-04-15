using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace RapidFireLib.Lib.Core
{
    internal static class Context
    {
        public static Http Http = new Http();
    }

    public class Http
    {
        public HttpContext HttpContext { get; set; }
        public IViewEngine ViewEngine { get; set; }
        public ActionContext ActionContext { get; set; }
        public ITempDataDictionaryFactory TempDataDictionaryFactory { get; set; }
        public ITempDataDictionary TempDataDictionary { get; set; }
        public IModelMetadataProvider ModelMetadataProvider { get; set; }
        public ViewDataDictionary ViewDataDictionary { get; set; }

        public Http()
        {
            DI<IHttpContextAccessor, HttpContextAccessor> di = new DI<IHttpContextAccessor, HttpContextAccessor>();
            HttpContext = di.Inflate.Members.HttpContext;
            ViewEngine = HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            ActionContext = new ActionContext(HttpContext, new RouteData(), new ActionDescriptor());
            TempDataDictionaryFactory = HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
            TempDataDictionary = TempDataDictionaryFactory.GetTempData(HttpContext);
            ModelMetadataProvider = HttpContext.RequestServices.GetRequiredService<IModelMetadataProvider>();
            ViewDataDictionary = new ViewDataDictionary(ModelMetadataProvider, new ModelStateDictionary());
        }
    }
}

