// Gegeben sei das folgende Array:

// int[] numbers = { 5,4,1,3,9,8,6,7,2,0,22,12,16,18,11,19,13 };

// Ermitteln Sie mittels LINQ-Ausdrücken die folgenden Informationen. Erstellen Sie jeweils eine Lösung in Extension-Method-Syntax und eine in Query-Expression-Syntax.
//         a. Alle Zahlen echt kleiner als 7
//         b. Alle geraden Zahlen
//         c. Alle einstelligen ungeraden Zahlen
//         d. Alle geraden Zahlen ab dem 6. Element (einschließlich) im Array
//         e. Alle Zahlen, die durch 2 oder durch 3 ohne Rest teilbar sind


namespace _2025_12_9___1___LINQ;

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
        List<int> numbers = new List<int>() { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0, 22, 12, 16, 18, 11, 19, 13 };

        //         a. Alle Zahlen echt kleiner als 7
        var numbersUnderSeven = from n in numbers
                                where n < 7
                                select n;
        numbersUnderSeven.ToList().Print();

        numbersUnderSeven = numbers.Where(n => n < 7);
        numbersUnderSeven.ToList().Print();


        //         b. Alle geraden Zahlen
        var evenNumbers = from n in numbers
                          where n % 2 == 0
                          select n;
        evenNumbers.ToList().Print();

        evenNumbers = numbers.Where(n => n% 2 == 0);
        evenNumbers.ToList().Print();


        //         c. Alle einstelligen ungeraden Zahlen
        var oddDigits = from n in numbers.Take(9)
                        where n % 2 == 1
                        select n;
        oddDigits.ToList().Print();

        oddDigits = numbers.Take(9).Where(n => n % 2 == 1);
        oddDigits.ToList().Print();


        //         d. Alle geraden Zahlen ab dem 6. Element (einschließlich) im Array
        var evenNumbersFromSix = from n in numbers.Skip(5)
                                 where n % 2 == 0
                                 select n;
        evenNumbersFromSix.ToList().Print();

        evenNumbersFromSix = numbers.Skip(5).Where(n => n % 2 == 0);
        evenNumbersFromSix.ToList().Print();


        //         e. Alle Zahlen, die durch 2 oder durch 3 ohne Rest teilbar sind
        var numbersDivisibleByTwoOrThree = from n in numbers
                                           where n % 2 == 0 || n % 3 == 0
                                           select n;
        numbersDivisibleByTwoOrThree.ToList().Print();

        numbersDivisibleByTwoOrThree = numbers.Where(n => n % 2 == 0 || n % 3 == 0);
        numbersDivisibleByTwoOrThree.ToList().Print();


        Console.WriteLine("\n--------------\n");


        // Gegeben sei das folgende Array:

        List<string> numberNames = new List<string>() { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen" };

        // Ermitteln Sie mittels LINQ-Ausdrücken die folgenden Informationen. Erstellen Sie jeweils eine 
        // Lösung in Extension-Method-Syntax und eine in Query-Expression-Syntax.

        // a. Alle „Zahlen“ die drei Zeichen lang sind

        var threeCharNumbers = from n in numberNames
                               where n.Length == 3
                               select n;
        threeCharNumbers.ToList().Print();

        threeCharNumbers = numberNames.Where(n => n.Length == 3);
        threeCharNumbers.ToList().Print();


        // b. Alle „Zahlen“ die ein „o“ enthalten
        var numbersWithO = from n in numberNames
                           where n.Contains('o')
                           select n;
        numbersWithO.ToList().Print();

        numbersWithO = numberNames.Where(n => n.Contains('o'));
        numbersWithO.ToList().Print();


        // c. Alle „Zahlen“ die auf „teen“ enden
        var teenager = from n in numberNames
                       where n.EndsWith("teen")
                       select n;
        teenager.ToList().Print();

        teenager = numberNames.Where(n => n.EndsWith("teen"));
        teenager.ToList().Print();


        // d. Alle „Zahlen“ die auf „teen“ enden in Großbuchstaben
        var TEENAGER = from n in numberNames
                       where n.EndsWith("teen")
                       select n.ToUpper();
        TEENAGER.ToList().Print();

        TEENAGER = numberNames.Where(n => n.EndsWith("teen")).Select(n => n.ToUpper());
        TEENAGER.ToList().Print();

        
        // e. Alle „Zahlen“ die „four“ enthalten
        var containsFours = from n in numberNames
                            where n.Contains("four")
                            select n;
        containsFours.ToList().Print();

        containsFours = numberNames.Where(n => n.Contains("four"));
        containsFours.ToList().Print();


        // f. Alle „Zahlen“ die nicht mit einem „t“ oder „f“ beginnen
        // (Notiz: Funktioniert, weil alles im Array kleingeschrieben ist.)
        var dontStartWithTorF = from n in numberNames
                                where !(n.StartsWith('f') || n.StartsWith('t'))
                                select n;
        dontStartWithTorF.ToList().Print();

        dontStartWithTorF = numberNames.Where(n => !(n.StartsWith('f') || n.StartsWith('t')));
        dontStartWithTorF.ToList().Print();
    }
}
