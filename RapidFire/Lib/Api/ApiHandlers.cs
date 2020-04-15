using RapidFireLib.Lib.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RapidFireLib.Lib.Api
{
    public class ApiHandler
    {
        internal List<ApiHandlersPool> ApiHandlersList = new List<ApiHandlersPool>();
        public void Push(object handler, params object[] handlers)
        {
            ApiHandlersList.Add(new ApiHandlersPool((IApiHandler)handler));
            for (int i = 0; i < handlers.Length; i++)
                ApiHandlersList.Add(new ApiHandlersPool((IApiHandler)handlers[i]));

        }

        public class ApiHandlersPool
        {
            public IApiHandler ApiHandler;

            public ApiHandlersPool(IApiHandler apiHandler)
            {
                ApiHandler = apiHandler;
            }
        }
    }
}
