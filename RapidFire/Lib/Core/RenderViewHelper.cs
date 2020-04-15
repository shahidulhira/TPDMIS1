using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RapidFireLib.Lib.Core
{
    //Important Method That Can be Use in Several Need 
    public class HttpContextItems
    {
        public HttpContextItems()
        {
            HttpContextAccessor = new HttpContextAccessor();
            DefaultHttpContext = new DefaultHttpContext { RequestServices = HttpContextAccessor.HttpContext.RequestServices };
            ControllerContext = new ControllerContext() { HttpContext = DefaultHttpContext };
            ActionContext = new ActionContext(DefaultHttpContext, new RouteData(), new ActionDescriptor());
        }
        public HttpContextAccessor HttpContextAccessor { get; set; }
        public DefaultHttpContext DefaultHttpContext { get; set; }
        public ActionContext ActionContext { get; set; }
        public ControllerContext ControllerContext { get; set; }
    }
    public class RenderViewHelper
    {
        public Tuple<string, HttpContextItems> CoreItemsForRenderView(string viewName)
        {
            var httpContextItems = new HttpContextItems();
            return new Tuple<string, HttpContextItems>(string.IsNullOrEmpty(viewName) ? httpContextItems.ControllerContext.ActionDescriptor.ActionName : viewName, httpContextItems);
        }
        public async Task<string> ViewHtmlGenerator(string viewName, HttpContextItems httpContextItems,bool partial = false)
        {
            using (StringWriter writer = new StringWriter())
            {
                IViewEngine viewEngine = httpContextItems.ControllerContext.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(httpContextItems.ActionContext, viewName, !partial);
                if (viewResult.Success == false)
                    return $"A view with the name {viewName} could not be found";
                await viewResult.View.RenderAsync(new ViewContext(
                    httpContextItems.ActionContext,
                    viewResult.View,
                    new ViewDataDictionary((IModelMetadataProvider)httpContextItems.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(IModelMetadataProvider)), httpContextItems.ActionContext.ModelState),
                     new TempDataDictionary(httpContextItems.DefaultHttpContext, new TempDataProvider()),
                    writer,
                    new HtmlHelperOptions()
                ));
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
