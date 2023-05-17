using DotNet18_Test1_Milos_Stojic.Help;
using DotNet18_Test1_Milos_Stojic.Model;
using DotNet18_Test1_Milos_Stojic.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic.DAO
{
    public class DAOVoznja
    {
        public static List<Voznja> PreuzmiVoznjuIzSql()
        {
            SqlConnection connection = DaoConnection.NewConnection();

            List<Voznja> sveVoznje = new List<Voznja>();

            string sQuerry = "select id,id_vozilo,id_polazak,id_dolazak,ZavrsenaDN from Voznja";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = (int)rdr["Id"];
                int id_vozila = (int)rdr["id_vozilo"];
                int id_polazak = (int)rdr["id_polazak"];
                int id_dolazak = (int)rdr["id_dolazak"];
                string ZavrsenDN = (string)rdr["ZavrsenaDN"];

                Voznja novaVoznja = new Voznja(id, id_vozila, id_polazak, id_dolazak,ZavrsenDN);
                sveVoznje.Add(novaVoznja);
            }
            rdr.Close();
            connection.Close();
            return sveVoznje;
        }





        public static List<Voznja> PreuzmiVoznjuSlobodnaVozilaIzSql()
        {
            SqlConnection connection = DaoConnection.NewConnection();

            List<Voznja> sveVoznje = new List<Voznja>();

            string sQuerry = "select id,id_vozilo,id_polazak,id_dolazak,ZavrsenaDN from Voznja where ZavrsenaDN=\'D\'";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = (int)rdr["Id"];
                int id_vozila = (int)rdr["id_vozilo"];
                int id_polazak = (int)rdr["id_polazak"];
                int id_dolazak = (int)rdr["id_dolazak"];
                string ZavrsenDN = (string)rdr["ZavrsenaDN"];

                Voznja novaVoznja = new Voznja(id, id_vozila, id_polazak, id_dolazak, ZavrsenDN);
                sveVoznje.Add(novaVoznja);
            }
            rdr.Close();
            connection.Close();
            return sveVoznje;
        }



        public static List<Voznja> PreuzmiVoznjuZauzetaVozilaIzSql()
        {
            SqlConnection connection = DaoConnection.NewConnection();

            List<Voznja> sveVoznje = new List<Voznja>();

            string sQuerry = "select id,id_vozilo,id_polazak,id_dolazak,ZavrsenaDN from Voznja where ZavrsenaDN=\'N\'";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = (int)rdr["Id"];
                int id_vozila = (int)rdr["id_vozilo"];
                int id_polazak = (int)rdr["id_polazak"];
                int id_dolazak = (int)rdr["id_dolazak"];
                string ZavrsenDN = (string)rdr["ZavrsenaDN"];

                Voznja novaVoznja = new Voznja(id, id_vozila, id_polazak, id_dolazak, ZavrsenDN);
                sveVoznje.Add(novaVoznja);
            }
            rdr.Close();
            connection.Close();
            return sveVoznje;
        }


        internal static bool TestDodavanjaVoznje(Voznja nova)
        {
            SqlConnection connection = DaoConnection.NewConnection();

            bool Izvrseno;

            string sQuerry = "insert into Voznja (id_vozilo,id_polazak,id_dolazak,ZavrsenaDN) values (@vozilo,@polazak,@dolazak,\'N\')";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);
            cmd.Parameters.AddWithValue("vozilo", nova.id_vozila);
            cmd.Parameters.AddWithValue("polazak", nova.id_polazak);
            cmd.Parameters.AddWithValue("dolazak", nova.id_dolazak);
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


        internal static bool VoznjaPromenaStatusaTest(int id)
        {
            SqlConnection connection = DaoConnection.NewConnection();

            bool Izvrseno;

            string sQuerry = "update Voznja set zavrsenaDN=\'D\' where id=@id";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);
            cmd.Parameters.AddWithValue("id", id);

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

        public static void VoznjaPromenaStatusa()
        {
            Vozilo zauzeto = VoziloUI.PreuzmiVoziloAkoJeZauzeto();

            bool izvrseno;
            izvrseno = VoznjaPromenaStatusaTest(zauzeto.id);

            if (izvrseno == true)
            {
                Vozilo vo = DAOVozilo.VoziloPreuzmiPoId(zauzeto.id);
                Console.WriteLine("Uspesno promenjen staus vozila {0} {1} ",vo.id,vo.registracija);
            }
            else
            {
                Console.WriteLine("Status vozila nije promenjen.");
            }
            return;
        }

        internal static void VoznjaSacuvajUCsv()
        {
            List<Voznja> sveVoznje = DAOVoznja.PreuzmiVoznjuIzSql();
            Console.WriteLine("\tPregled svih voznji :");
            string csvFilePath = "SpisakVoznji.csv";
            if (File.Exists(csvFilePath))
            {
                File.Delete(csvFilePath);
            }

            foreach (Voznja vo in sveVoznje)
            {
                Vozilo v = DAOVozilo.VoziloPreuzmiPoId(vo.id_vozila);
                Adresa polazak = DAOAdresa.AdresaPreuzmiPoId(vo.id_polazak);
                Adresa dolazak = DAOAdresa.AdresaPreuzmiPoId(vo.id_dolazak);
                string adresaPolaska = polazak.ulica + " " + polazak.broj + " , " + polazak.mesto;
                string adresaDolaska = dolazak.ulica + " " + dolazak.broj + " , " + dolazak.mesto;
                string status = string.Empty;
                if (vo.zavrsenDN == "D")
                {
                    status = "zavrsena";
                }
                else if (vo.zavrsenDN == "N")
                {
                    status = "u toku";
                }
                else
                { return; }
                

                using (StreamWriter writer = new StreamWriter(csvFilePath,true))
                {
                    // Write the header row
                    writer.WriteLine("\tID voznje : {0} , taxi sa registracijom : {1} , polazak sa adrese {2} -- " +
                    "na adresu {3}, status voznje : {4} ", vo.id, v.registracija, adresaPolaska, adresaDolaska, status);
                    writer.Close();
                }
                

            }
            Console.WriteLine();
        }


    }
    
}
