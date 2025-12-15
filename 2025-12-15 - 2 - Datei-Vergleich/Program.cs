// Aufgabe Datei-Vergleich:
// Schreiben Sie ein Programm, mit dem Sie zwei Text-Dateien miteinander vergleichen können.
// Die Dateien sollen mit Streams ausgelesen und verglichen werden.
// Legen Sie sich dazu ein paar Textdateien mit gleichen und unterschiedlichen Inhalten an und speichern Sie diese in Ihrem Projekt-Verzeichnis.

namespace _2025_12_15___2___Datei_Vergleich;

class Program
{
    //     a) Schreiben Sie zunächst eine Variante, die beide Dateien komplett in jeweils einen String einliest und anschließend vergleicht. Das Ergebnis des Vergleichs soll in verständlicher Form auf der Konsole ausgegeben werden.

    static void CompareAsString(string pathA, string pathB)
    {

        string textA, textB;

        using (StreamReader streamA = File.OpenText(pathA), streamB = File.OpenText(pathB))
        {
            textA = streamA.ReadToEnd();
            textB = streamB.ReadToEnd();
        }

        string result = textA == textB ? "gleich" : "ungleich";
        Console.WriteLine($"Die Dateien '{pathA}' und '{pathB} sind {result}.");
    }


    //     b) Schreiben Sie nun eine Version, die die beiden Dateien zeilenweise einliest und bei der ersten abweichenden Zeile abbricht. Es soll eine Meldung ausgegeben werden, in welcher Zeile die Abweichung gefunden wurde.

    static void CompareToFirstDifferentLine(string pathA, string pathB)
    {
        using (StreamReader streamA = File.OpenText(pathA), streamB = File.OpenText(pathB))
        {
            string? lineA, lineB;
            while (true)
            {
                lineA = streamA.ReadLine();
                lineB = streamB.ReadLine();

                // Eine Datei ist zuerst "zu Ende"
                if (lineA == null ^ lineB == null)
                {
                    Console.WriteLine("Eine Datei ist länger als die andere.");
                    return;
                }

                // Ende erreicht
                if (lineA == null && lineB == null)
                {
                    Console.WriteLine("Die beiden Dateien sind zeilenweise gleich.");
                    return;
                }

                // Die aktuellen Zeilen sind unterschiedlich.
                if (lineA != lineB)
                {
                    Console.WriteLine($"Diese Zeile aus '{pathA}' ist in '{pathB}' nicht vorhanden");
                    Console.WriteLine("\"" + lineA + "\"");
                    return;
                }
            }
        }
    }


    //     c) Schreiben Sie eine weitere Version, die die beiden Dateien zeilenweise einliest und vergleicht. Die Anzahl abweichender Zeilen soll am Ende auf der Konsole ausgegeben werden.

    public static void CompareAndCountDifferingLines(string pathA, string pathB)
    {
        using (StreamReader streamA = File.OpenText(pathA), streamB = File.OpenText(pathB))
        {
            uint differingLines = 0;

            string? lineA, lineB;

            do
            {
                lineA = streamA.ReadLine();
                lineB = streamB.ReadLine();

                // Dateien sind ungleicher Länger
                if (lineA == null ^ lineB == null)
                {
                    Console.WriteLine("Dateien sind ungleicher Länge");
                    break;
                }

                // Ende der Dateien erreicht
                // Note: Zählt nicht, wenn beide Dateien die ungleiche Anzahl von Zeilen hat.
                if (lineA == null || lineB == null)
                {
                    break;
                }

                // Ungleichen Zeilen 
                if (lineA != lineB)
                {
                    differingLines++;
                }

            } while (true);

            // Ausgabe
            if (differingLines != 0)
            {
                Console.WriteLine($"{differingLines} ungleiche Zeilen");
            }
            else
            {
                Console.WriteLine("Die beiden Dateien scheinen gleich zu sein");
            }
        }
    }

    static void Main(string[] args)
    {
        Console.Clear();

        // Auf das Hauptverzeichnis wechseln
        // ./bin/Debug/net9.0/ -> ./
        DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        dir = dir.Parent!.Parent!.Parent!;
        Directory.SetCurrentDirectory(dir.FullName);
        Console.WriteLine("Aktuelles Working Directory: " + Directory.GetCurrentDirectory());
        Console.WriteLine();

        // Aufgabe a)
        Console.WriteLine("Aufgabe a: Als ganze Strings vergleichen");
        CompareAsString("beispiel-a.txt", "beispiel-a.txt");
        CompareAsString("beispiel-a.txt", "beispiel-b.txt");
        Console.WriteLine();

        // Aufgabe b)
        Console.WriteLine("Aufgabe b: Zeilenweise vergleichen und bei der ersten Differenz abbrechen");
        CompareToFirstDifferentLine("beispiel-a.txt", "beispiel-a.txt");
        CompareToFirstDifferentLine("beispiel-a.txt", "beispiel-b.txt");
        Console.WriteLine();

        // Aufgabe c)
        Console.WriteLine("Aufgabe c: Zeilenweise vergleichen und die ungleichen Zeilen zählen");
        CompareAndCountDifferingLines("beispiel-a.txt", "beispiel-a.txt");
        CompareAndCountDifferingLines("beispiel-a.txt", "beispiel-b.txt");
        Console.WriteLine();

    }
}
