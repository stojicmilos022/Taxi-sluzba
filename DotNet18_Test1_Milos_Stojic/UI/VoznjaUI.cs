using DotNet18_Test1_Milos_Stojic.DAO;
using DotNet18_Test1_Milos_Stojic.Help;
using DotNet18_Test1_Milos_Stojic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet18_Test1_Milos_Stojic.UI
{
    public class VoznjaUI
    {
        internal static void VoznjaKreirajNovuSaUnosom()
        {
            Console.WriteLine("Unisi adresu za polaznu tacku :");
            Adresa polazak = AdresaHelp.AdresaUnosZaVoznju();
            Adresa pocetak = AdresaHelp.ProveriDaliAdresaVecPostoji(polazak);

            Console.WriteLine("Unisi adresu za destinaciju :");
            Adresa dolazak = AdresaHelp.AdresaUnosZaVoznju();
            Adresa kraj = AdresaHelp.ProveriDaliAdresaVecPostoji(dolazak);

            Vozilo vozilo = VoziloUI.PreuzmiVoziloAkoJeSlobodno();

            string status = "N";

            Voznja novaVoznja = new Voznja(vozilo.id, pocetak.id, kraj.id, status);

            bool uspesno = DAOVoznja.TestDodavanjaVoznje(novaVoznja);

            if (uspesno == false)
            {
                Console.WriteLine("Greska pri unosu voznje...");
            }
            else
            {
                Console.WriteLine("Voznja sa adrese : {0} {1}, {2} sa vozilom : {6} {7} \n" +
                    "na adresu {3} {4}, {5} je uspesno dodata\n", pocetak.ulica, pocetak.broj, pocetak.mesto, kraj.ulica, kraj.broj, kraj.mesto,vozilo.id,vozilo.registracija);
            }
            return;
        }
        internal static void VoznjaKreirajNovuBezIzboraVozila()
        {
            Console.WriteLine("Unisi adresu za polaznu tacku :");
            Adresa polazak = AdresaHelp.AdresaUnosZaVoznju();
            Adresa pocetak = AdresaHelp.ProveriDaliAdresaVecPostoji(polazak);

            Console.WriteLine("Unisi adresu za destinaciju :");
            Adresa dolazak = AdresaHelp.AdresaUnosZaVoznju();
            Adresa kraj = AdresaHelp.ProveriDaliAdresaVecPostoji(dolazak);

            Vozilo vozilo = DAOVozilo.PreuzmiVoziloAkoJeSlobodnoAuto();

            string status = "N";

            Voznja novaVoznja = new Voznja(vozilo.id, pocetak.id, kraj.id, status);

            bool uspesno = DAOVoznja.TestDodavanjaVoznje(novaVoznja);

            if (uspesno == false)
            {
                Console.WriteLine("Greska pri unosu voznje...");
            }
            else
            {
                Console.WriteLine("Voznja sa adrese : {0} {1}, {2}  sa vozilom : {6} {7}\n" +
                    "na adresu {3} {4}, {5} je uspesno dodata\n", pocetak.ulica, pocetak.broj, pocetak.mesto, kraj.ulica, kraj.broj, kraj.mesto, vozilo.id, vozilo.registracija);
            }
            return;
        }

        public static void VoznjaIspisiSve()
        {

            List<Voznja> sveVoznje = DAOVoznja.PreuzmiVoznjuIzSql();
            Console.WriteLine("\tPregled svih voznji :");

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
                Console.WriteLine("\tID voznje : {0} , taxi sa registracijom : {1} , polazak sa adrese {2} --\n " +
                    "na adresu {3}, status voznje : {4} ", vo.id, v.registracija, adresaPolaska, adresaDolaska, status);
            }
            Console.WriteLine();
        }
    }
}
