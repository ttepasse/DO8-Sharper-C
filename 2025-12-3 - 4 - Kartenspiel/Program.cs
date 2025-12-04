// Aufgabe Kartenspiel:

// Sie möchten ein Kartenspiel programmieren und als eine wesentliche Aufgabe sehen Sie es an, Karten mischen zu können. Karten besitzen grundsätzlich einen Wert (7, 8, 9, 10, Bube, Dame, König, Ass) und eine Farbe (Karo, Herz, Pik, Kreuz).
// Gehen Sie davon aus, dass Sie es mit zwei Kartenstapeln zu tun haben, die jeweils in aufsteigender Reihenfolge sortiert sind. Der Stapel 1 enthält die Pik-Reihe, der Stapel 2 die Herz-Reihe.


using System.Text;

namespace _2025_12_3___4___Kartenspiel;


public enum CardColor
{
    Kreuz, Pik, Herz, Karo
}


public static class ExtensionMethods
{
    // CardColor::ToString - Damit man schönen Text bekommt.
    public static string ToString(this CardColor color) => color switch
    {
        CardColor.Kreuz => "Kreuz",
        CardColor.Pik => "Pik",
        CardColor.Herz => "Herz",
        CardColor.Karo => "Karo",
        _ => throw new NotImplementedException()
    };

    // Stack::Push(int count, otherStack)
    // Poppt count Karten von otherStack und Pusht diese auf sich selbst.
    public static void Push<T>(this Stack<T> self, int count, Stack<T> otherStack)
    {
        for (int i = 0; i < count; i++)
        {
            self.Push(otherStack.Pop());
        }
    }

    // Stack::Push(otherStack)
    // Pusht alle gepoppten Karten von otherStack auch sich selbst.
    public static void Push<T>(this Stack<T> self, Stack<T> otherStack)
    {
        self.Push(otherStack.Count, otherStack);
    }


    //     d) Machen Sie die Anwendung flexibler, indem Sie eine Methode schreiben,
    //        die einen Stapel und eine positive Zahl (für die Anzahl Teil-Stapel) entgegennimmt.
    //        Ihre Methode soll eine Liste zurückgeben, die so viele Stapel enthält, wie Teil-Stapel angegeben wurden.

    // Stack::PopStacks(int count)
    // Gibt count Teil-Stacks zurück.
    public static List<Stack<T>> PopStacks<T>(this Stack<T> self, int count)
    {
        var lst = new List<Stack<T>>();
        int take = (self.Count / count);
        for (int i = 0; i < count; i++)
        {
            var stck = new Stack<T>();
            stck.Push(take, self);
            lst.Add(stck);
        }
        return lst;
    }

    // Stack::Print()
    // Text-Ausgabe eines Stacks.
    // Man kann leider nicht ToString() als Extension Method überschreiben.
    public static string Print<Card>(this Stack<Card> self)
    {
        StringBuilder sb = new StringBuilder("<Stack: ");
        foreach (var item in self.SkipLast(1))
        {
            sb.Append($"{item}, ");
        }
        sb.Append(self.Last());
        sb.Append(">");
        return sb.ToString();
    }


}


public class Card
{
    public CardColor Color { get; private set; }
    public int Value { get; private set; }

    public Card(CardColor color, int value)
    {
        Color = color;
        if (7 > value || value > 14)
        {
            throw new ArgumentException("Kartenwert muss zwischen 7 und 13 liegen.");
        }
        Value = value;
    }

    // Statische Konstruktoren
    public static Card Kreuz(int value) => new Card(CardColor.Kreuz, value);
    public static Card Pik(int value) => new Card(CardColor.Pik, value);
    public static Card Herz(int value) => new Card(CardColor.Herz, value);
    public static Card Karo(int value) => new Card(CardColor.Karo, value);

    public override string ToString()
    {
        string v = Value switch
        {
            11 => "Bube",
            12 => "Dame",
            13 => "König",
            14 => "Ass",
            _ => Value.ToString(),
        };

        return $"<{Color} {v}>";
    }
}



class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        // Gehen Sie davon aus, dass Sie es mit zwei Kartenstapeln zu tun haben, die jeweils in 
        // aufsteigender Reihenfolge sortiert sind. Der Stapel 1 enthält die Pik-Reihe, der Stapel 2 
        // die Herz-Reihe.
        // a) Zunächst einmal sollten Sie diese beiden Stapel erzeugen.
        // Nutzen Sie dafür die Klasse Stack<T>, eine einzelne Karte können Sie über einen String 
        // darstellen (z.B. „Herz 7“), wenn Sie  mögen, können Sie aber auch eine eigene Klasse 
        // dafür schreiben.

        var piks = new Stack<Card>();
        var hearts = new Stack<Card>();

        foreach (var value in Enumerable.Range(7, 8))   // 7 bis 14 
        {
            piks.Push(Card.Pik(value));
            hearts.Push(Card.Herz(value));
        }

        Console.WriteLine("Zwei Kartenstapel, Piks und Herzen: ");
        Console.WriteLine(piks.Print());
        Console.WriteLine(hearts.Print());
        Console.WriteLine();



        //     b) Ihre nächste Aufgabe ist es, diese beiden Stapel in einem neuen Stapel 
        //        zusammenzufassen. Dieser neue Stapel soll bei jeder Karte die Farbe wechseln.

        var mixed = new Stack<Card>();

        for (int i = 0, end = piks.Count; i < end; i++)
        {
            // Console.WriteLine($"DEBUG: {i}  {piks.Peek()}  {hearts.Peek()}");
            mixed.Push(piks.Pop());
            mixed.Push(hearts.Pop());
        }

        Console.WriteLine("Piks und Herzen gemischt:");
        Console.WriteLine(mixed.Print());
        Console.WriteLine();



        //     c) Bilden Sie aus diesem Stapel vier neue Stapel zu je vier Karten.
        //        Legen Sie anschließend den ersten Stapel auf den dritten und den zweiten auf den
        //        vierten. Danach legen Sie den neu entstandenen ersten Stapel auf den neu
        //        entstandenen zweiten, so dass Sie wieder einen Stapel haben.

        var first = new Stack<Card>();
        var second = new Stack<Card>();
        var third = new Stack<Card>();
        var fourth = new Stack<Card>();

        first.Push(4, mixed);
        second.Push(4, mixed);
        third.Push(4, mixed);
        fourth.Push(4, mixed);

        Console.WriteLine("Erstes Abheben:");
        Console.WriteLine($"Eins: {first.Print()}");
        Console.WriteLine($"Zwei: {second.Print()}");
        Console.WriteLine($"Drei: {third.Print()}");
        Console.WriteLine($"Vier: {fourth.Print()}");
        Console.WriteLine();

        third.Push(first);
        fourth.Push(second);

        Console.WriteLine("Zweites Abheben:");
        Console.WriteLine($"Eins auf Drei: {third.Print()}");
        Console.WriteLine($"Zwei auf Vier: {fourth.Print()}");
        Console.WriteLine();

        fourth.Push(third);

        Console.WriteLine("Drittes Mischen:");
        Console.WriteLine($"Wieder ein Stapel: {fourth.Print()}");
        Console.WriteLine();


        // d) Test
        Console.WriteLine("Test von PopStacks():");
        var lst = fourth.PopStacks(4);
        foreach (var stack in lst)
        {
            Console.WriteLine(stack.Print());
        }
    }
}
