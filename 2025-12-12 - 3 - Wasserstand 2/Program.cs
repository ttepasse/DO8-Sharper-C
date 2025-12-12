// Aufgabe Wasserstand 2:
// Flüsse führen im Verlauf der Zeit unterschiedlich viel Wasser. Ein zu hoher oder zu niedriger Wasserstand kann für Schiffe, Anwohner und andere Objekte eine Gefahr darstellen. Deshalb muss der Wasserstand eines Flusses überwacht werden, damit bei Gefahr reagiert werden kann.


namespace _2025_12_12___3___Wasserstand_2;


// Ein Fluss im Modell soll über
//     • einen Namen,
//     • einen Wasserstand (Wert zwischen 100 und 10.000),
//     • ein Event WasserstandÄnderungEvent vom Typ EventHandler<MyEventArgs>,
//     • sowie eine Methode zur (zufälligen) Änderung des Wasserstands
// verfügen. Diesmal soll der Fluss aber einfach bei jeder Änderung des Wasserstands das Event feuern und den aktuellen Wasserstand dabei mitliefern.

public class WasserstandEventArgs : EventArgs
{
    public int newWasserstand { get; }

    public WasserstandEventArgs(int wasserstand)
    {
        newWasserstand = wasserstand;
    }
}


public class Fluss
{
    public string Name { get; }
    public int Wasserstand { get; private set; } = 100;
    private Random random = new Random();

    public event EventHandler<WasserstandEventArgs>? WasserstandÄnderungEvent;

    public Fluss(string name)
    {
        Name = name;
    }

    public void ChangeWasserstand()
    {
        Wasserstand = random.Next(100, 10_001);
        if (WasserstandÄnderungEvent != null)
        {
            WasserstandÄnderungEvent.Invoke(this, new WasserstandEventArgs(Wasserstand));
        }
    }

    public override string ToString()
    {
        return $"{Name.PadRight(12)} (Wasserstand: {Wasserstand})";
    }
}


// Es soll folgende folgende Typen von Beobachtern geben:
//     • Schiffe, die anhalten, sobald der Wasserstand unter 250 oder über 8.000 liegt

public abstract class WasserstandBeobachter
{
    public abstract string Name { get; }
    protected abstract string Action { get; set; }
    public abstract void OnWasserstandÄnderung(object? sender, WasserstandEventArgs ea);

    public override string ToString()
    {
        return $"  {GetType().Name.PadRight(10)} {this.Name.PadRight(12)} {this.Action}";
    }
}


public class Schiff : WasserstandBeobachter
{
    private static int Minimum = 250;
    private static int Maximum = 8_000;

    public override string Name { get; }
    private Fluss _fluss;

    protected override string Action { get; set; } = "";

    public Schiff(string name, Fluss fluss)
    {
        Name = name;
        _fluss = fluss;
        _fluss.WasserstandÄnderungEvent += OnWasserstandÄnderung;
    }

    public override void OnWasserstandÄnderung(object? sender, WasserstandEventArgs ea)
    {
        if (ea.newWasserstand < Minimum)
        {
            Action = $"(Gestoppt, da Wasserstand unter {Minimum})";
        }
        else if (Maximum < ea.newWasserstand)
        {
            Action = $"(Gestoppt, da Wasserstand über {Maximum})";
        }
        else
        {
            Action = "(Segelt)";
        }
    }
}

//     • Städte, die eine Wasserschutzwand errichten, sobald der Wasserstand über 8.200 steigt

public class Stadt : WasserstandBeobachter
{
    private Fluss _fluss;
    protected override string Action { get; set; } = "";

    public override string Name { get; }

    public Stadt(string name, Fluss fluss)
    {
        Name = name;
        _fluss = fluss;

        _fluss.WasserstandÄnderungEvent += OnWasserstandÄnderung;
    }

    public override void OnWasserstandÄnderung(object? sender, WasserstandEventArgs ea)
    {
        Action = ea.newWasserstand > 8200 ? $"(Wasserschutzwand errichtet, da Wasserstand über 8200)" : "";
    }
}


//     • Klärwerke, die ihre Einleitungen stoppen, sobald der Wasserstand über 8.000 steigt und ihre Einleitungen steigern, wenn der Wasserstand unter 3.000 sinkt

public class Klärwerk : WasserstandBeobachter
{
    public override string Name { get; }
    private Fluss _fluss;
    protected override string Action { get; set; } = "";

    public Klärwerk(string name, Fluss fluss)
    {
        Name = name;
        _fluss = fluss;

        _fluss.WasserstandÄnderungEvent += OnWasserstandÄnderung;
    }

    public override void OnWasserstandÄnderung(object? sender, WasserstandEventArgs ea)
    {
        if (ea.newWasserstand < 3000)
        {
            Action = $"(Einleitung gesteigert, da unter 3000)";
        }
        else if (ea.newWasserstand > 8000)
        {
            Action = $"(Einleitung gestoppt, da über 8000)";
        }
        else
        {
            Action = "(Leitet ein)";
        }
    }
}



class Program
{
    // Erstellen Sie ein passendes Klassenmodell und implementieren Sie dann ein passendes Testprogramm mit den Flüssen „Rhein“ und „Donau“.
    // Der „Rhein“ bekommt als Beobachter die Städte „Köln“ und „Düsseldorf“ und die Schiffe „Rheingold“ und „Lorelei“.
    // Die „Donau“ bekommt als Beobachter die Stadt „Ulm“, die Schiffe „Xaver“ und „Franz“ und das Klärwerk „Strauß 1“.
    
    static void Main(string[] args)
    {
        var rhein = new Fluss("Rhein");
        var köln = new Stadt("Köln", rhein);
        var düsseldorf = new Stadt("Düsseldorf", rhein);
        var rheingold = new Schiff("Rheingold", rhein);
        var lorelei = new Schiff("Lorelei", rhein);

        var donau = new Fluss("Donau");
        var ulm = new Stadt("Ulm", donau);
        var xaver = new Schiff("Xaver", donau);
        var franz = new Schiff("Franz", donau);
        var strauß = new Klärwerk("Strauß 1", donau);

        while (true)
        {
            Thread.Sleep(2000);

            rhein.ChangeWasserstand();
            donau.ChangeWasserstand();

            Console.Clear();
            Console.WriteLine("(Ctrl-C zum Beenden)");
            Console.WriteLine();

            Console.WriteLine(rhein);
            Console.WriteLine(köln);
            Console.WriteLine(düsseldorf);
            Console.WriteLine(rheingold);
            Console.WriteLine(lorelei);
            Console.WriteLine();

            Console.WriteLine(donau);
            Console.WriteLine(ulm);
            Console.WriteLine(xaver);
            Console.WriteLine(franz);
            Console.WriteLine(strauß);
        }
    }
}
