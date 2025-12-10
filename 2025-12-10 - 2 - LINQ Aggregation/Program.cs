namespace _2025_12_10___2___LINQ_Partionierung;


public static class IEnumerableExtension
{
    public static void Print<T>(this IEnumerable<T> self)
    {
        var lst = self.ToList();
        Console.Write("{");
        foreach (var element in lst.SkipLast(1))
        {
            Console.Write($"{element}, ");
        }
        Console.WriteLine($"{lst.Last()}}}");
    }
}


class Program
{
    static void Main(string[] args)
    {
        // 1. Gegeben sei das folgende Array:

        int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0, 22, 12, 16, 18, 11, 19, 13 };

        // Ermitteln Sie mittels LINQ-Ausdrücken die folgenden Informationen.
        // Erstellen Sie jeweils eine Lösung in Extension-Method-Syntax und eine in Query-Expression-Syntax.

        Console.Clear();

        Console.WriteLine("Das Array:");
        numbers.Print();

        // a. Die Summe aller Werte im Array
        Console.WriteLine("\na. Die Summe aller Werte im Array");
        Console.WriteLine(numbers.Sum());

        // b. Die kleinste Zahl
        Console.WriteLine("\nb. Die kleinste Zahl");
        Console.WriteLine(numbers.Min());

        // c. Die größte Za\nhl
        Console.WriteLine("\nc. Die größte Zahl");
        Console.WriteLine(numbers.Max());

        // d. Den Durchschn\nittswert
        Console.WriteLine("\nd. Den Durchschnittswert");
        Console.WriteLine(numbers.Average());

        // e. Die kleinste \ngerade Zahl
        Console.WriteLine("\ne. Die kleinste gerade Zahl");
        Console.WriteLine(numbers.Where(n => n % 2 == 0).Min());

        // f. Die größte un\ngerade Zahl
        Console.WriteLine("\nf. Die größte ungerade Zahl");
        Console.WriteLine(numbers.Where(n => n % 2 == 1).Max());

        // g. Die Summe aller geraden Zahlen
        Console.WriteLine("\ng. Die Summe aller geraden Zahlen");
        Console.WriteLine(numbers.Where(n => n % 2 == 0).Sum());

        // h. Den Durchschnittswert aller ungeraden Zahlen
        Console.WriteLine("\nh. Den Durchschnittswert aller ungeraden Zahlen");
        Console.WriteLine(numbers.Where(n => n % 2 == 1).Average());

        // i. Die Anzahl aller geraden Zahlen
        Console.WriteLine("\ni. Die Anzahl aller geraden Zahlen");
        Console.WriteLine(numbers.Where(n => n % 2 == 0).Count());


    }
}