using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace GaleriUygulamasi
{
    internal class Program
    {
        // Kullanıcı ile etkileşime gireceğimiz bütün kodlar program sınıfı içinde yazılacak.

        static Galeri OtoGaleri = new Galeri();
        static int hataliGirisSayisi = 0;

        static void Main(string[] args)
        {
            Araba a = new Araba("34ARB3434", "FIAT", 70, "Sedan");
            OtoGaleri.Arabalar.Add(a);
            a.Durum = "Galeride";
            Araba b = new Araba("34ARB3535", "KIA", 60, "SUV");
            OtoGaleri.Arabalar.Add(b);
            a.Durum = "Galeride";
            Araba c = new Araba("34US2342", "OPEL", 50, "Hatchback");
            OtoGaleri.Arabalar.Add(c);
            a.Durum = "Galeride";
            Uygulama();
        }
        static void Uygulama()
        {
            Menu();
            while (true)
            {
                SecimAl();
            }
        }
        static void SecimAl()
        {
            Console.Write("\nSeçiminiz: ");
            string secim = Console.ReadLine().ToUpper();

            switch (secim)
            {
                case "1":
                case "K":
                    ArabaKirala();
                    break;
                case "3":
                case "R":
                    KiradakiArabalarıListele();
                    break;
                case "6":
                case "I":
                    KiralamaIptal();
                    break;
                case "X":
                    Console.WriteLine("Ana menüye dönülüyor...");
                    Menu();
                    break;

                default:
                    hataliGirisSayisi++;
                    if (hataliGirisSayisi >= 10)
                    {
                        Console.WriteLine("10 hatalı giriş yapıldı. Program sonlandırılıyor.");
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    }
                    break;
            }
        }


        static void Menu()
        {
            Console.WriteLine("Galeri Otomasyon                     ");
            Console.WriteLine("1 - Araba Kirala(K)                  ");
            Console.WriteLine("3 - Kiradaki Arabaları Listele(R)    ");
            Console.WriteLine("6 - Kiralama İptali(I)               ");

        }
        static void ArabaKirala()
        {
            Console.WriteLine("-Araba Kirala-");
            Console.WriteLine();
            string plaka = "";
            Araba secilenAraba = null;
            

            while (true)
            {
                Console.Write("Kiralanacak arabanın plakası: ");
                plaka = Console.ReadLine().ToUpper();

                // Plaka format kontrolü
                while (!Regex.IsMatch(plaka, @"^[0-9]{2}[A-Z]{1,3}[0-9]{2,4}$"))
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                   Console.Write("Kiralanacak arabanın plakası: ");
                   plaka = Console.ReadLine().ToUpper();
                }

                // Araba galeride var mı?
                secilenAraba = OtoGaleri.Arabalar.Find(a => a.Plaka == plaka);
                if (secilenAraba == null)
                {
                    Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                    continue;
                }

                // Araba kirada mı?    
                if (secilenAraba.Durum == "Kirada")
                {
                    Console.WriteLine("Araba şu anda kirada. Farklı araba seçiniz.");
                    continue;
                }

                // Kiralama süresi
                Console.Write("Kiralanma süresi: ");
                string saatGiris = Console.ReadLine();
                int sure;
                while (!int.TryParse(saatGiris, out sure) || sure <= 0)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    Console.Write("Kiralanma süresi: ");
                    saatGiris = Console.ReadLine();
                }

                // Kiralama işlemi
                secilenAraba.Durum = "Kirada";
                Console.WriteLine(secilenAraba.Plaka + " plakalı araba " + sure + " saatliğine kiralandı.");
                OtoGaleri.ArabaKirala(plaka, sure);
                break;
            }
        }

        static void KiralamaIptal()
        {
            Console.WriteLine("-Kiralama İptali-");
            Console.WriteLine();
            string plaka = "";
            Araba secilenAraba = null;
            
            while (true)
            {
                Console.Write("Kiralaması iptal edilecek arabanın plakası: ");
                plaka = Console.ReadLine().ToUpper();

                // Plaka format kontrolü
                while (!Regex.IsMatch(plaka, @"^[0-9]{2}[A-Z]{1,3}[0-9]{2,4}$"))
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                    Console.Write("Kiralaması iptal edilecek arabanın plakası: ");
                    plaka = Console.ReadLine().ToUpper();
                }

                // Araba galeride var mı?
                secilenAraba = OtoGaleri.Arabalar.Find(a => a.Plaka == plaka);
                if (secilenAraba == null)
                {
                    Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                    continue;
                }

                // Araba kirada mı? 
                if (secilenAraba.Durum == "Galeride") 
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                    continue;
                }
                else if(secilenAraba.Durum == "Kirada")
                {
                    Console.WriteLine("İptal gerçekleştirildi.");
                    OtoGaleri.KiralamaIptal(plaka);
                    break; ;
                                       
                }
               
            }
            Console.WriteLine();
        }
        static void KiradakiArabalarıListele()
        {
            Console.WriteLine("-Kiradaki Arabalar-");
            Console.WriteLine();
            String Durum = "";
            Araba secilenAraba = null;

            secilenAraba = OtoGaleri.Arabalar.Find(a => a.Durum == "Kirada");
            if (secilenAraba == null)
               
            {
                Console.WriteLine("Listelenecek araç yok.");
                Console.WriteLine();
                 
            }

            Console.WriteLine("Plaka".PadRight(14) + "Marka".PadRight(12) + "K. Bedeli".PadRight(12) + "Araba Tipi".PadRight(12) + "K. Sayısı".PadRight(12) + "Durum");
            Console.WriteLine("----------------------------------------------------------------------");

            foreach (Araba item in OtoGaleri.Arabalar)
            {
                if (item.Durum == "Kirada")
                {
                    Console.WriteLine(item.Plaka.PadRight(14) + item.Marka.PadRight(12) + item.KiralamaBedeli.ToString().PadRight(12) + item.AracTipi.PadRight(12) + item.KiralanmaSayisi.ToString().PadRight(12) + item.Durum);
                }
            }



        }

        //static void ArabaEkle()
        //{
        //    string plaka = "";
        //    string marka = "";
        //    float kiaralamaBedeli = 0;
        //    string aTipi = "";
        //    string arabaTipiSecim = "2";

        //    if (arabaTipiSecim == "2")
        //    {
        //        aTipi = "Hatchback";
        //    }

            
        //}
    }
}
