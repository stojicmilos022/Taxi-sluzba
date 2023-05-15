using DotNet18_Test1_Milos_Stojic.Help;
using DotNet18_Test1_Milos_Stojic.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic.DAO
{
    public class DAOAdresa
    {
        public static List<Adresa> PreuzmiAdresuIzSql()
        {
            SqlConnection connection = DaoConnection.NewConnection();

            List<Adresa> sveAdrese = new List<Adresa>();

            string sQuerry = "select id,Ulica,Broj,Mesto from adresa";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = (int)rdr["Id"];
                string Ulica = (string)rdr["Ulica"];
                string Broj = (string)rdr["Broj"];
                string Mesto= (string)rdr["Mesto"];
                Adresa novaAdresa = new Adresa(id, Ulica,Broj,Mesto);
                sveAdrese.Add(novaAdresa);
            }
            rdr.Close();
            connection.Close();
            return sveAdrese;
        }

        public static Adresa AdresaPreuzmiPoId(int id)
        {
            SqlConnection conn = DaoConnection.NewConnection();

            Adresa AdresaPreuzmi;
            // integrisana i provera dali ima mesta 
            string sQuerry = "select id,Ulica,Broj,Mesto from adresa where id=@id";
            SqlCommand cmd = new SqlCommand(sQuerry, conn);
            cmd.Parameters.AddWithValue("id", id);

            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                int id_vozila = (int)rdr["id"];
                string Ulica = (string)rdr["Ulica"];
                string Broj = (string)rdr["Broj"];
                string Mesto = (string)rdr["Mesto"];

                Adresa a = new Adresa(id, Ulica, Broj, Mesto); ;
                AdresaPreuzmi = a;

            }
            else
            {
                AdresaPreuzmi = null;
                //Console.WriteLine("Ili nema kursa ili nema mesta na kursu ");
            }
            conn.Close();
            rdr.Close();
            return AdresaPreuzmi;
        }




        public static void AdresaUnosNove()
        {

            Adresa novi = AdresaHelp.ProveraUnosaAdresa();
            if (novi != null)
            {
                Adresa AdresaPostoji = AdresaHelp.ProveriDaliAdresaVecPostoji(novi.ulica, novi.broj,novi.mesto);
                if (AdresaPostoji == null)
                {
                    bool uspesno = AdresaHelp.TestDodavanjaAdresa(novi);

                    if (uspesno == false)
                    {
                        Console.WriteLine("Greska pri unosu clana...");
                    }
                    else
                    {
                        Console.WriteLine("Adresa {0} {1}, {2} je uspesno dodata\n", novi.ulica, novi.broj,novi.mesto);
                    }
                }
                else
                {
                    Console.WriteLine("Adresa {0} {1} , {2} vec postoji uneta je pod Id brojem : {3}\n" +
                        "nije moguce uneti dva puta istog Adresaa....\n", AdresaPostoji.ulica, AdresaPostoji.broj,AdresaPostoji.mesto,AdresaPostoji.id);
                    return;
                }
                return;
            }
        }

        public static void AdresaUnosNove(string ulica,string broj,string mesto)
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

        public static void AdresaIspisiSve()
        {
            List<Adresa> sveAdrese= DAOAdresa.PreuzmiAdresuIzSql();
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
    }
}
