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
    static string ISBN(string? possibleISBN)
    {
        if (possibleISBN == null)
        {
            throw new ArgumentNullException("Darf nicht null sein");
        }

        if (!possibleISBN.All(Char.IsDigit))
        {
            throw new FormatException("Alle Zeichen müssen Ziffern sein.");
        }

        if (possibleISBN.Length != 13)
        {
            throw new ArgumentOutOfRangeException("Eine ISBN hat 13 Ziffern");
        }

        // string[] ISBNdigits = possibleISBN.Split("");

        StringBuilder sb = new StringBuilder();

        // Ziffern 1, 2, 3
        sb.Append(possibleISBN[0]);
        sb.Append(possibleISBN[1]);
        sb.Append(possibleISBN[2]);
        
        // Strich, Ziffer 4, Strich
        sb.Append("-");
        sb.Append(possibleISBN[3]);
        sb.Append("-");

        // Ziffern 5 bis 9
        sb.Append(possibleISBN[4]);
        sb.Append(possibleISBN[5]);
        sb.Append(possibleISBN[6]);
        sb.Append(possibleISBN[7]);
        sb.Append(possibleISBN[8]);

        // Strich
        sb.Append("-");

        // Ziffern 10, 11, 12
        sb.Append(possibleISBN[9]);
        sb.Append(possibleISBN[10]);
        sb.Append(possibleISBN[11]);

        // Strich
        sb.Append("-");

        // Ziffer 13
        sb.Append(possibleISBN[12]);

        return sb.ToString();
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
