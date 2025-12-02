// Aufgabe ISBN:
// Schreiben Sie eine Methode der man eine 13-stellige Ziffernfolge als String übergeben kann.
// Die Methode soll zunächst prüfen, ob es sich tatsächlich um 13 Ziffern handelt. 
// ◦ Wurde als Argument null übergeben, soll eine ArgumentNullException geworfen werden. 
// ◦ Wurde etwas anderes als Ziffern übergeben, soll eine ArgumentFormatException geworfen werden
//   (die müssen sie selbst schreiben). 
// ◦ Und wenn es korrekterweise Ziffern sind, die Länge des Strings aber nicht 13 Zeichen beträgt,
//   soll eine ArgumentOutOfRangeException geworfen werden.
//   Ist der String in Ordnung, sollen die Ziffern nach dem Muster
// einer ISBN ausgeben werden (z. B. ISBN 978-3-86680-192-9).


using System.Text;

namespace _2025_1_2___2___ISBN;


class Program
{
    static string ISBN(string? isbn)
    {
        if (isbn == null)
        {
            throw new ArgumentNullException("Darf nicht null sein");
        }

        if (!isbn.All(Char.IsDigit))
        {
            throw new FormatException("Alle Zeichen müssen Ziffern sein.");
        }

        if (isbn.Length != 13)
        {
            throw new ArgumentOutOfRangeException("Eine ISBN hat 13 Ziffern");
        }

        return $"ISBN {isbn[0..3]}-{isbn[3]}-{isbn[4..9]}-{isbn[9..12]}-{isbn[12]}";
    }



    static void Main(string[] args)
    {
        Console.Clear();

        Console.WriteLine("Test: Argument ist null.");
        try
        {
            Console.WriteLine(ISBN(null));
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine();


        Console.WriteLine("Test: keine Ziffern");
        try
        {
            Console.WriteLine(ISBN("Tim100"));
        }
        catch (FormatException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine();

        
        Console.WriteLine("Test: String zu kurz");
        try
        {
            Console.WriteLine(ISBN("123456"));
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine();


        Console.WriteLine("Test: Endlich eine richtige ISBN:");
        Console.WriteLine(  ISBN("9783866801929")  );
    }
}
