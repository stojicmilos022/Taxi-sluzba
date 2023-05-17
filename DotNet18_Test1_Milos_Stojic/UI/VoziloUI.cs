using DotNet18_Test1_Milos_Stojic.DAO;
using DotNet18_Test1_Milos_Stojic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic.UI
{
    internal class VoziloUI
    {
        public static void VoziloIspisiSve()
        {
            List<Vozilo> svaVozila = DAOVozilo.PreuzmiVoziloIzSql();
            Console.WriteLine("\tSva vozila :");
            foreach (Vozilo v in svaVozila)
            {
                Console.WriteLine(v);
            }
            Console.WriteLine();
        }

        public static void VoziloIspisiSvaDostupna()
        {
            List<Voznja> sveVoznje = DAOVoznja.PreuzmiVoznjuIzSql();
            Console.WriteLine("\tSva vozila :");

            foreach (Voznja v in sveVoznje)
            {
                if (v.zavrsenDN == "D")
                {
                    Vozilo vo = DAOVozilo.VoziloPreuzmiPoId(v.id_vozila);
                    Console.WriteLine(" Vozilo sa ID : {0}  registracija vozila : {1} je dostupno ", vo.id, vo.registracija);
                }
                else
                {
                    continue;
                }
            }

            Console.WriteLine();
        }


        public static void VoziloIspisiSvaZauzeta()
        {
            List<Voznja> sveVoznje = DAOVoznja.PreuzmiVoznjuIzSql();
            Console.WriteLine("\tSva vozila :");

            foreach (Voznja v in sveVoznje)
            {
                if (v.zavrsenDN == "N")
                {
                    Vozilo vo = DAOVozilo.VoziloPreuzmiPoId(v.id_vozila);
                    Console.WriteLine(" Vozilo sa ID : {0}  registracija vozila : {1} je zauzeto ", vo.id, vo.registracija);
                }
                else
                {
                    continue;
                }
            }

            Console.WriteLine();
        }

        public static Vozilo PreuzmiVoziloAkoPostoji()
        {
            Vozilo pronadji = null;
            int userInput;
            Console.Clear();
            VoziloIspisiSve();
            Console.WriteLine("Unesite id vozila :");
            string unetiTekst = Console.ReadLine();
            if (int.TryParse(unetiTekst, out userInput) == false)
            {
                Console.WriteLine("Id nije integer");
            }
            else
            {
                userInput = int.Parse(unetiTekst);

                pronadji = DAOVozilo.VoziloPreuzmiPoId(userInput);

                if (pronadji == null)
                {
                    Console.WriteLine("Nepostojece vozilo");
                }
            }

            return pronadji;
        }

        public static Vozilo PreuzmiVoziloAkoJeSlobodno()
        {
            Vozilo pronadji = null;
            int userInput;
            Console.Clear();
            Console.WriteLine("Izaberi jedno od dostupnih vozila :");
            VoziloIspisiSvaDostupna();
            Console.WriteLine("Unesite id vozila :");
            string unetiTekst = Console.ReadLine();
            if (int.TryParse(unetiTekst, out userInput) == false)
            {
                Console.WriteLine("Id nije integer");
            }
            else
            {

                userInput = int.Parse(unetiTekst);
                List<Voznja> slobodno = DAOVoznja.PreuzmiVoznjuSlobodnaVozilaIzSql();

                foreach (Voznja v in slobodno)
                {
                    if (v.id_vozila == userInput)
                    {
                        pronadji = DAOVozilo.VoziloPreuzmiPoId(userInput);
                    }

                }


                if (pronadji == null)
                {
                    Console.WriteLine("Nepostojece vozilo ili vozilo nije slobodno\n");
                }
            }

            return pronadji;
        }



        public static Vozilo PreuzmiVoziloAkoJeZauzeto()
        {
            Vozilo pronadji = null;
            int userInput;
            Console.Clear();
            Console.WriteLine("Izaberi jedno od zauzetih vozila vozila :");
            VoziloIspisiSvaZauzeta();
            Console.WriteLine("Unesite id vozila :");
            string unetiTekst = Console.ReadLine();
            if (int.TryParse(unetiTekst, out userInput) == false)
            {
                Console.WriteLine("Id nije integer");
            }
            else
            {

                userInput = int.Parse(unetiTekst);
                List<Voznja> zauzeto = DAOVoznja.PreuzmiVoznjuZauzetaVozilaIzSql();

                foreach (Voznja v in zauzeto)
                {
                    if (v.id_vozila == userInput)
                    {
                        pronadji = DAOVozilo.VoziloPreuzmiPoId(userInput);
                    }

                }


                if (pronadji == null)
                {
                    Console.WriteLine("Nepostojece vozilo ili vozilo nije zauzeto\n");
                }
            }

            return pronadji;
        }
    }
}
