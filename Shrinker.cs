using System;
using System.IO;

namespace Projekt_PPMI
{
    class Shrinker
    {
        StreamReader wacky;
        int cnt;
        public Shrinker(String wackypedia_location, int ilosc_dokumentow)
        {
            wacky = new StreamReader(wackypedia_location);
            cnt = ilosc_dokumentow;
        }
        public Shrinker()
        {
            wacky = new StreamReader("C:\\Users\\chime\\Documents\\wackypedia_en1");
            cnt = 1000;
        }
        public void shrink_file()
        {
            String koniec_zdania = "</s>";
            int doccnt = 0; //licznik dokumentow
            String line;
            if (!File.Exists("shrinkedwacky"))
            {
                StreamWriter newfile = new StreamWriter("shrinkedwacky", true);
                newfile.Close();
                while ((line = wacky.ReadLine()) != null)
                {
                    if (doccnt == cnt)
                    {
                        newfile.Close();
                        newfile.Dispose();
                        break;
                    }
                    if (line.Contains(koniec_zdania))
                        doccnt++;
                    File.AppendAllText("shrinkedwacky", line + Environment.NewLine);
                }

            }
            Console.WriteLine("Wackypedia skurczona do " + cnt + " artykulow");
            close_shrinker();
        }

        public void delete_shrinked_file()
        {
            if (File.Exists("shrinkedwacky"))
            {
                File.Delete("shrinkedwacky");
                Console.WriteLine("Usunieto shrinkedwacky");
            }
        }
        void close_shrinker()
        {
            wacky.Close();
            wacky.DiscardBufferedData();
            Console.WriteLine("Shrinker zamkniety");
        }
        ~Shrinker()
        {
            Console.WriteLine("Shrinker zniszczony");
        }
    }
}
