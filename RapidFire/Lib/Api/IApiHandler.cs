using RapidFireLib.Lib.Core;
using System;
using System.Collections.Generic;
using System.Text;
using static RapidFireLib.Lib.Api.WebApi;

namespace RapidFireLib.Lib.Api
{
    public enum Mode
    {
        Pre = 0,
        Post
    }
    public interface IApiHandler
    {
        object Handle(Mode modePrePost, ProcessType processType, object model, Db db);
    }
}
