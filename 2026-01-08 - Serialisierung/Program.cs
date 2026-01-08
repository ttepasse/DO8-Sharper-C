// Schreiben Sie ein Programm mit einem kleinen Menu mit zwei Einträgen:
//     • Laden
//     • Speichern
// Wenn man Speichern wählt, soll das Programm ein Objekt der Klasse Person erzeugen, die über Vorname, Nachname und Alter verfügt. Die einzelnen Werte sollen vom Benutzer über die Konsole eingegeben werden. Anschließend wird das erzeugte Objekt in die Datei <Name>.txt serialisiert.
// Wenn man Laden wählt, wird man nach dem Namen der Person gefragt, und das Programm deserialisiert anschließend die entsprechende Datei in ein Objekt und zeigt die Werte auf der Konsole an.
// Wählen Sie einen Serialisierer Ihrer Wahl für das Programm oder probieren Sie mehrere aus.


using System.IO.Pipelines;
using System.Net;
using System.Text.Json;

namespace _2026_01_08___Serialisierung;


public class Person
{
    public string Vorname { get; set; } = String.Empty;
    public string Nachname { get; set; } = String.Empty;
    public int Alter { get; set; }
}


public class Input
{
    public static string String(string prompt)
    {
        string? result;
        while (true)
        {
            Console.Write(prompt);
            result = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(result)) { continue; }
            else { break; }
        }
        return result;
    }

    public static int Int(string prompt)
    {
        int number;
        while (true)
        {
            Console.Write(prompt);
            try
            {
                number = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                continue;
            }
            break;
        }
        return number;
    }

    public static Person Person()
    {
        return new Person()
        {
            Vorname = Input.String("Vorname: "),
            Nachname = Input.String("Nachname: "),
            Alter = Input.Int("Alter: ")
        };
    }
}


public class Program
{
    private static void DisplayMenu()
    {


        while (true)
        {
            Console.Clear();

            Console.WriteLine("[L]aden");
            Console.WriteLine("[S]peichern");
            Console.WriteLine("[B]eenden");

            string input = Console.ReadLine()!;
            if (input.IsWhiteSpace()) { continue; }

            switch (input.ToUpper().First())
            {
                case 'L':
                    Load();
                    continue;
                case 'S':
                    Save();
                    continue;
                case 'B':
                    Console.Clear();
                    Console.WriteLine("Programm beendet");
                    return;
                default:
                    continue;
            }
        }
    }


    private static void Save()
    {
        Console.Clear();
        Console.WriteLine("Bitte geben Sie die Daten für eine Person ein:");
        Person person = Input.Person();

        string personJSON = JsonSerializer.Serialize(person);
        string filename = $"{person.Vorname.ToLower()}.txt";
        File.WriteAllText(filename, personJSON);

        Console.WriteLine();
        Console.WriteLine($"'{filename}' gespeichert");
    }


    private static void Load()
    {
        Console.Clear();
        string filename = $"{Input.String("Vorname: ").ToLower()}.txt";

        if (!File.Exists(filename))
        {
            Console.WriteLine($"Kein Datei namens '{filename}' gefunden");
            Console.ReadKey();
            return;
        }
        else
        {
            try
            {
                string filetext = File.ReadAllText(filename);
                Person p = JsonSerializer.Deserialize<Person>(filetext)!;
                Console.WriteLine($"{p.Vorname} {p.Nachname} (Alter: {p.Alter})");
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("Irgendein Fehler ist aufgetreten.");
                Console.ReadKey();
                return;
            }
        }
    }


    static void Main(string[] args)
    {
        DisplayMenu();
    }
}

