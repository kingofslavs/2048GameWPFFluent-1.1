using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentUIGame.Resources
{
    public class Tile
    {
        public int Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Tile(int value, int x, int y)
        {
            Value = value;
            X = x;
            Y = y;
        }
    }
}
