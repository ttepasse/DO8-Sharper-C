using System.ComponentModel.DataAnnotations;

namespace _2025_12_9___2___LINQ_Partionierung;


public static class ListExtension
{
    public static void Print<T>(this List<T> lst)
    {
        Console.Write("List {");
        foreach (var item in lst.SkipLast(1))
        {
            Console.Write(item + ", ");
        }
        Console.Write(lst.Last());
        Console.WriteLine("}");
    }
}


class Program
{
    static void Main(string[] args)
    {

        // Aufgabe Partitionierung:
        // 1. Gegeben sei das folgende Array:

        int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0, 22, 12, 16, 18, 11, 19, 13 };

        // Erstellen Sie jeweils eine Lösung in Extension-Method-Syntax und eine
        // in Query-Expression-Syntax für die folgenden Aufgaben:


        // a. Ermitteln Sie die ersten 5 Elemente im Array.
        // (Keine Query Syntax dafür)
        var firstFive = numbers.Take(5);
        firstFive.ToList().Print();


        // b. Ermitteln Sie die letzten fünf Elemente im Array.
        var lastFive = numbers.TakeLast(5);
        lastFive.ToList().Print();


        // c. Ermitteln Sie alle Elemente, außer den ersten und letzten drei Elementen.
        var middleElements = numbers.Skip(1).SkipLast(3);
        middleElements.ToList().Print();


        // d. Ermitteln Sie alle Elemente, die vor der 22 im Array stehen.
        var beforeTwentyTwo = numbers.TakeWhile(n => n != 22);
        beforeTwentyTwo.ToList().Print();


        // e. Ermitteln Sie alle Elemente, die nach der 12 im Array stehen.
        var afterTwelve = numbers.SkipWhile(n => n != 12).Skip(1);
        afterTwelve.ToList().Print();


        // f. Geben Sie das Array in einer Schleife „seitenweise“ aus. Jede Seite soll 5 Elemente enthalten.
        int offset = 0;
        int numberOfElements = 5;

        Console.WriteLine("Paginierung");
        while (offset < numbers.Count())
        {
            numbers.Skip(offset).Take(numberOfElements).ToList().Print();
            offset += numberOfElements;
        }
    }
}
