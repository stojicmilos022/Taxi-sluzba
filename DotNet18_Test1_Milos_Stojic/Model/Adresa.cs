using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic.Model
{
    public class Adresa
    {
        public int id { get; set; }
        public string ulica { get; set; }
        public string broj { get; set; }

        public string mesto { get; set; }

        public Adresa(int id,string ulica,string broj,string mesto)
        {
            this.id = id;
            this.ulica = ulica;
            this.broj = broj;
            this.mesto = mesto;
        }

        public Adresa( string ulica, string broj, string mesto)
        {

            this.ulica = ulica;
            this.broj = broj;
            this.mesto = mesto;
        }
        public override string ToString()
        {
            return "Id: " + id+" Adresa : "+ulica+" "+broj+" , "+mesto ;
        }
    }
}
