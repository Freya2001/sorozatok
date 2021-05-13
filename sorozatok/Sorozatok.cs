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
            public Episode(string datum, string nev, string episodeNumber, int hossz, bool latta)
            {
                this.datum = datum;
                this.nev = nev;
                this.episodeNumber = episodeNumber;
                this.hossz = hossz;
                this.latta = latta;
            }
        }
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
                bool cLatta = (Int32.Parse(line) == 0) ? false : true;
                epizodadat.Add(new Episode(cDatum, cNev, cEpisodeNumber, cHossz, cLatta));
            }
            #endregion
            #region 2.feladat
            #endregion
            #region 3.feladat
            #endregion
            #region 4.feladat
            #endregion
            #region 5.feladat
            #endregion
            #region 6.feladat
            #endregion
            #region 7.feladat
            #endregion
            #region 8.feladat
            #endregion
        }
    }
}
