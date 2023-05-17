using DotNet18_Test1_Milos_Stojic.DAO;
using DotNet18_Test1_Milos_Stojic.Model;
using DotNet18_Test1_Milos_Stojic.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic.Help
{
    internal class AdresaHelp
    {
        internal static Adresa ProveraUnosaAdresa()
        {
            Adresa novaAdresa = null;
            string ulica, broj, mesto;

            Console.WriteLine("Unesi ime ulice  ");
            ulica = Console.ReadLine();
            Console.WriteLine("Unesi kucni broj ");
            broj = Console.ReadLine();
            Console.WriteLine("Unesi mesto ");
            mesto = Console.ReadLine();

            if (!string.IsNullOrEmpty(ulica) && !string.IsNullOrEmpty(broj) && !string.IsNullOrEmpty(mesto))
            {
                novaAdresa = new Adresa(ulica, broj, mesto);
            }
            else
            {
                Console.WriteLine("Los unos podataka");
            }

            return novaAdresa;
        }

        internal static Adresa ProveraUnosaAdresa(string ulica, string broj, string mesto)
        {
            Adresa novaAdresa = null;


            if (!string.IsNullOrEmpty(ulica) && !string.IsNullOrEmpty(broj) && !string.IsNullOrEmpty(mesto))
            {
                novaAdresa = new Adresa(ulica, broj, mesto);
            }
            else
            {
                Console.WriteLine("Los unos podataka");
            }

            return novaAdresa;
        }

        internal static Adresa ProveriDaliAdresaVecPostoji(string ulica, string broj, string mesto)
        {
            SqlConnection conn = DaoConnection.NewConnection();

            Adresa adresa;

            string sQuerry = @"select id,ulica,broj,mesto from Adresa where ulica=" + "\'" + ulica + "\' and broj=" + "\'" + broj + "\' and Mesto=" + "\'" + mesto + "\'";
            SqlCommand cmd = new SqlCommand(sQuerry, conn);


            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int i = (int)dr[0];
                string u = (string)dr[1];
                string b = (string)dr[2];
                string m = (string)dr[3];
                Adresa adresaNadjena = new Adresa(i, u, b, m);
                adresa = adresaNadjena;

            }
            else
            {
                adresa = null;
            }
            conn.Close();
            dr.Close();
            return adresa;
        }

        internal static Adresa ProveriDaliAdresaVecPostoji(Adresa adresa)
        {
            SqlConnection conn = DaoConnection.NewConnection();

            

            string sQuerry = @"select id,ulica,broj,mesto from Adresa where ulica=" + "\'" + adresa.ulica + "\' and broj=" + "\'" + adresa.broj + "\' and Mesto=" + "\'" + adresa.mesto + "\'";
            SqlCommand cmd = new SqlCommand(sQuerry, conn);


            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int i = (int)dr[0];
                string u = (string)dr[1];
                string b = (string)dr[2];
                string m = (string)dr[3];
                Adresa adresaNadjena = new Adresa(i, u, b, m);
                adresa = adresaNadjena;

            }
            else
            {
                adresa = null;
            }
            conn.Close();
            dr.Close();
            return adresa;
        }

        internal static bool TestDodavanjaAdresa(Adresa novi)
        {
            SqlConnection connection = DaoConnection.NewConnection();

            bool Izvrseno;

            string sQuerry = "insert into Adresa (ulica,broj,mesto) values (@ulica,@broj,@mesto)";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);
            cmd.Parameters.AddWithValue("ulica", novi.ulica);
            cmd.Parameters.AddWithValue("broj", novi.broj);
            cmd.Parameters.AddWithValue("mesto", novi.mesto);
            try
            {
                cmd.ExecuteNonQuery();
                Izvrseno = true;
            }
            catch
            {
                Izvrseno = false;
            }

            connection.Close();
            return Izvrseno;
        }

        public static Adresa PreuzmiAdresuAkoPostoji()
        {
            Adresa pronadji = null;
            int userInput;
            Console.Clear();
            AdresaUI.AdresaIspisiSve();
            Console.WriteLine("Unesite id adrese :");
            string unetiTekst = Console.ReadLine();
            if (int.TryParse(unetiTekst, out userInput) == false)
            {
                Console.WriteLine("Id nije integer");
            }
            else
            {
                userInput = int.Parse(unetiTekst);

                pronadji = AdresaPreuzmiPoId(userInput);

                if (pronadji == null)
                {
                    Console.WriteLine("Nepostojeca adresa");
                }
            }

            return pronadji;
        }

        public static Adresa AdresaPreuzmiPoId(int id)
        {
            SqlConnection conn = DaoConnection.NewConnection();

            Adresa adresaPreuzmi;
            string sQuerry = "select id,ulica,broj,mesto from Adresa where  id=@id";
            SqlCommand cmd = new SqlCommand(sQuerry, conn);
            cmd.Parameters.AddWithValue("id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int i = (int)dr[0];
                string u = (string)dr[1];
                string b = (string)dr[2];
                string m = (string)dr[3];
                Adresa adresaNadjena = new Adresa(i, u, b, m);
                adresaPreuzmi = adresaNadjena;

            }
            else
            {
                adresaPreuzmi = null;
            }
            conn.Close();
            dr.Close();
            return adresaPreuzmi;
        }

        public static Adresa AdresaUnosZaVoznju()
        {
            string ulica, broj, mesto;

            Console.WriteLine("Unesi ime ulice : ");
            ulica = Console.ReadLine();
            Console.WriteLine("Unesi kucni broj : ");
            broj = Console.ReadLine();
            Console.WriteLine("Unesi mesto : ");
            mesto = Console.ReadLine();


            Adresa adresaU = AdresaHelp.ProveraUnosaAdresa(ulica, broj, mesto);
            if (adresaU != null)
            {
                Adresa nadjena = AdresaHelp.ProveriDaliAdresaVecPostoji(adresaU.ulica, adresaU.broj, adresaU.mesto);
                if (nadjena != null)
                {
                    adresaU = nadjena;
                    Console.WriteLine(" Adresa {0} {1} , {2} vec postoji i uneta je pod id brojem : {3} \n Adresa ce biti iskoristena za ovu voznju ", adresaU.ulica, adresaU.broj, adresaU.mesto, adresaU.id);
                }
                else
                {
                    bool uspesno = AdresaHelp.TestDodavanjaAdresa(adresaU);

                    if (uspesno == false)
                    {
                        Console.WriteLine("Greska pri unosu adrese...");
                    }
                    else
                    {
                        Console.WriteLine("Adresa {0} {1}, {2} je uspesno dodata u spisak adresa i bice iskoristena za ovu voznju...\n", adresaU.ulica, adresaU.broj, adresaU.mesto);
                    }
                }
            }
            else
            {

                Console.WriteLine("Problem sa adresom ... proveriti dali su podatci svi uneti ispravno Ulica : {0} Broj : {1} , Mesto {3} ", ulica, broj, mesto);
                
            }
            return adresaU;
        }
    }
}
