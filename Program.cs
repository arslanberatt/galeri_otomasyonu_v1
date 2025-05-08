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
        Uygulama();
    }
    static void Uygulama()
    {
        Menu();
        SahteVeriEkle();

        while (true)
        {
            SecimAl();
        }
    }
    static void SecimAl()
    {
        Console.Write("\nSeçiminiz: ");
        string secim = Console.ReadLine().ToUpper();
        Console.WriteLine();

        switch (secim)
        {
            case "K":
            case "1":
                ArabaKirala();
                break;

            case "T":
            case "2":
                ArabaTeslimAl();
                break;

            case "R":
            case "3":
                KiradakiArabalarıListele();
                break;

            case "M":
            case "4":
                GaleridekiArabalariListele();
                break;

            case "A":
            case "5":
                TumArabalariListele();
                break;

            case "I":
            case "6":
                KiralamaIptal();
                break;

            case "Y":
            case "7":
                ArabaEkle();
                break;

            case "S":
            case "8":
                ArabaSil();
                break;

            case "G":
            case "9":
                BilgileriGoster();
                break;

            case "X":
                return;
            default:
                hataliGirisSayisi++;
                if (hataliGirisSayisi >= 10)
                {
                    Console.WriteLine("Üzgünüm sizi anlayamıyorum. Program sonlandırılıyor.");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Hatalı işlem gerçekleştirildi. Tekrar deneyin.");
                }
                break;
        }
    }

    static void Menu()
    {
        Console.WriteLine("Galeri Otomasyon                     ");
        Console.WriteLine("1 - Araba Kirala(K)                  ");
        Console.WriteLine("2 - Araba Teslim Al(T)               ");
        Console.WriteLine("3 - Kiradaki Arabaları Listele(R)    ");
        Console.WriteLine("4 - Galerideki Arabaları Listele(M)  ");
        Console.WriteLine("5 - Tüm Arabaları Listele(A)         ");
        Console.WriteLine("6 - Kiralama İptali(I)               ");
        Console.WriteLine("7 - Araba Ekle(Y)                    ");
        Console.WriteLine("8 - Araba Sil(S)                     ");
        Console.WriteLine("9 - Bilgileri Göster(G)              ");

        


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
            if (plaka == "X")
            {
                return;
            }

            // Plaka format kontrolü
            while (!Regex.IsMatch(plaka, @"^[0-9]{2}[A-Z]{1,3}[0-9]{2,4}$"))
            {
                Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                Console.Write("Kiralanacak arabanın plakası: ");
                plaka = Console.ReadLine().ToUpper();
                if (plaka == "X")
                {
                    return; // veya break;
                }
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
            if (saatGiris == "X")
            {
                return; // veya break;
            }

            int sure;
            while (!int.TryParse(saatGiris, out sure) || sure <= 0 && saatGiris != "X")
            {
                Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                Console.Write("Kiralanma süresi: ");
                saatGiris = Console.ReadLine();
                if (saatGiris == "X")
                {
                    return; // veya break;
                }
            }


            // Kiralama işlemi 
            OtoGaleri.ArabaKirala(plaka, sure);
            Console.WriteLine();
            Console.WriteLine(plaka + " plakalı araba " + sure + " saatliğine kiralandı.");
            return;
        }


        Console.WriteLine();

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
            if (plaka == "X")
            {
                return; // veya break;
            }

            // Plaka format kontrolü
            while (!Regex.IsMatch(plaka, @"^[0-9]{2}[A-Z]{1,3}[0-9]{2,4}$"))
            {
                Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                Console.Write("Kiralaması iptal edilecek arabanın plakası: ");
                plaka = Console.ReadLine().ToUpper();
                if (plaka == "X")
                {
                    return; // veya break;
                }
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
            else
            {
                Console.WriteLine("\nİptal gerçekleştirildi.");
                OtoGaleri.KiralamaIptal(plaka);
                return;

            }
        }
        Console.WriteLine();

    }

    static void ArabaTeslimAl()
    {

        Console.WriteLine("-Araba Teslim Al-");
        Console.WriteLine();
        string plaka = "";
        Araba secilenAraba = null;


        while (true)
        {
            Console.Write("Teslim edilecek arabanın plakası: ");
            plaka = Console.ReadLine().ToUpper();
            if (plaka == "X")
            {
                return; // veya break;
            }

            while (!Regex.IsMatch(plaka, @"^[0-9]{2}[A-Z]{1,3}[0-9]{2,4}$"))
            {
                Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                Console.Write("Teslim edilecek arabanın plakası: ");
                plaka = Console.ReadLine().ToUpper();
                if (plaka == "X")
                {
                    return; // veya break;
                }
            }

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
            else
            {
                Console.WriteLine("\nAraba galeride beklemeye alındı.");
                OtoGaleri.ArabaTeslimAl(plaka);
                return;

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
            return;

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

    static void GaleridekiArabalariListele()
    {

        Console.WriteLine("-Galerideki Arabalar-");
        Console.WriteLine();
        String Durum = "";
        Araba secilenAraba = null;

        secilenAraba = OtoGaleri.Arabalar.Find(a => a.Durum == "Galeride");
        if (secilenAraba == null)

        {
            Console.WriteLine("Listelenecek araç yok.");
            Console.WriteLine();
            return;

        }

        Console.WriteLine("Plaka".PadRight(14) + "Marka".PadRight(12) + "K. Bedeli".PadRight(12) + "Araba Tipi".PadRight(12) + "K. Sayısı".PadRight(12) + "Durum");
        Console.WriteLine("----------------------------------------------------------------------");

        foreach (Araba item in OtoGaleri.Arabalar)
        {
            if (item.Durum == "Galeride")
            {
                Console.WriteLine(item.Plaka.PadRight(14) + item.Marka.PadRight(12) + item.KiralamaBedeli.ToString().PadRight(12) + item.AracTipi.PadRight(12) + item.KiralanmaSayisi.ToString().PadRight(12) + item.Durum);
            }
        }

    }

    static void TumArabalariListele()
    {
    }
    static void ArabaEkle()
    {
        string plaka = "";
        string marka = "";
        float kiaralamaBedeli = 0;
        string aTipi = "";
        string arabaTipiSecim = "2";

        if (arabaTipiSecim == "2")
        {
            aTipi = "Hatchback";
        }


    }

    static void ArabaSil()
    {
    }

    static void BilgileriGoster()
    {
    }

    static void SahteVeriEkle()
    {
        //Deneme yapabilmek için ekledim_Aliye

        Araba a = new Araba("34ARB3434", "FIAT", 70, "Sedan");
        OtoGaleri.Arabalar.Add(a);
        a.Durum = "Galeride";
        Araba b = new Araba("35ARB3535", "KIA", 60, "SUV");
        OtoGaleri.Arabalar.Add(b);
        a.Durum = "Galeride";
        Araba c = new Araba("34US2342", "OPEL", 50, "Hatchback");
        OtoGaleri.Arabalar.Add(c);
        a.Durum = "Galeride";

    }


}
}
