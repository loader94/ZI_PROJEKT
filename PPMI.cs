using System;
using System.Collections.Generic;
using System.IO;

namespace Projekt_PPMI
{
    class PPMI
    {
        Matrix<int> cooc;
        Matrix<double> ppmi;
        StreamReader coocwacky;
        int ilosc_linii;
        Dictionary d;
        public PPMI()
        {
            coocwacky = new StreamReader("coocwacky");
            String[] line;
            String l;
            int wiersz = 0;

            d = new Dictionary();
            ilosc_linii = d.get_dict_lines();

            cooc = new Matrix<int>(ilosc_linii, ilosc_linii);
            while ((l = coocwacky.ReadLine()) != null)
            {
                line = l.Split('\t');
                for (int i = 0; i < line.Length; i++)
                {
                    cooc.wypelnij_macierz(i, wiersz, Convert.ToInt32(line[i]));
                    //Console.Write(cooc.get_matrix_value(i, wiersz));
                }
                wiersz++;
            }
            coocwacky.Close();

            ppmi = new Matrix<double>(ilosc_linii, ilosc_linii);
        }
        public void create_ppmi()
        {
            if (!File.Exists("ppmiwacky"))
            {
                List<Dictionary> ld = d.create_list();
                int[] wystapienia_x = new int[ilosc_linii];
                int ind = 0;
                foreach (Dictionary temp in ld)
                    wystapienia_x[ind++] = temp.getWystapienia();

                for (int j = 0; j < ilosc_linii; j++)
                {
                    for (int i = 0; i < ilosc_linii; i++)
                    {
                        ppmi.wypelnij_macierz(i, j, 0.0);
                    }
                }
                for (int j = 0; j < ilosc_linii; j++)
                {
                    for (int i = 0; i < ilosc_linii; i++)
                    {
                        double p_xy = 0.0;
                        double p_x = 0.0;
                        int co = cooc.get_matrix_value(i, j);
                        double value = Convert.ToDouble(co);

                        p_xy = value / Convert.ToDouble(ilosc_linii);
                        //Console.WriteLine(p_xy);
                        if (p_xy == 0.0)
                            p_xy = 0.001;

                        p_x = Convert.ToDouble(wystapienia_x[i]) / Convert.ToDouble(ilosc_linii);
                        double val = p_xy / p_x;
                        //Console.WriteLine(wystapienia_x[i]);
                        ppmi.wypelnij_macierz(i, j, Math.Log(val));
                    }
                }
                ld.Clear();
                
            }
            Console.WriteLine("Obliczono macierz ppmi");
        }
        public void save_ppmi_to_file()
        {
            if(!File.Exists("ppmiwacky"))
            {
                StreamWriter newfile = new StreamWriter("ppmiwacky", true);
                for (int j = 0; j < ilosc_linii; j++)
                {
                    for (int i = 0; i < ilosc_linii; i++)
                    {
                        if (i == ilosc_linii - 1)
                        {
                            if (j == ilosc_linii - 1)
                                newfile.Write(ppmi.get_matrix_value(i, j));
                            else
                                newfile.Write(ppmi.get_matrix_value(i, j) + "\n");
                        }
                        else
                        {
                            newfile.Write(ppmi.get_matrix_value(i, j) + "\t");
                        }
                    }
                }
                newfile.Close();
            }
            Console.WriteLine("Zapisano macierz ppmi");
        }
        public void max_ppmi()
        {
            if (File.Exists("ppmiwacky"))
            {
                double max = 0.0;
                int maxi = 0;
                int maxj = 0;
                StreamReader linie_cooc = new StreamReader("ppmiwacky");
                String[] line;
                String l;
                int wiersz = 0;
                int kolumna = 0;
                Matrix<double> temp = new Matrix<double>(ilosc_linii, ilosc_linii);
                while ((l = linie_cooc.ReadLine()) != null)
                {
                    line = l.Split('\t');
                    for (int i = 0; i < line.Length; i++)
                    {
                        temp.wypelnij_macierz(i, wiersz, Convert.ToDouble(line[i]));
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
                    if(tmp.getIndex()==maxi)
                    {
                        Console.WriteLine(tmp.getSlowo());
                    }
                    if(tmp.getIndex()==maxj)
                    {
                        Console.WriteLine(tmp.getSlowo());
                    }
                }
                Console.WriteLine("najwieksze ppmi: " + max);
                ld.Clear();
            }
        }
    }
}
