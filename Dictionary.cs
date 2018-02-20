using System;
using System.Collections.Generic;
using System.IO;

namespace Projekt_PPMI
{
    class Dictionary
    {
        int index;
        String slowo;
        int wystapienia;

        public Dictionary(int i, String s, int w)
        {
            index = i;
            slowo = s;
            wystapienia = w;
        }
        public Dictionary()
        {
            index = 0;
            slowo = null;
            wystapienia = 0;
        }
        public int getIndex()
        {
            return index;
        }
        public String getSlowo()
        {
            return slowo;
        }
        public int getWystapienia()
        {
            return wystapienia;
        }
        public void zwieksz_wystapienie()
        {
            wystapienia++;
        }
        public Dictionary znajdz_wyraz(int index, List<Dictionary> ld)
        {
            Dictionary d = null;
            foreach(Dictionary d_temp in ld)
            {
                if(d_temp.getIndex()==index)
                {
                    d = new Dictionary(d_temp.getIndex(), d_temp.getSlowo(), d_temp.getWystapienia());
                    break;
                }
            }
            
            ld.Clear();
            ld = null;
            return d;
        }
        public int get_dict_lines()
        {
            int lines = 0;
            if (File.Exists("dictwacky"))
            {
                StreamReader dict = new StreamReader("dictwacky");
                while (dict.ReadLine() != null)
                {
                    lines++;
                }
                dict.Close();
                dict.Dispose();
            }
            return lines;
        }
        public void wypisz_wyraz()
        {
            Console.WriteLine("index:" + index + " slowo: " + slowo + " wystapienia: " + wystapienia);
        }
        public List<Dictionary>create_list()
        {
            List<Dictionary> ld = new List<Dictionary>();
            if (File.Exists("dictwacky"))
            {
                File.Copy("dictwacky", "dictwacky_temp2");
                StreamReader dict = new StreamReader("dictwacky_temp2");
                String l;
                String[] line;
                while ((l = dict.ReadLine()) != null)
                {
                    line = l.Split('\t');
                    ld.Add(new Dictionary(Convert.ToInt32(line[0]),line[1], Convert.ToInt32(line[2])));
                }
                dict.Close();
                File.Delete("dictwacky_temp2");
            }
            return ld;
        }
        ~Dictionary()
        {
            //Console.WriteLine("Zniszczono dictionary");
        }
    }
}
