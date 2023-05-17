using DotNet18_Test1_Milos_Stojic.DAO;
using DotNet18_Test1_Milos_Stojic.Help;
using DotNet18_Test1_Milos_Stojic.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic
{
    public class MainMenu
    {
        public static void MMenuUI()
        {
            int odluka = -1;
            while (odluka != 0)
            {
                Console.WriteLine("Taxi sluzba \n");
                Console.WriteLine("Odaberi opciju za rad :\n");
                Console.WriteLine("\t1. Unos nove adrese u sistem :");
                Console.WriteLine("\t2. Pregled svih voznji sa dodeljenim adresama i voznjama:");
                Console.WriteLine("\t3. Kreiranje nove voznje :");
                Console.WriteLine("\t4. Zavrsi voznju  :");
                Console.WriteLine("\t5. Ispisi sva slobodna vozila :");
                Console.WriteLine("\t6. Sacuvaj u csvFajl :");
                Console.WriteLine("\t7. Kreiranje nove voznje bez biranja vozila :");




                Console.WriteLine("\t0. Izlaz iz programa...");
                Console.WriteLine("Izaberi jednu od opcija :");
                odluka = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz iz programa");
                        break;
                    case 1:
                        AdresaUI.AdresaUnosNove();
                        break;
                    case 2:
                        VoznjaUI.VoznjaIspisiSve();
                        break;
                    case 3:
                        VoznjaUI.VoznjaKreirajNovuSaUnosom();
                        break;
                    case 4:
                        DAOVoznja.VoznjaPromenaStatusa();
                        break;
                    case 5:
                        VoziloUI.VoziloIspisiSvaDostupna();
                        break;
                    case 6:
                        DAOVoznja.VoznjaSacuvajUCsv();
                        break;
                    case 7:
                        VoznjaUI.VoznjaKreirajNovuBezIzboraVozila();
                        break;
                    default:
                        Console.WriteLine("Nepoznata komanda");
                        break;
                }
            }
        }
    }
}
