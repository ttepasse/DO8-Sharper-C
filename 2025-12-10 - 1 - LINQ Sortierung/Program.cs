namespace _2025_12_10___1___LINQ_Sortierung;


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

        // Erstellen Sie jeweils eine Lösung in Extension-Method-Syntax und eine in Query-Expression-Syntax für die folgenden Aufgaben:

        Console.Clear();

        // a. Geben Sie das Array in aufsteigender Reihenfolge aus
        Console.WriteLine("a. Geben Sie das Array in aufsteigender Reihenfolge aus");
        var res = from n in numbers
                  orderby n
                  select n;
        res.Print();
        numbers.Order().Print();

        // b. Geben Sie das Array in absteigender Reihenfolge aus
        Console.WriteLine("\nb. Geben Sie das Array in absteigender Reihenfolge aus");
        res = from n in numbers
              orderby n descending
              select n;
        res.Print();
        numbers.OrderDescending().Print();

        // c. Geben Sie alle geraden Zahlen in aufsteigender Reihenfolge aus
        Console.WriteLine("\nc. Geben Sie alle geraden Zahlen in aufsteigender Reihenfolge aus");
        res = from n in numbers
              where n % 2 == 0
              orderby n
              select n;
        res.Print();
        numbers.Where(n => n % 2 == 0).Order().Print();

        // d. Geben Sie alle Zahlen zwischen 5 und 11 in absteigender Reihenfolge aus
        Console.WriteLine("\nd. Geben Sie alle Zahlen zwischen 5 und 11 in absteigender Reihenfolge aus");
        res = from n in numbers
              where 5 <= n && n <= 11
              orderby n descending
              select n;
        res.Print();
        numbers.Where(n => 5 <= n && n <= 11).OrderDescending().Print();
    }
}
