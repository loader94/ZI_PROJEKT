using System;
using System.Collections.Generic;

namespace Projekt_PPMI
{
    class Program
    {
        static void Main(string[] args)
        {
            Shrinker shr = new Shrinker("C:\\Users\\chime\\Documents\\wackypedia_en1", 1000);
            shr.shrink_file();
            //shr.delete_shrinked_file();
            
            Splitter spl = new Splitter("shrinkedwacky");
            spl.split();
            Console.WriteLine("Liczba linii w podzielonym pliku: " + spl.get_splittedwacky_lines());

            Dictionary_creator dc = new Dictionary_creator();
            dc.create_dictionary();
            Dictionary d = new Dictionary();
            Console.WriteLine("Liczba linii w slowniku " + d.get_dict_lines());
            List<Dictionary> ld = d.create_list();
            d = d.znajdz_wyraz(5,ld);//troche pokretnie ale dziala
            d.wypisz_wyraz();

            Coocurency_Matrix cm = new Coocurency_Matrix();
            cm.create_cooc();
            cm.zapisz_cooc_do_pliku();
            Console.WriteLine("Najwieksza wartosc cooc: " + cm.get_max_cooc());

            PPMI p = new PPMI();
            p.create_ppmi();
            p.save_ppmi_to_file();
            p.max_ppmi();
            Console.ReadLine();
        }
    }
}
