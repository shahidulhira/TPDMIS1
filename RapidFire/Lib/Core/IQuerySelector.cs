using RapidFireLib.UX.JGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidFireLib.Lib.Core
{
    public interface IQuerySelector
    {
        string Select(JGridRequest packet);
    }
}
