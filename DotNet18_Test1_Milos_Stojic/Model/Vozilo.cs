using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic.Model
{
    public class Vozilo
    {
        public int id { get; set; }  
        
        public string registracija { get; set; }

        public Vozilo (int id, string registracija)
        {
            this.id = id;
            this.registracija = registracija;
        }

        public Vozilo(string registracija)
        {
            this.registracija = registracija;
        }

        public override string ToString()
        {
            return "Vozilo sa registarskim oznakama"+registracija;
        }

    }
}
