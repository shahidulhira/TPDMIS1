using Domain.Aggregates;
using RapidFireLib.Lib.Core;
using System;

namespace Domain.Handlers.Pre
{
    public class UpdateCommonFields : IPreDbOperationHandler
    {
        public object Handle(object obj)
        {
            var student = (Student)obj;
            student.ModifiedBy = "1";
            student.ModifiedDate= DateTime.Now;
            return student;
        }
    }
}
