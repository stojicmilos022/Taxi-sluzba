using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic.Model
{
    public class Voznja
    {
        public int id {  get; set; }
        public int id_vozila { get; set; }
        public int id_polazak { get; set; }

        public int id_dolazak { get; set; }

        public string zavrsenDN { get; set; }



        public Voznja (int id, int id_vozila, int id_polazak, int id_dolazak,string zavrsenDN)
        {
            this.id = id;
            this.id_vozila = id_vozila;
            this.id_polazak=id_polazak;
            this.id_dolazak=id_dolazak;
            this.zavrsenDN = zavrsenDN;
        }
        public Voznja(int id_vozila, int id_polazak,int id_dolazak, string zavrsenDN)
        {

            this.id_vozila = id_vozila;
            this.id_polazak = id_polazak;
            this.id_dolazak = id_dolazak;
            this.zavrsenDN = zavrsenDN;
        }
        /*
        public override string ToString()
        {
            List<Vozilo> vozilo = new List<Vozilo>();
            List<Adresa> adresa = new List<Adresa>();
            return "Vozilo : "+vozilo.;
        }
        */

    }
}
