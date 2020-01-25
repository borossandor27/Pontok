using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pontok
{
    class Program
    {
        static List<Pont> pontok = new List<Pont>();
        static List<Multi_Pont> azonosak = new List<Multi_Pont>();
        static Pont[] max = new Pont[2];
        static void Main(string[] args)
        {
            Beolvas();
            Console.WriteLine($"\n1. feladat: Pontok száma a pontok.txt állományban: {pontok.Count} db");
            //-- 2. feladat ------------------------------------
            int tengelyen = pontok.Where(p => p.X == 0 || p.Y == 0).Count(); 
            Console.WriteLine($"\n2. feladat: Pontok száma az x vagy y tengelyen: {tengelyen} db");
            
            //-- 3. feladat ------------------------------------
            Console.WriteLine($"\n3. feladat: Azonos koordinátájú pontok:");
            Feladat03_hagyomanyos();
            foreach (var item in azonosak)
            {
                Console.WriteLine($"\tAz x={item.X,3} y={item.Y,3} koordinátán: {item.Elofordulas}");
            }

            Feladat03_lambda();

            //-- 4. feladat ------------------------------------
            Console.WriteLine($"\n4. feladat: Leghosszabb szakasz hossza: {Legnagyobb_Tavolsag()}");

            //-- 5. feladat -------------------------------------
            using (StreamWriter sw = new StreamWriter("max_hossz.txt"))
            {
                foreach (var item in max)
                {
                    Console.WriteLine(item);
                    sw.WriteLine(item);
                }
            }
            Console.WriteLine("Program vége!");
            Console.ReadKey();
        }
        static void Beolvas()
        {
            Console.WriteLine("Adatok beolvasása...");

            string forras = @"../../pontok.txt";
            try
            {
                using (StreamReader sr = new StreamReader(forras))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        pontok.Add(new Pont(line.Substring(2, 3).Trim(), line.Substring(7, 3).Trim(), line.Substring(11, 3).Trim()));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            Console.WriteLine("Beolvasás vége!");
        }

        static void Feladat03_hagyomanyos()
        {
            //-- Ismétlődő pontok keresése --------------------------------------
            for (int i = 0; i < pontok.Count; i++)
            {
                int ismetlodik = -1;
                //-- a pontokat egyesével vizsgáljuk ----------------------------
                for (int j = i+1; j < pontok.Count; j++)
                {
                    //--- Az összes őt követő potot megvizsgáljuk, megegyezik-e vele
                    if (pontok[i].X == pontok[j].X && pontok[i].Y == pontok[j].Y)
                    {
                        //-- megegyezik valamelyik őt követővel -----------------------
                        ismetlodik = j;
                        //-- nem keresünk további egyezést!
                        break;
                    }
                }
                if (ismetlodik>=0)
                {
                    Multi_Pont multi = azonosak.Find(p => p.X == pontok[i].X && p.Y == pontok[i].Y);
                    if (multi != null)
                    {
                        multi.Ujabb(i, ismetlodik);
                    }
                    else
                    {
                        azonosak.Add(new Multi_Pont(pontok[i].X, pontok[i].Y, 2, string.Format($"{i + 1,3}. {ismetlodik + 1,3}.")));
                    }
                    
                }
            }
        }
        static void Feladat03_lambda()
        {
            Console.WriteLine("\nLINQ és lambda");
            var query = pontok.GroupBy(p => new { p.X, p.Y })
                .Select(q => new {q.Key.X, q.Key.Y, db = q.Count() })
                .Where(r => r.db > 1);
            foreach (var item in query)
            {
                var m = pontok.FindAll(p => p.X == item.X && p.Y == item.Y).Select(r => r.Ssz).ToArray();
                //string s = "";
                //foreach (var q in m)
                //{
                //    s += string.Format($" {q.Ssz}.");
                //}
                Console.WriteLine($"\tAz x={item.X,3} y={item.Y,3} koordinátán: {string.Join(". ", m)}.");

            }
        }

        static double Legnagyobb_Tavolsag()
        {
            double d = 0;
            for (int i = 0; i < pontok.Count; i++)
            {
                max[0] = pontok[i];
                for (int j = i+1; j < pontok.Count; j++)
                {
                    double tavolsag = Math.Sqrt(Math.Pow(pontok[j].X - pontok[i].X, 2.0) + Math.Pow(pontok[j].Y - pontok[i].Y, 2.0));
                    if (tavolsag > d)
                    {
                        d = tavolsag;
                        max[1] = pontok[j];
                    }
                }
            }
            return d;
        }
    }
}
