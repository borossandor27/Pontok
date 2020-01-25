using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pontok
{
    class Pont 
    {
        int x, y, ssz;
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Ssz { get => ssz; set => ssz = value; }
        public Pont(string ssz, string x, string y)
        {
            this.X = int.Parse(x);
            this.Y = int.Parse(y);
            this.Ssz = int.Parse(ssz);
        }

        public override string ToString()
        {
            return string.Format($"P({ssz,3})={x,3},{y,3}");
        }
    }
}
