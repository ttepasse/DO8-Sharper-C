namespace _2025_12_8___2___Mitarbeiterverwaltung;


// 1. Definiere eine Klasse Mitarbeiter:
// Die Klasse soll die folgenden Eigenschaften haben:
//     Name (string)
//     Alter (int)
//     Gehalt (double)
// Erstelle einen Konstruktor, der diese Eigenschaften initialisiert.

public class Mitarbeiter(string name, int alter, double gehalt)
{
    public string Name { get; private set; } = name;
    public int Alter { get; private set; } = alter;
    public double Gehalt { get; private set; } = gehalt;

    public override string ToString()
    {
        return $"  {Name} ({Alter} Jahre, {Gehalt} Gehalt)";
    }
}


// 2. Erstelle einen Delegate-Typ:
// Definiere einen Delegate-Typ MitarbeiterFilter, der eine Methode repräsentiert,
// die einen Mitarbeiter entgegennimmt und einen bool zurückgibt
// (z.B. für Filteroperationen wie "ist der Mitarbeiter älter als 30?").

public delegate bool MitarbeiterFilter(Mitarbeiter ma);


// 3. Filtere die Mitarbeiter:
//     ◦ Erstelle eine Methode FilternMitarbeiter, die eine Liste von Mitarbeiter-Objekten und einen 
// MitarbeiterFilter-Delegate als Parameter erhält. Diese Methode soll alle Mitarbeiter zurückgeben, die 
// den Filterbedingungen entsprechen.
//     ◦ Benutze Lambda-Ausdrücke, um die Filterbedingungen zu definieren
//       (z.B. "Alter > 30", "Gehalt > 50000").

class Program
{
    public static List<Mitarbeiter> FilternMitarbeiter(List<Mitarbeiter> ma, MitarbeiterFilter filter)
    {
        return ma.Where(m => filter(m)).ToList();
    }


    public static List<Mitarbeiter> SortiereMitarbeiter(List<Mitarbeiter> ma, Comparison<Mitarbeiter> cmp)
    {
        ma.Sort(cmp);
        return ma;
    }


    static void Main(string[] args)
    {
        List<Mitarbeiter> belegschaft = [
            new Mitarbeiter("Lea"    , 34,  50_000.0),
            new Mitarbeiter("Dawid"  , 29,  25_000.0),
            new Mitarbeiter("Steffen", 29, 150_000.0),
            new Mitarbeiter("Sergey" , 28,  50_000.0),
            new Mitarbeiter("Tim"    , 45, 500_000.0),
            new Mitarbeiter("Rachid" , 45, 350_000.0),
        ];

        Console.Clear();

        Console.WriteLine("\nMitarbeiter über Dreissig:");
        var ueberDreissig = FilternMitarbeiter(belegschaft, ma => ma.Alter > 30);
        ueberDreissig.ForEach(Console.WriteLine);

        Console.WriteLine("\nMitarbeiter, die über 100.000 erhalten:");
        var ueberHunderttausend = FilternMitarbeiter(belegschaft, ma => ma.Gehalt >= 100_000);
        ueberHunderttausend.ForEach(Console.WriteLine);

        Console.WriteLine("\nMitarbeiter sortiert nach Alter:");
        var mitarbeiterNachAlter = SortiereMitarbeiter(belegschaft, (m1, m2) => m1.Alter.CompareTo(m2.Alter));
        mitarbeiterNachAlter.ForEach(Console.WriteLine);

        Console.WriteLine("\nMitarbeiter, sortiert nach Gehalt:");
        SortiereMitarbeiter(belegschaft, (m1, m2) => m1.Gehalt.CompareTo(m2.Gehalt))
            .ForEach(Console.WriteLine);

        Console.WriteLine();
    }
}
