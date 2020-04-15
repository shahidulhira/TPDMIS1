using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.IO;

namespace RapidFireLib.Lib.Core
{
    internal static class Cast
    {
        public static View View = new View();

    }

    public class View
    {
        public string GetString(string viewNamePath, object Model = null)
        {
            Http currentHttp = new Http();
            //if (string.IsNullOrEmpty(viewNamePath)) viewNamePath = controller.ControllerContext.ActionDescriptor.ActionName;

            //controller.ViewData.Model = model; ???
            if (Model != null) currentHttp.ViewDataDictionary.Model = Model;

            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    ViewEngineResult viewResult = null;

                    if (viewNamePath.EndsWith(".cshtml"))
                        viewResult = currentHttp.ViewEngine.GetView(viewNamePath, viewNamePath, false);
                    else
                        viewResult = currentHttp.ViewEngine.FindView(currentHttp.ActionContext, viewNamePath, false); //viewResult = viewEngine.FindView(controller.ControllerContext, viewNamePath, false);

                    if (!viewResult.Success)
                        return $"A view with the name '{viewNamePath}' could not be found";

                    ViewContext viewContext = new ViewContext(
                        currentHttp.ActionContext,
                        viewResult.View,
                        currentHttp.ViewDataDictionary,
                        currentHttp.TempDataDictionary,
                        writer,
                        new HtmlHelperOptions()
                    );

                    viewResult.View.RenderAsync(viewContext);

                    return writer.GetStringBuilder().ToString();
                }
                catch (Exception exc)
                {
                    return $"Failed - {exc.Message}";
                }
            }
        }
    }
}
