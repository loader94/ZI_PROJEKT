using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Projekt_PPMI
{
    class Dictionary_creator
    {
        Stopwords sw;
        StreamReader splittedwacky;
        public Dictionary_creator()
        {
            sw = new Stopwords();
            splittedwacky = new StreamReader("splittedwacky");
        }

        public void create_dictionary()
        {
            if (!File.Exists("dictwacky"))
            {
                if (File.Exists("splittedwacky"))
                {
                    List<Dictionary> d = new List<Dictionary>();
                    String line;
                    bool stopword = false;
                    bool indict = false;
                    StreamWriter newfile = new StreamWriter("dictwacky", true);
                    newfile.Close();
                    int index_slowa = 0;
                    while ((line = splittedwacky.ReadLine()) != null)
                    {
                        stopword = false;
                        indict = false;
                        if (line != string.Empty || line != "")//jak linia niepusta
                        {
                            if (!line.Contains('-'))//jak nie jest numerem dokumentu
                            {
                                line = line.ToLower();

                                foreach (String s in sw.stopwords)//czy jest stopwordem
                                {
                                    if (s.Equals(line))
                                    {
                                        stopword = true;
                                    }
                                }//stopwprd check
                                if (!stopword)//jak nie stopword
                                {
                                    if (d.Count == 0)// jak pusta lista
                                    {
                                        //////INDEX/SLOWO/WYSTAPIENIA/////
                                        d.Add(new Dictionary(index_slowa, line, 1));
                                        index_slowa++;
                                    }
                                    else// jak niepusta lista
                                    {
                                        if (d.Exists(x => x.getSlowo().Equals(line)))//czy slowo w slowniku
                                        {
                                            indict = true;
                                        }
                                        if (indict)
                                        {
                                            int pos = d.FindIndex(x => x.getSlowo().Equals(line));
                                            d.ElementAt(pos).zwieksz_wystapienie();
                                        }
                                        else
                                        {
                                            d.Add(new Dictionary(index_slowa, line, 1));
                                            index_slowa++;
                                        }
                                    }//czy slowo w slowniku
                                }//niestopword
                            }//niepusta
                        }//nr dokumentu if
                    }
                    splittedwacky.Close();
                    for (int i = 0; i < d.Count; i++)//zapis do pliku
                    {
                        String linia = d.ElementAt(i).getIndex().ToString() + "\t" + d.ElementAt(i).getSlowo() + "\t" + d.ElementAt(i).getWystapienia().ToString() + "\n";
                        if (i == d.Count - 1)
                        {
                            linia = d.ElementAt(i).getIndex().ToString() + "\t" + d.ElementAt(i).getSlowo() + "\t" + d.ElementAt(i).getWystapienia().ToString();
                        }
                        File.AppendAllText("dictwacky", linia);
                    }
                    d.Clear();
                }

            }
            Console.WriteLine("stworzono slownik");
            close_dict_creator();
        }
        void close_dict_creator()
        {
            sw.clear_stopwords();
            splittedwacky.Close();
            splittedwacky.DiscardBufferedData();
            Console.WriteLine("Zamknieto kreator slownika");
        }
        ~Dictionary_creator()
        {
            Console.WriteLine("zniszczono dictionary creator");
        }
    }
}
