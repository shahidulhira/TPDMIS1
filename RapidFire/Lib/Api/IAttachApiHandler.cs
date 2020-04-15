using System;
using System.Collections.Generic;
using System.Text;

namespace RapidFireLib.Lib.Api
{
    public interface IAttachApiHandler
    {
        void Push(string modelName,ref ApiHandler apiHandler);
        
    }
}
