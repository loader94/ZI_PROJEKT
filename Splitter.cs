using System.Text.RegularExpressions;
using System;
using System.IO;

namespace Projekt_PPMI
{
    class Splitter
    {
        Regex r;
        StreamReader shrinkedwacky;

        public Splitter()
        {
            shrinkedwacky = new StreamReader("shrinkedwacky");
            r = new Regex("^[a-zA-Z0-9]*$");
        }
        public Splitter(String shrinkedwacky_location)
        {
            shrinkedwacky = new StreamReader(shrinkedwacky_location);
            r = new Regex("^[a-zA-Z0-9]*$");
        }

        void close_splitter()
        {
            shrinkedwacky.Close();
            shrinkedwacky.DiscardBufferedData();
            Console.WriteLine("Splitter zamkniety");
        }
        public void split()
        {
            String poczatek_zdania = "<s>";
            String koniec_zdania = "</s>";
            int cnt = 0;

            String line;
            String[] lines;

            bool zapisywanie = false;
            bool pierwszy_wiersz = false;

            if (!File.Exists("splittedwacky"))
            {
                /////---numer dokumentu/////////slowa w dokumencie//////////
                StreamWriter newfile = new StreamWriter("splittedwacky");
                while ((line = shrinkedwacky.ReadLine()) != null)
                {
                    lines = line.Split('\t');
                    if (zapisywanie)
                    {
                        if (line.Contains(koniec_zdania))
                        {
                            zapisywanie = false;
                            pierwszy_wiersz = false;
                            cnt++;
                        }
                        if (pierwszy_wiersz)
                        {
                            newfile.WriteLine("---" + cnt.ToString());
                            pierwszy_wiersz = false;
                        }
                        else
                        {
                            if (zapisywanie && !line.Contains(poczatek_zdania) && !line.Contains(koniec_zdania))
                            {
                                if (r.IsMatch(lines[1]))
                                {
                                    newfile.WriteLine(lines[1]);
                                }
                            }
                        }
                    }
                    if (line.Contains(poczatek_zdania))
                    {
                        zapisywanie = true;
                        pierwszy_wiersz = true;
                    }
                }
                newfile.Close();
            }
            Console.WriteLine("plik podzielony");
            close_splitter();
        }
        public int get_splittedwacky_lines()
        {
            int lines = 0;
            if(File.Exists("splittedwacky"))
            {
                StreamReader temp = new StreamReader("splittedwacky");
                while (temp.ReadLine() != null)
                    lines++;
                temp.Close();
                temp.Dispose();
            }
            return lines;
        }
        ~Splitter()
        {
            Console.WriteLine("Splitter zniszczony");
        }
    }
}
