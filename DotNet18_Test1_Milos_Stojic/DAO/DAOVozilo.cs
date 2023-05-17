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

        
        public static Vozilo PreuzmiVoziloAkoJeSlobodnoAuto()
        {
            SqlConnection connection = DaoConnection.NewConnection();

            //List<Voznja> sveVoznje = new List<Voznja>();

            Vozilo vozilo = null;
            string sQuerry = "select top (1) id,registracija from Vozilo where id not in (select id_vozilo from voznja where zavrsenaDN=\'N\')";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = (int)rdr["Id"];
                string registracija = (string)rdr["registracija"];


                vozilo = new Vozilo(id, registracija);
            }
            rdr.Close();
            connection.Close();
            return vozilo;
        }
    }
}
