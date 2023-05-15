using DotNet18_Test1_Milos_Stojic.DAO;
using DotNet18_Test1_Milos_Stojic.Help;
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
                        DAOAdresa.AdresaUnosNove();
                        break;
                    case 2:
                        DAOVoznja.VoznjaIspisiSve();
                        break;
                    case 3:
                        DAOVoznja.VoznjaKreirajNovuSaUnosom();
                        break;


                    default:
                        Console.WriteLine("Nepoznata komanda");
                        break;
                }
            }
        }
    }
}
