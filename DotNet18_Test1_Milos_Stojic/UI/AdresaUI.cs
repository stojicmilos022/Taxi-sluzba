﻿using DotNet18_Test1_Milos_Stojic.DAO;
using DotNet18_Test1_Milos_Stojic.Help;
using DotNet18_Test1_Milos_Stojic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic.UI
{
    internal class AdresaUI
    {
        public static void AdresaIspisiSve()
        {
            List<Adresa> sveAdrese = DAOAdresa.PreuzmiAdresuIzSql();
            Console.WriteLine("\tSvi kursevi u skoli :");
            Console.WriteLine("\t_____________________________________________________________________________________________________________");
            Console.WriteLine("\t{0,-4} | {1,-25} | {2,-15} | {3,-15} | {4,-15} | {5,-15}", "Id", "Naziv", "Pohadja. ucenika", "Max ucenika", "strani jezik", "AktivanDN");
            Console.WriteLine("\t_____________________________________________________________________________________________________________");
            foreach (Adresa a in sveAdrese)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine();
        }

        public static void AdresaUnosNove()
        {

            Adresa novi = AdresaHelp.ProveraUnosaAdresa();
            if (novi != null)
            {
                Adresa AdresaPostoji = AdresaHelp.ProveriDaliAdresaVecPostoji(novi.ulica, novi.broj, novi.mesto);
                if (AdresaPostoji == null)
                {
                    bool uspesno = AdresaHelp.TestDodavanjaAdresa(novi);

                    if (uspesno == false)
                    {
                        Console.WriteLine("Greska pri unosu clana...");
                    }
                    else
                    {
                        Console.WriteLine("Adresa {0} {1}, {2} je uspesno dodata\n", novi.ulica, novi.broj, novi.mesto);
                    }
                }
                else
                {
                    Console.WriteLine("Adresa {0} {1} , {2} vec postoji uneta je pod Id brojem : {3}\n" +
                        "nije moguce uneti dva puta istog Adresaa....\n", AdresaPostoji.ulica, AdresaPostoji.broj, AdresaPostoji.mesto, AdresaPostoji.id);
                    return;
                }
                return;
            }
        }
    }
}
