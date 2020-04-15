using Domain.Aggregates;
using RapidFireLib.Lib.Api;
using RapidFireLib.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static RapidFireLib.Lib.Api.WebApi;

namespace Domain.Handlers.Api.Common
{
    public class AssessmentHandler : IApiHandler
    {
        public object Handle(Mode modePrePost, ProcessType processType, object model, Db db)
        {
            var modelConverted = (Assessment)model;
            if (modePrePost == Mode.Pre)
            {
                switch (processType)
                {
                    case ProcessType.Add:
                       // modelConverted.AssessmentId = 0;
                       // modelConverted.AssessmentDetails = modelConverted.AssessmentDetails.Select(x => { x.RowId = 0; return x; }).ToList();
                        break;
                    case ProcessType.Update:
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
    }
}
