using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Utility
{
    public static class ConditionalHandler
    {
        public static bool IsNullOrZero(this long a)
        {
            return a == 0;
        }
    }
}
