using RapidFireLib.Lib.Core;
using System;

namespace Domain.Handlers.Post.Student
{
    public class EmailParents : IPostDbOperationHandler
    {
        public Result Handle(object model)
        {
            //throw new NotImplementedException();
            return new Result(true, "");
        }
    }
}
