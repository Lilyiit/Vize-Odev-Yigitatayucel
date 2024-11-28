using System;
using System.Collections.Generic;

public class Program
{
    private static List<Ders> dersListesi = new List<Ders>();
    private static List<Ogrenci> ogrenciListesi = new List<Ogrenci>();

    static void Main(string[] args)
    {
        bool sistemAcik = true;

        while (sistemAcik)
        {
            Console.WriteLine("\n--- Sistem Giriş Sayfası ---");
            Console.WriteLine("1. Kayıtlı Dersleri Listele");
            Console.WriteLine("2. Yeni Ders Ekle");
            Console.WriteLine("3. Yeni Öğrenci Ekle");
            Console.WriteLine("4. Öğrencinin Ders Seçmesi");
            Console.WriteLine("5. Sistemden Çık");
            Console.Write("Seçiminizi yapınız: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    DersleriListele();
                    break;
                case "2":
                    YeniDersEkle();
                    break;
                case "3":
                    YeniOgrenciEkle();
                    break;
                case "4":
                    OgrenciDersSec();
                    break;
                case "5":
                    Console.WriteLine("Sistemden çıkılıyor...");
                    sistemAcik = false;
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyin.");
                    break;
            }
        }
    }

    private static void DersleriListele()
    {
        if (dersListesi.Count == 0)
        {
            Console.WriteLine("Sistemde kayıtlı ders bulunmamaktadır.");
            return;
        }

        Console.WriteLine("\n--- Kayıtlı Dersler ---");
        for (int i = 0; i < dersListesi.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Ders: {dersListesi[i].Name}, Kredi: {dersListesi[i].Credit}, Öğretim Görevlisi: {dersListesi[i].Instructor.Name}");
        }

        Console.Write("\nDetaylarını görmek istediğiniz dersin numarasını girin (Çıkmak için 0): ");
        if (int.TryParse(Console.ReadLine(), out int secim))
        {
            if (secim == 0)
                return;

            if (secim < 1 || secim > dersListesi.Count)
            {
                Console.WriteLine("Geçersiz seçim!");
                return;
            }

            var secilenDers = dersListesi[secim - 1];
            secilenDers.ShowCourseInfo();
        }
        else
        {
            Console.WriteLine("Lütfen geçerli bir sayı girin.");
        }
    }

    private static void YeniDersEkle()
    {
        Console.WriteLine("\n--- Yeni Ders Ekle ---");
        Console.Write("Ders Adı: ");
        string dersAdi = Console.ReadLine();
        Console.Write("Kredi: ");
        if (!int.TryParse(Console.ReadLine(), out int kredi))
        {
            Console.WriteLine("Geçerli bir kredi giriniz.");
            return;
        }

        Console.WriteLine("\n--- Öğretim Görevlisi Bilgileri ---");
        Console.Write("Ad: ");
        string ogretimGorevlisiAdi = Console.ReadLine();
        Console.Write("Email: ");
        string ogretimGorevlisiEmail = Console.ReadLine();
        Console.Write("Bölüm: ");
        string department = Console.ReadLine();

        var ogretimGorevlisi = new OgretimGorevlisi
        {
            Name = ogretimGorevlisiAdi,
            Email = ogretimGorevlisiEmail,
            Department = department
        };

        var yeniDers = new Ders
        {
            Name = dersAdi,
            Credit = kredi,
            Instructor = ogretimGorevlisi
        };

        dersListesi.Add(yeniDers);

        Console.WriteLine($"Ders başarıyla eklendi: {yeniDers.Name}");
    }

    private static void YeniOgrenciEkle()
    {
        Console.WriteLine("\n--- Yeni Öğrenci Ekle ---");
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Geçerli bir ID giriniz.");
            return;
        }

        Console.Write("Ad: ");
        string name = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Kayıt Yılı: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
        {
            Console.WriteLine("Geçerli bir kayıt yılı giriniz.");
            return;
        }

        var yeniOgrenci = new Ogrenci
        {
            Id = id,
            Name = name,
            Email = email,
            EnrollmentYear = year
        };

        ogrenciListesi.Add(yeniOgrenci);

        Console.WriteLine($"Öğrenci başarıyla eklendi: {yeniOgrenci.Name}");
    }

    private static void OgrenciDersSec()
    {
        if (ogrenciListesi.Count == 0)
        {
            Console.WriteLine("Sistemde kayıtlı öğrenci bulunmamaktadır.");
            return;
        }

        if (dersListesi.Count == 0)
        {
            Console.WriteLine("Sistemde kayıtlı ders bulunmamaktadır.");
            return;
        }

        Console.WriteLine("\n--- Öğrenciler ---");
        for (int i = 0; i < ogrenciListesi.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ogrenciListesi[i].Name} (ID: {ogrenciListesi[i].Id})");
        }

        Console.Write("Ders seçmesini istediğiniz öğrencinin numarasını girin: ");
        if (!int.TryParse(Console.ReadLine(), out int ogrenciIndex) || ogrenciIndex < 1 || ogrenciIndex > ogrenciListesi.Count)
        {
            Console.WriteLine("Geçersiz seçim!");
            return;
        }

        Ogrenci secilenOgrenci = ogrenciListesi[ogrenciIndex - 1];

        Console.WriteLine("\n--- Dersler ---");
        for (int i = 0; i < dersListesi.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {dersListesi[i].Name} (Kredi: {dersListesi[i].Credit})");
        }

        Console.Write("Öğrencinin seçmek istediği dersin numarasını girin: ");
        if (!int.TryParse(Console.ReadLine(), out int dersIndex) || dersIndex < 1 || dersIndex > dersListesi.Count)
        {
            Console.WriteLine("Geçersiz seçim!");
            return;
        }

        Ders secilenDers = dersListesi[dersIndex - 1];

        secilenDers.AddStudent(secilenOgrenci);
        Console.WriteLine($"{secilenOgrenci.Name}, {secilenDers.Name} dersine başarıyla kaydedildi.");
    }
}

// Ders sınıfı
public class Ders
{
    public string Name { get; set; }
    public int Credit { get; set; }
    public OgretimGorevlisi Instructor { get; set; }
    public List<Ogrenci> EnrolledStudents { get; set; } = new List<Ogrenci>();

    public void AddStudent(Ogrenci student)
    {
        EnrolledStudents.Add(student);
    }

    public void ShowCourseInfo()
    {
        Console.WriteLine($"\nDers: {Name}, Kredi: {Credit}, Öğretim Görevlisi: {Instructor.Name}");
        Console.WriteLine("Kayıtlı Öğrenciler:");
        if (EnrolledStudents.Count == 0)
        {
            Console.WriteLine("Bu ders için kayıtlı öğrenci yok.");
        }
        else
        {
            foreach (var student in EnrolledStudents)
            {
                Console.WriteLine($"- {student.Name} (ID: {student.Id})");
            }
        }
    }
}

// Temel sınıf
public abstract class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public abstract void ShowInfo();
}

// Öğrenci sınıfı
public class Ogrenci : Person
{
    public int EnrollmentYear { get; set; }
    public override void ShowInfo()
    {
        Console.WriteLine($"[Öğrenci] Ad: {Name}, ID: {Id}, Email: {Email}, Kayıt Yılı: {EnrollmentYear}");
    }
}

// Öğretim görevlisi sınıfı
public class OgretimGorevlisi : Person
{
    public string Department { get; set; }
    public override void ShowInfo()
    {
        Console.WriteLine($"[Öğretim Görevlisi] Ad: {Name}, Email: {Email}, Bölüm: {Department}");
    }
}
