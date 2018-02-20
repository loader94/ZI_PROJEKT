using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Projekt_PPMI
{
    class Coocurency_Matrix
    {
        StreamReader splittedwacky;
        int ilosc_linii;
        Matrix<int> m;
        Dictionary d;
        public Coocurency_Matrix()
        {
            d = new Dictionary();
            ilosc_linii = d.get_dict_lines();
            //splittedwacky = new StreamReader("splittedwacky");
            m = new Matrix<int>(ilosc_linii, ilosc_linii);
            for (int j = 0; j < ilosc_linii; j++)
            {
                for (int i = 0; i < ilosc_linii; i++)
                {
                    m.wypelnij_macierz(i, j, 0);
                }
            }
        }

        public void create_cooc()
        {
            if (!File.Exists("coocwacky"))
            {

                bool znalazlem_wyraz_ze_slownika = false;
                List<Dictionary> ld2 = d.create_list();
                List<Dictionary> ld3 = d.create_list();
                foreach (Dictionary ddd in ld2)
                {
                    d = ddd;
                    String nastepne_slowo;
                    String wacky_slowo;
                    splittedwacky = new StreamReader("splittedwacky");
                    while ((wacky_slowo = splittedwacky.ReadLine()) != null)
                    {
                        if (!wacky_slowo.Contains('-'))
                        {
                            wacky_slowo = wacky_slowo.ToLower();
                            if (znalazlem_wyraz_ze_slownika)
                            {
                                znalazlem_wyraz_ze_slownika = false;
                                nastepne_slowo = wacky_slowo;

                                Dictionary d2 = new Dictionary();
                                foreach (Dictionary d_temp in ld3)
                                {
                                    d2 = d_temp;
                                    if (d2.getSlowo().Equals(nastepne_slowo))
                                    {
                                        int i = d.getIndex();
                                        int j = d2.getIndex();
                                        m.wypelnij_macierz(i, j, m.get_matrix_value(i, j) + 1);
                                        //Console.WriteLine("i " + i + " j " + j);
                                        //d.wypisz_wyraz();
                                       // d2.wypisz_wyraz();
                                        break;
                                    }
                                }

                                
                            }
                            if (wacky_slowo.Equals(d.getSlowo()))
                            {
                                znalazlem_wyraz_ze_slownika = true;
                            }

                        }
                    }
                    splittedwacky.Close();
                }

            }
            Console.WriteLine("stworzono macierz kookurencji");
        }

        public int get_max_cooc()
        {
            int max = 0;
            if (File.Exists("coocwacky"))
            {
                StreamReader linie_cooc = new StreamReader("coocwacky");
                String[] line;
                String l;
                int wiersz = 0;
                int kolumna = 0;
                int maxi = 0;
                int maxj = 0;
                Matrix<int> temp = new Matrix<int>(ilosc_linii, ilosc_linii);
                while ((l = linie_cooc.ReadLine()) != null)
                {
                    line = l.Split('\t');
                    for (int i = 0; i < line.Length; i++)
                    {
                        temp.wypelnij_macierz(i, wiersz, Convert.ToInt32(line[i]));
                    }
                    wiersz++;
                    kolumna++;
                }
                Console.WriteLine("wiersze: " + wiersz + " kolumny: " + kolumna);
                linie_cooc.Close();
                for (int j = 0; j < ilosc_linii; j++)
                {
                    for (int i = 0; i < ilosc_linii; i++)
                    {
                        if (temp.get_matrix_value(i, j) > max)
                        {
                            max = temp.get_matrix_value(i, j);
                            maxi = i;
                            maxj = j;
                        }
                    }
                }
                List<Dictionary> ld = d.create_list();
                foreach (Dictionary tmp in ld)
                {
                    if (tmp.getIndex() == maxi)
                    {
                        Console.WriteLine(tmp.getSlowo());
                    }
                    if (tmp.getIndex() == maxj)
                    {
                        Console.WriteLine(tmp.getSlowo());
                    }
                }
                ld.Clear();
            }
            return max;
        }

        public void zapisz_cooc_do_pliku()
        {
            if (!File.Exists("coocwacky"))
            {
                StreamWriter newfile = new StreamWriter("coocwacky", true);
                for (int j = 0; j < ilosc_linii; j++)
                {
                    for (int i = 0; i < ilosc_linii; i++)
                    {
                        if (i == ilosc_linii - 1)
                        {
                            if (j == ilosc_linii - 1)
                                newfile.Write(m.get_matrix_value(i, j));
                            else
                                newfile.Write(m.get_matrix_value(i, j) + "\n");
                        }
                        else
                        {
                            newfile.Write(m.get_matrix_value(i, j) + "\t");
                        }
                    }
                }
                newfile.Close();
            }
            Console.WriteLine("Zapisano macierz do pliku");
        }
    }
}
