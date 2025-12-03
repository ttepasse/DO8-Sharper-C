// Aufgabe Baumarkt:

namespace _2025_12_3___3___Baumarkt;

class Program
{
    
    // Helfermethode: Konvertiert eine Zeile des Text in einen Tupel von Accountnummer und Liste von Artikeln.

    static (string, List<string>) Line2Tupel(string line)
    {
        string[] lineArray = line.Split("; ");
        string accountNumber = lineArray.First();
        string itemsAsString = lineArray.Last();
        List<string> items = itemsAsString.Split(", ").ToList();
        return (accountNumber, items);
    }


    //     a) Zerlegen Sie die Liste in ihre Bestandteile (mit Split()) und speichern
    //        Sie diese in einem Dictionary<string, List<string>>.
    //        Der Key soll dabei die Kundennummer sein und die gekauften Artikel sollen
    //        in einer List<string> als Value gespeichert werden.

    static Dictionary<string, List<string>> ConvertTextToList(string text)
    {
        var result = new Dictionary<string, List<string>>();

        foreach (string line in text.Split("\n"))
        {
            (string accountNumber, List<string> articles) = Line2Tupel(line);
            result[accountNumber] = articles;
        }

        return result;
    }


    //    b) Schreiben Sie eine Methode, die den Inhalt des Dictionary aus Aufgabe a)
    //        in folgendem Format auf dem Bildschirm ausgibt:
    // Kundennummer: 0123
    // - Hammer
    // - Dübel
    // - Nägel

    public static void PrintCustomersAndOrders(Dictionary<string, List<string>> dict)
    {
        foreach (var item in dict)
        {
            Console.WriteLine($"Kundennummer: {item.Key}");
            foreach (var article in item.Value)
            {
                Console.WriteLine($"- {article}");
            }
            Console.WriteLine();
        }
    }


    //     c) Erzeugen Sie einen weiteren Dictionary<string, List<string>>.
    //        Nehmen Sie den Dictionary aus Aufgabe a) und übertragen Sie die Werte in den neuen Dictionary.
    //        Diesmal soll aber der Artikel als Key verwendet werden und die zugehörigen Kundennummern
    //        sollen in einer List<string> als Value gespeichert werden.

    public static Dictionary<string, List<string>> ArticlesForCustomers(Dictionary<string, List<string>> dict)
    {
        var result = new Dictionary<string, List<string>>();

        foreach (var item in dict)
        {
            foreach (var article in item.Value)         // Value ist eine Liste von Artikeln
            {
                // Wenn der Key nicht existiert, braucht es eine neue Liste als Value.
                if (!result.ContainsKey(article))
                {
                    result[article] = new List<string>();
                }

                // Wenn die Kundennummer noch nicht drin ist, in die Liste hinzufügen
                if (!result[article].Contains(item.Key))
                {
                    result[article].Add(item.Key);      // Key ist die Kundennummer.}
                }
            }
        }

        return result;
    }


    //     d) Verändern Sie die Methode aus Aufgabe b), so dass Sie mit dem neuen Dictionary
    //        folgende Ausgabe auf dem Bildschirm bekommen:
    // Artikel: Hammer
    // - 0123
    // - 9876

    public static void PrintArticlesAndCustomers(Dictionary<string, List<string>> dict)
    {
        foreach (var article in dict)
        {
            Console.WriteLine($"Artikel: {article.Key}");
            foreach (var accountNumber in article.Value)
            {
                Console.WriteLine($"- {accountNumber}");
            }
            Console.WriteLine();
        }
    }


    // MAIN

    static void Main(string[] args)
    {
        string liste = "0123; Hammer, Dübel, Nägel\n"
                     + "4711; Kantholz, Säge, Nägel, Leim\n"
                     + "8698; Schrauben, Dübel, Hänge-WC\n"
                     + "9876; Fischfutter, Hammer, Säge\n"
                     + "4862; Kantholz, Säge\n"
                     + "3179; Schrauben, Schraubenzieher, Dübel\n"
                     + "7410; Leim, Fischfutter\n"
                     + "8520; Hänge-WC, Nägel, Säge";

        var dict = ConvertTextToList(liste);
        PrintCustomersAndOrders(dict);

        Console.WriteLine("\n-----\n");

        var dict2 = ArticlesForCustomers(dict);
        PrintArticlesAndCustomers(dict2);
    }
}
