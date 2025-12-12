// Aufgabe Wasserstand 1:
// Flüsse führen im Verlauf der Zeit unterschiedlich viel Wasser. Ein zu hoher oder zu niedriger Wasserstand kann für Schiffe, Anwohner und andere Objekte eine Gefahr darstellen. Deshalb muss der Wasserstand eines Flusses überwacht werden, damit bei Gefahr reagiert werden kann.


namespace _2025_12_12___2___Wasserstand;

// Ein Fluss im Modell soll über
//     • einen Namen,
//     • einen Wasserstand (Wert zwischen 100 und 10.000),
//     • sowie eine Methode zur (zufälligen) Änderung des Wasserstands
// verfügen.
// Darüber hinaus soll der Fluss zwei Events anbieten mit denen er seine Beobachter über
// einen zu hohen oder zu niedrigen Wasserstand informiert. Das jeweilige Event soll gefeuert
// werden, wenn der geänderte Wasserstand unter 250 oder über 8.000 liegt.

public class Fluss(string name)
{
    private static int Minimum = 250;
    private static int Maximum = 8_000;

    private Random random = new Random();

    public string Name { get; } = name;
    public int Wasserstand = Minimum;

    public event EventHandler? WasserstandZuNiedrig;
    public event EventHandler? WasserstandZuHoch;
    public event EventHandler? WasserstandNormal;

    public void ChangeWasserstand()
    {
        Wasserstand = random.Next(100, 10_001);
        if (Wasserstand < Minimum && WasserstandZuNiedrig != null)
        {
            WasserstandZuNiedrig.Invoke(this, new EventArgs());
        }
        else if (Maximum < Wasserstand && WasserstandZuHoch != null)
        {
            WasserstandZuHoch(this, new EventArgs());
        }
        else if (WasserstandNormal != null)
        {
            WasserstandNormal(this, new EventArgs());
        }
    }

    public override string ToString()
    {
        return $"{Name.PadRight(12)} (Wasserstand: {Wasserstand})";
    }
}


// Schiffe sollen in diesem Szenario als Beobachter agieren, d.h. sie lassen sich vom Fluss über einen zu hohen oder zu niedrigen Wasserstand informieren. Sie verfügen über einen Namen und eine Methode, die anzeigt, dass das Schiff wegen zu hohem oder zu niedrigem Wasserstand die Fahrt gestoppt hat.

public class Schiff
{
    private Fluss _fluss;
    private bool _istGestoppt = false;
    private string _grundFürStopp = "";

    public string Name { get; }

    public Schiff(string name, Fluss fluss)
    {
        Name = name;
        _fluss = fluss;

        _fluss.WasserstandNormal += OnWasserstandNormal;
        _fluss.WasserstandZuNiedrig += OnWasserstandZuNiedrig;
        _fluss.WasserstandZuHoch += OnWasserstandZuHoch;
    }

    public void OnWasserstandNormal(object? sender, EventArgs ea)
    {
        _istGestoppt = false;
    }

    public void OnWasserstandZuNiedrig(object? sender, EventArgs ea)
    {
        _istGestoppt = true;
        _grundFürStopp = "Wasserstand zu niedrig";
    }

    public void OnWasserstandZuHoch(object? sender, EventArgs ea)
    {
        _istGestoppt = true;
        _grundFürStopp = "Wasserstand zu hoch";
    }

    public override string ToString()
    {
        string stopInfo = _istGestoppt ? $"Gestoppt, da {_grundFürStopp})" : "Segelt";
        return $"  {Name.PadRight(10)} ({stopInfo})";
    }
}


// Implementieren Sie ein passendes Testprogramm mit den Flüssen „Rhein“ und „Donau“. Der „Rhein“ bekommt als Beobachter die Schiffe „Rheingold“ und „Lorelei“. Die „Donau“ bekommt als Beobachter die Schiffe „Xaver“ und „Franz“.

class Program
{
    static void Main(string[] args)
    {
        var rhein = new Fluss("Rhein");
        var rheingold = new Schiff("Rheingold", rhein);
        var lorelei = new Schiff("Lorelei", rhein);

        var donau = new Fluss("Donau");
        var xaver = new Schiff("Xaver", donau);
        var franz = new Schiff("Franz", donau);

        while (true)
        {
            Thread.Sleep(1000);

            rhein.ChangeWasserstand();
            donau.ChangeWasserstand();

            Console.Clear();
            Console.WriteLine("(Ctrl-C zum Beenden)");
            Console.WriteLine();

            Console.WriteLine(rhein);
            Console.WriteLine(rheingold);
            Console.WriteLine(lorelei);
            Console.WriteLine();

            Console.WriteLine(donau);
            Console.WriteLine(xaver);
            Console.WriteLine(franz);
        }
    }
}
