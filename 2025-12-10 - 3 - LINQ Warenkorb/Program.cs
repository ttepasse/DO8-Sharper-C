using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace AufgabeWarenkorb;


public static class IEnumerableExtension
{
    public static void Print<T>(this IEnumerable<T> self)
    {
        Console.Write("{");
        foreach (var element in self.SkipLast(1))
        {
            Console.Write($"{element}, ");
        }
        Console.WriteLine($"{self.Last()}}}");
    }

    public static string ToString<T>(this IEnumerable<T> self)
    {
        var sb = new StringBuilder("{}");
        foreach (var element in self.SkipLast(1))
        {
            sb.Append($"{element}, ");
        }
        sb.Append("}");
        return sb.ToString();
    }
}

class Program
{


    static void Main(string[] args)
    {
        // In der Main()-Methode der Klasse Program erzeugen Sie nun zwei Arrays:
        Kunde[] kunden = Kunde.GetKundenListe();
        Produkt[] produkte = Produkt.GetProduktListe();

        // In den beiden Klassen Kunde und Produkt ist die ToString()-Methode 
        // überschrieben, Sie können also die Inhalte der Arrays direkt in 
        // einer Schleife mit einem Console.WriteLine() ausgeben.
        // Lösen Sie nun die folgenden Aufgaben. Schreiben Sie jeweils eine 
        // Lösung in Extension-Method-Syntax und eine in Query-Expression 
        // Syntax (soweit das möglich/sinnvoll ist).

        Console.Clear();

        // a) Selektieren Sie nur die Namen aller Produkte und anschließend nur Name und Wohnort aller Kunden aus den beiden Arrays.
        Console.WriteLine("a) Selektieren Sie nur die Namen aller Produkte und anschließend nur Name und Wohnort aller Kunden aus den beiden Arrays.");
        var resultA1 = from p in produkte
                       select p.Name;
        Console.Write("Produkte 1: ");
        resultA1.Print();

        Console.Write("Produkte 2: ");
        produkte.Select(p => p.Name).Print();

        var resultA2 = from k in kunden
                       select $"{k.Name} ({k.Ort})";
        Console.Write("Name, Wohnort 1: ");
        resultA2.Print();

        Console.Write("Name, Wohnort 2: ");
        kunden.Select(k => $"{k.Name} ({k.Ort})").Print();


        // b) Selektieren Sie die Bestellungen aller Kunden aus Deutschland.
        Console.WriteLine("b) Selektieren Sie die Bestellungen aller Kunden aus Deutschland.");
        var resultB = from k in kunden
                      where k.Land == Länder.Germany
                      from b in k.Bestellungen
                      select $"\"{b}\"";
        resultB.Print();

        kunden.Where(k => k.Land == Länder.Germany)
              .SelectMany(k => k.Bestellungen)
              .Select(b => $"\"{b}\"")
              .Print();


        // c) Selektieren Sie nur den Namen für jeden zweiten Kunden, beginnend mit dem ersten.
        Console.WriteLine("\nc) Selektieren Sie nur den Namen für jeden zweiten Kunden, beginnend mit dem ersten.");
        kunden.Where((k, index) => index % 2 == 0).Select(k => k.Name).Print();


        //     d) Selektieren Sie nur Name und Preis aller Produkte, die höchstens 20 Euro kosten. Das Ergebnis soll absteigend nach dem Preis sortiert sein.
        Console.WriteLine("\nd) Selektieren Sie nur Name und Preis aller Produkte, die höchstens 20 Euro kosten. Das Ergebnis soll absteigend nach dem Preis sortiert sein.");
        var resultD = from p in produkte
                      where p.Preis <= 20
                      orderby p.Preis descending
                      select $"\"{p.Name} ({p.Preis})\"";
        resultD.Print();

        produkte.Where(p => p.Preis <= 20)
                .OrderByDescending(p => p.Preis)
                .Select(p => $"\"{p.Name} ({p.Preis})\"")
                .Print();


        // e) Selektieren Sie nur Name und Land aller Kunden. Das Ergebnis soll aufsteigend nach dem Land sortiert werden. Bei gleichem Land sollen die Kunden nach dem Namen sortiert werden.
        Console.WriteLine("\ne) Selektieren Sie nur Name und Land aller Kunden. Das Ergebnis soll aufsteigend nach dem Land sortiert werden. Bei gleichem Land sollen die Kunden nach dem Namen sortiert werden.");

        // (Notiz: Die Länder werden nach Enum-Wert sortiert.
        //  Laut Max einfach ignorieren, statt rumzuarbeiten)

        var resultE = from k in kunden
                      orderby k.Land, k.Name
                      select $"\"{k.Name} ({k.Land})\"";
        resultE.Print();

        kunden.OrderBy(k => k.Land)
              .ThenBy(k => k.Name)
              .Select(k => $"\"{k.Name} ({k.Land})\"")
              .Print();


        //     f) Gruppieren Sie die Kunden nach dem Land. Als Gruppenelement soll jeweils das gesamte Kunden-Objekt verwendet werden.
        Console.WriteLine("\nf) Gruppieren Sie die Kunden nach dem Land. Als Gruppenelement soll jeweils das gesamte Kunden-Objekt verwendet werden.");
        var resultF = from k in kunden
                      group k by k.Land;
        resultF = kunden.GroupBy(k => k.Land);
        foreach (var group in resultF)
        {
            Console.WriteLine(group.Key);
            foreach (var kunde in group)
            {
                Console.WriteLine($"- {kunde.Name} ({kunde.Land})");
            }
        }


        //     g) Gruppieren Sie die Produkte nach dem ersten Buchstaben des Namens. Als Elemente in den Gruppen sollen nur die Namen der Produkte vorhanden sein.
        Console.WriteLine("\ng) Gruppieren Sie die Produkte nach dem ersten Buchstaben des Namens. Als Elemente in den Gruppen sollen nur die Namen der Produkte vorhanden sein.");
        var resultG = from p in produkte
                      orderby p.Name
                      group p.Name by p.Name.First();

        resultG = produkte.OrderBy(p => p.Name)
                          .GroupBy(p => p.Name.First(),
                                   p => p.Name);

        foreach (var group in resultG)
        {
            Console.WriteLine(group.Key);
            foreach (var kunde in group)
            {
                Console.WriteLine($"- {kunde}");
            }
        }


        //     h) Bilden Sie einen Join zwischen den Bestellungen und den Produkten. Selektieren Sie dann die Werte für Monat, ProduktNr, Name, Preis und Versendet sortiert nach dem Preis.
        Console.WriteLine("\nh) Bilden Sie einen Join zwischen den Bestellungen und den Produkten. Selektieren Sie dann die Werte für Monat, ProduktNr, Name, Preis und Versendet sortiert nach dem Preis.");
        var bestellungen = kunden.SelectMany(k => k.Bestellungen);

        var resultH = from b in bestellungen
                      join p in produkte on b.ProduktNr equals p.ProduktNr
                      orderby p.Preis
                      select new { b.Monat, p.ProduktNr, p.Name, p.Preis, b.Versendet };

        resultH = bestellungen
                    .Join(produkte,
                          b => b.ProduktNr,
                          p => p.ProduktNr,
                          (b, p) => new
                          {
                              b.Monat,
                              p.ProduktNr,
                              p.Name,
                              p.Preis,
                              b.Versendet
                          })
                    .OrderBy(x => x.Preis);


        foreach (var x in resultH)
        {
            Console.WriteLine($"{x.ProduktNr} {x.Name} ({x.Preis} Geld), bestellt: {x.Monat}, versendet {x.Versendet}");
        }


        //     i) Selektieren Sie alle Kunden mit Name, Wohnort und Anzahl Bestellungen.
        Console.WriteLine("\ni) Selektieren Sie alle Kunden mit Name, Wohnort und Anzahl Bestellungen.");
        var resultI = from k in kunden
                      select new
                      {
                          Name = k.Name,
                          Ort = k.Ort,
                          AnzahlBestellungen = k.Bestellungen.Length
                      };
        resultI.Print();


        // j) Summieren Sie die Preise aller Produkte aus der Produktliste.
        Console.WriteLine("\nj) Summieren Sie die Preise aller Produkte aus der Produktliste.");
        Console.WriteLine(produkte.Sum(p => p.Preis));


        // k) Selektieren Sie alle Kunden mit ihrem Namen und dem Gesamtbetrag ihrer Bestellungen.
        Console.WriteLine("\nk) Selektieren Sie alle Kunden mit ihrem Namen und dem Gesamtbetrag ihrer Bestellungen.");
        var resultK = from k in kunden
                      select new {
                        Name = k.Name,
                        Summe = (from b in k.Bestellungen
                                 join p in produkte on b.ProduktNr equals p.ProduktNr
                                 select b.Anzahl * p.Preis).Sum()
                      };
        
        resultK = kunden.Select(k => new
        {
            Name = k.Name,
            Summe = k.Bestellungen.Join(produkte,
                                        b => b.ProduktNr,
                                        p => p.ProduktNr,
                                        (b, p) => b.Anzahl * p.Preis).Sum()
        });

        foreach(var kunde in resultK)
        {
            Console.WriteLine($"{kunde.Name}    {kunde.Summe}");
        }

    }
}
