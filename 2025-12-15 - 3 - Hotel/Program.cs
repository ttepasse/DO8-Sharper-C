// Aufgabe Hotel-Service:

// Ein Hotel bietet seinen Gästen einen besonderen Service: Im hoteleigenen Intranet kann man auf eine Liste der lukrativsten Aktien und deren aktuelle Kurse in Euro zugreifen. Da das Hotel sich nach dem Vollzug des Brexit Sorgen über einen Dexit macht, sollen die zur Verfügung gestellten Dateien mit den Aktien-Informationen schon einmal automatisch von Euro auf DM umgerechnet werden.
// Die Dateien enthalten jeweils Angaben zu mehreren Aktien, unter anderem die Wertpapier-Kenn-Nummer (WKN) in der ersten Spalte und den aktuellen Kurs in Euro in der zweiten Spalte. Die restlichen Spalten sind erst einmal nicht von Belang.


using System.Globalization;
using System.Net.Sockets;

namespace _2025_12_15___3___Hotel;

class Program
{
    // Geht drei Schritte hoch
    // ./bin/Debug/net9.0 -> ./
    static void ChangeCurrentWorkingDirectory()
    {
        var dir = new DirectoryInfo( Directory.GetCurrentDirectory() );
        dir = dir.Parent!.Parent!.Parent!;
        Directory.SetCurrentDirectory(dir.FullName);
    }

    //     a) Schreiben Sie ein Programm mit dem Sie exemplarisch die Datei data_hotel.txt einlesen können. Anschließend soll für jede Aktie die WKN, der aktuelle Kurs in Euro und der umgerechnete Kurs in DM auf der Konsole ausgegeben werden. Der Umrechnungskurs soll 1 Euro = 1,95583 DM lauten.
    // Bsp.:	WKN=500340, EUR=65,30, DM=127,72

    static decimal EUR2DM = 1.95583m;

    static void PartA()
    {
        using (StreamReader reader = File.OpenText("data_hotel.txt"))
        {
            string? line;
            string[] parts;

            while ((line = reader.ReadLine()) != null)
            {
                parts = line.Split(", ");
                string wkn = parts[0];
                decimal euro = Convert.ToDecimal(parts[1], new CultureInfo("en-GB"));
                decimal dm = euro * EUR2DM;
                Console.WriteLine($"WKN={wkn}, EUR={euro}, DM={dm:N2}");
            }
        }
    }


    //     b) Schreiben Sie nun ein Programm, dass die Datei data_hotel.txt einliest, den Euro-Betrag jeweils in DM umrechnet und die Daten anschließend mit dem neuen (also umgerechneten) Betrag als data_hotel_dm.txt abspeichert. Die ursprüngliche Datei soll in data_hotel_eur.txt umbenannt werden.
    // Bsp.-Datensatz:	500340, 127.72, "4:37", 0.00, 62.70, 65.50, 65.30, 0

    static string TransformLineToDM(string line)
    {
        string[] parts = line.Split(", ");
        decimal euro = Convert.ToDecimal(parts[1], new CultureInfo("en-GB"));
        decimal dm = euro * EUR2DM;
        string dm_s = $"{dm:N2}".Replace(",", ".");
        parts[1] = dm_s;
        return String.Join(", ", parts);
    }


    static void PartB()
    {
        FileStream fs = File.Create("data_hotel_dm.txt");
        using StreamWriter writer = new StreamWriter(fs);
        using StreamReader reader = File.OpenText("data_hotel.txt");
        string? line;

        while ((line = reader.ReadLine()) != null) {
            line = TransformLineToDM(line);
            writer.WriteLine(line);
            Console.WriteLine(line);
        }

        File.Copy("data_hotel.txt", "data_hotel_eur.txt");
    }
    


    static void Main(string[] args)
    {
        ChangeCurrentWorkingDirectory();
        PartA();
        Console.WriteLine("\n-------\n");
        PartB();
        Console.WriteLine("\n-------\n");

        Console.WriteLine("Any Key for Cleanup");
        Console.ReadKey();
        File.Delete("data_hotel_eur.txt");
        File.Delete("data_hotel_dm.txt");
    }
}
