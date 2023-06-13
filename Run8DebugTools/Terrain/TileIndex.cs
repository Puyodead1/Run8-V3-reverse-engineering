using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrain
{
    public class TileIndex
    {
        public TileIndex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static TileIndex TileXZZero;

        public int x;

        public int y;
    }
}
