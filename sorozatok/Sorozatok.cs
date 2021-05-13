using System;
using System.Collections.Generic;
using System.IO;


namespace sorozatok
{
    class Program
    {
        

        class Episode
        {
            public string datum;
            public string nev;
            public string episodeNumber;
            public int hossz;
            public bool latta;
            //public Episode(string datum, string nev_, string number_, int hossz_, bool latta)
            //{
            //    this.datum = datum;
            //    nev = nev_;
            //    this.latta = latta;
            //}
            //public Episode() { }
            public int ev()
            {
                return Int32.Parse(datum.Substring(0, 4));
            }
            public int honap()
            {
                return Int32.Parse(datum.Substring(5, 2));
            }
            public int nap()
            {
                return Int32.Parse(datum.Substring(8, 2));
            }
            public Episode(string datum, string nev, string episodeNumber, int hossz, bool latta)
            {
                this.datum = datum;
                this.nev = nev;
                this.episodeNumber = episodeNumber;
                this.hossz = hossz;
                this.latta = latta;
            }
        }
        class Epistat
        {
            public int osszido = 0;
            public int episodes = 0;
            public void Process(Episode ep)
            {
                osszido += ep.hossz;
                episodes++;
            }
        }
        #region 6.feladat
        public static string Hetnapja(int ev, int ho, int nap)
        {
            string[] napok = { "v", "h", "k", "sze", "cs", "p", "szo", };
            int[] honapok = { 0, 3, 2, 5, 0, 3, 5, 1, 4, 6, 2, 4 };
            if (ho < 3) ev--;
            return napok[(ev + ev / 4 - ev / 100 + ev / 400 + honapok[ho - 1] + nap) % 7];
        }
        #endregion
        static void Main(string[] args)
        {
            #region 1.feladat           
            StreamReader file = new StreamReader("../../../lista.txt");
            bool readlinesuccesful = true;
            List<Episode> epizodadat = new List<Episode>();
            
            while (readlinesuccesful)
            {
                string line = file.ReadLine();
                if (line == null)
                {
                    readlinesuccesful = false;
                    break;
                }
                //Episode ep = new Episode();
                //ep.datum = line;
                //line = file.ReadLine();
                //ep.nev = line;
                //line = file.ReadLine();
                //ep.episodeNumber = line;
                //line = file.ReadLine();
                //ep.hossz = Int32.Parse(line);
                //line = file.ReadLine();
                //ep.latta = (Int32.Parse(line) == 0) ? false : true;
                //epizodadat.Add(ep);
                string cDatum = line;
                line = file.ReadLine();
                string cNev = line;
                line = file.ReadLine();
                string cEpisodeNumber = line;
                line = file.ReadLine();
                int cHossz = Int32.Parse(line);
                line = file.ReadLine();
                bool cLatta = Int32.Parse(line)!=0 ? true : false;
                epizodadat.Add(new Episode(cDatum, cNev, cEpisodeNumber, cHossz, cLatta));
            }
            file.Close();
            #endregion
            #region 2.feladat
            Console.WriteLine("2. feladat");
            int ismertdatum = 0;
            for (int i = 0; i < epizodadat.Count; i++)
            {
                if (epizodadat[i].datum != "NI")
                {
                    ismertdatum++;
                }
            }
            Console.WriteLine($"A listában {ismertdatum} db vetítési dátummal rendelkező epizód van.");
            Console.Write('\n');
            #endregion
            #region 3.feladat
            Console.WriteLine("3.feladat");
            int latta = 0;
            int osszesido = 0;
            for (int i = 0; i < epizodadat.Count; i++)
            {
                if (epizodadat[i].latta)
                {
                    latta++;
                    osszesido += epizodadat[i].hossz;
                }
            }

            Console.WriteLine($"A listában lévő epizódok { /*Math.Round(*/Convert.ToDouble(latta * 100) / epizodadat.Count/*, 2)*/:f2} % -át látta.");
            Console.Write('\n');
            #endregion
            #region 4.feladat
            Console.WriteLine("4.feladat");
            //int perc = osszesido % 60;
            //int ora_m = osszesido / 60;
            //int nap_m = ora_m / 24;
            //ora_m = ora_m % 24;

            int nap = osszesido / 60 / 24;
            int ora = osszesido / 60 - (nap * 24);
            Console.WriteLine($"Sorozatnézéssel {nap} napot {ora} órát és {osszesido - (ora * 60) - (nap * 24 * 60)} percet töltött.");
            Console.Write('\n');
            #endregion
            #region 5.feladat
            Console.WriteLine("5.feladat");
            Console.Write("Adjon meg egy dátumot! Dátum= ");
            string adottdatum = Console.ReadLine();
            for (int i = 0; i < epizodadat.Count; i++)
            {
                if (string.Compare(epizodadat[i].datum, adottdatum) <= 0 && !epizodadat[i].latta)
                {
                    Console.WriteLine($"{epizodadat[i].episodeNumber}\t{epizodadat[i].nev}");
                }
            }
            Console.Write('\n');
            #endregion
            #region 7.feladat
            Console.WriteLine("7.feladat");
            Console.Write("Adja meg a hét egy napját(például cs)! Nap =");
            string adottnap = Console.ReadLine();
            bool volte = false;
            HashSet<string> epikus = new HashSet<string>();
            for (int i = 0; i < epizodadat.Count; i++)
            {
                var epi = epizodadat[i];
                if (epi.datum != "NI" && Hetnapja(epi.ev(), epi.honap(), epi.nap()) == adottnap 
                    && epikus.Add(epi.nev))
                {
                    Console.WriteLine(epi.nev);
                    volte = true;
                }
            }
            if (!volte)
            {
                Console.WriteLine("Az adott napon nem kerül adásba sorozat.");
            }
            Console.Write('\n');
            #endregion
            #region 8.feladat
            Dictionary<string, Epistat> episodefile = new Dictionary<string, Epistat>();
            foreach (var ad in epizodadat)
            {
                if (!episodefile.ContainsKey(ad.nev))
                {
                    episodefile.Add(ad.nev, new Epistat());
                }
                episodefile[ad.nev].Process(ad);
            }
            StreamWriter summa = new StreamWriter("../../../summa.txt");
            foreach (var kv in episodefile)
            {
                summa.WriteLine($"{kv.Key} {kv.Value.osszido} {kv.Value.episodes}");
            }
            summa.Close();            
            #endregion
        }
    }
}
