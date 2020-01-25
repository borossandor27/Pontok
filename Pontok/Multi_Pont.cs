using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pontok
{
    class Multi_Pont
    {
        int x, y, db = 0;
        string elofordulas;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int Db { get => db; set => db = value; }
        public string Elofordulas { get => elofordulas; set => elofordulas = value; }

        public Multi_Pont(int x, int y, int db, string elofordulas)
        {
            X = x;
            Y = y;
            Db = db;
            Elofordulas = elofordulas;
        }

        public void Ujabb(int ssz1, int ssz2)
        {
            db++;
            elofordulas += string.Format($" {ssz2,3}.");
        }
    }
}
