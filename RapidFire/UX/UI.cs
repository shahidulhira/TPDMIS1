using Microsoft.EntityFrameworkCore;
using RapidFireLib.Lib.Core;
using RapidFireLib.UX.JGrid;

namespace RapidFireLib.UX
{
    public class UI
    {
        public JGridHelper JGrid;
        public UI(IConfig config)
        {
            JGrid = new JGridHelper(config);
        }
    }
}
