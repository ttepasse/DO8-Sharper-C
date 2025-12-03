// Aufgabe Hotel:

// Sie erhalten folgenden Auszug aus der Buchungsliste eines Hotels:
// string text = "15;D;Peter Schmidt;Wuppertal\n"
//             + "17;D;Hans Meier;Düsseldorf\n"
//             + "23;E;Regina Schulz;Mettmann\n"
//             + "31;D;Kathrin Müller;Erkrath\n"
//             + "32;E;Rudolf Kramer;Witten\n"
//             + "35;E;Anne Kunze;Bremen";

//     a) Kopieren Sie den String in Ihr Programm und zerlegen Sie ihn in seine einzelnen Werte. 
// Erzeugen Sie für jeden Datensatz eine Ausgabe auf der Konsole die folgendem Muster entspricht:

// Zimmer 15:
//   Doppelzimmer
//   Vorname: Peter
//   Nachname: Schmidt
//   Wohnort: Wuppertal

//     b) Verändern Sie Ihr Programm, so dass die Werte für jeden Datensatz in einem eigenen 
// Dictionary<string, string> gespeichert werden. Sammeln Sie die einzelnen KeyValuePairs dann in
// einer List<KeyValuePair<string, string>> und geben Sie die Werte dann in der bekannten Weise auf 
// der Konsole aus.
//     • List<KeyValuePair<string, string>> oder Dictionary<string,string> oder beides :D

namespace _2025_12_3_2___Hotel;


class Program
{
    static void TaskA(string text)
    {
        foreach (string line in text.Split("\n"))
        {
            string[] reservation = line.Split(";");

            string roomNumber = reservation[0];
            string roomType = (reservation[1] == "E") ? "Einzelzimmer" : "Doppelzimmer";
            string givenName = reservation[2].Split(" ")[0];
            string lastName = reservation[2].Split(" ")[1];
            string origin = reservation[3];

            Console.WriteLine($"Zimmer {roomNumber}");
            Console.WriteLine($"  {roomType}");
            Console.WriteLine($"  Vorname:  {givenName}");
            Console.WriteLine($"  Nachname: {lastName}");
            Console.WriteLine($"  Wohnort:  {origin}");
            Console.WriteLine();
        }
    }

    static void TaskB(string text)
    {
        List<Dictionary<string, string>> reservations = new List<Dictionary<string, string>>();

        foreach (string line in text.Split("\n"))
        {
            Dictionary<string, string> reservation = new Dictionary<string, string>();

            string[] lineArray = line.Split(";");

            reservation["roomNumber"] = lineArray[0];
            reservation["roomType"] = (lineArray[1] == "E") ? "Einzelzimmer" : "Doppelzimmer";
            reservation["givenName"] = lineArray[2].Split(" ")[0];
            reservation["lastName"] = lineArray[2].Split(" ")[1];
            reservation["origin"] = lineArray[3];

            reservations.Add(reservation);
        }

        foreach (var reservation in reservations)
        {
            
            Console.WriteLine($"Zimmer {reservation["roomNumber"]}");
            Console.WriteLine($"  {reservation["roomType"]}");
            Console.WriteLine($"  Vorname:  {reservation["givenName"]}");
            Console.WriteLine($"  Nachname: {reservation["lastName"]}");
            Console.WriteLine($"  Wohnort:  {reservation["origin"]}");
            Console.WriteLine();
        }
    }


    static void Main(string[] args)
    {
        string text = "15;D;Peter Schmidt;Wuppertal\n"
                    + "17;D;Hans Meier;Düsseldorf\n"
                    + "23;E;Regina Schulz;Mettmann\n"
                    + "31;D;Kathrin Müller;Erkrath\n"
                    + "32;E;Rudolf Kramer;Witten\n"
                    + "35;E;Anne Kunze;Bremen";

        // TaskA(text);
        Console.Clear();
        TaskB(text);
    }
}
