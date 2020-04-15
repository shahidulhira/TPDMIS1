using RapidFireLib.Lib.Api;
using RapidFireLib.Lib.Core;
using RapidFireLib.Lib.Extension;
using System.Collections;
using System.Collections.Generic;
using static RapidFireLib.Lib.Api.WebApi;

namespace Domain.Handlers.Api.Common
{
    public class SyncHandler : IApiHandler
    {
        public object Handle(Mode modePrePost, ProcessType processType, object model, Db db)
        {

            if (modePrePost == Mode.Pre)
            {
                switch (processType)
                {
                    case ProcessType.Add:
                        StatusFieldUpdate(model);
                        break;
                    case ProcessType.Update:
                        StatusFieldUpdate(model);
                        break;
                    case ProcessType.Delete:
                        break;
                    case ProcessType.Get:
                        break;
                    default:
                        break;
                }
            }

            return model;
        }
        private void StatusFieldUpdate(object model)
        {
            var modelType = model.GetType();
            if (model.HasProperty("Status"))
                model.SetPropertyValue("Status", 1);
            var genericTypeProperty = modelType.GetProperties();
            foreach (var item in genericTypeProperty)
            {
                if (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition().Equals(typeof(List<>)))
                {
                    var genericPropertyValues = model.GetPropertyValue(item.Name);
                    foreach (var modelChild in (IList)genericPropertyValues)
                    {
                        if (modelChild.HasProperty("Status"))
                            modelChild.SetPropertyValue("Status", 1);
                    }
                }

            }

        }
    }
}
