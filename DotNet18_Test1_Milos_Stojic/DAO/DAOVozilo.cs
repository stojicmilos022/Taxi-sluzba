using DotNet18_Test1_Milos_Stojic.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic.DAO
{
    public class DAOVozilo
    {
        public static List<Vozilo> PreuzmiVoziloIzSql()
        {
            SqlConnection connection = DaoConnection.NewConnection();

            List<Vozilo> svaVozila= new List<Vozilo>();

            string sQuerry = "select id,registracija from Vozilo";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = (int)rdr["Id"];
                string registracija = (string)rdr["registracija"];       
                Vozilo novoVozilo = new Vozilo(id, registracija);
                svaVozila.Add(novoVozilo);
            }
            rdr.Close();
            connection.Close();
            return svaVozila;
        }



        public static Vozilo VoziloPreuzmiPoId(int id)
        {
            SqlConnection conn =DaoConnection.NewConnection();

            Vozilo voziloPreuzmi;
            // integrisana i provera dali ima mesta 
            string sQuerry = "select id,registracija from Vozilo where id=@id";
            SqlCommand cmd = new SqlCommand(sQuerry, conn);
            cmd.Parameters.AddWithValue("id", id);

            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                int id_vozila = (int)rdr["id"];
                string registracija = (string)rdr["registracija"];
    
                Vozilo v = new Vozilo(id_vozila,registracija);
                voziloPreuzmi = v;

            }
            else
            {
                voziloPreuzmi = null;
                //Console.WriteLine("Ili nema kursa ili nema mesta na kursu ");
            }
            conn.Close();
            rdr.Close();
            return voziloPreuzmi;
        }

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
                if(v.zavrsenDN=="D")
                {
                    Vozilo vo = DAOVozilo.VoziloPreuzmiPoId(v.id_vozila);
                    Console.WriteLine(" Vozilo sa ID : {0}  registracija vozila : {1} je dostupno ",vo.id,vo.registracija);
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
            DAOVozilo.VoziloIspisiSve();
            Console.WriteLine("Unesite id vozila :");
            string unetiTekst = Console.ReadLine();
            if (int.TryParse(unetiTekst, out userInput) == false)
            {
                Console.WriteLine("Id nije integer");
            }
            else
            {
                userInput = int.Parse(unetiTekst);

                pronadji = VoziloPreuzmiPoId(userInput);

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
            DAOVozilo.VoziloIspisiSvaDostupna();
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

                foreach(Voznja v in slobodno)
                {
                    if (v.id_vozila == userInput)
                    {
                        pronadji = VoziloPreuzmiPoId(userInput);
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
            DAOVozilo.VoziloIspisiSvaZauzeta();
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
                        pronadji = VoziloPreuzmiPoId(userInput);
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
