// Es soll eine Methode ShowFilteredValues() zur Filterung eines Arrays entwickelt werden.
// Die Methode soll einen String-Array und einen Delegate-Typ als Parameter erwarten.
// Der Delegate-Typ wiederum soll einen String-Parameter entgegennehmen und als Rückgabetyp
// ein boolean liefern.
// Innerhalb der Methode soll dann der Array durchlaufen werden, und für jedes Element der
// Delegate-Parameter aufgerufen werden. Nur wenn der Delegate für das Element true ergibt,
// soll das Element auf dem Bildschirm ausgegeben werden.


using System.ComponentModel;

namespace _2025_12_8___2___ArrayFilter;


// Schreiben Sie einen neuen Delegate-Typen, der Methoden mit einem string-Parameter
// und einem bool-Rückgabewert speichern kann.

public delegate bool Filter(string strg);



public class Program
{
    // Schreiben Sie die Methode ShowFilteredValues() die einen Array von Strings und
    // einen Lambda-Ausdruck entgegennimmt. Die Methode soll den Array durchlaufen und
    // anhand der übergebenen Filter-Methode entscheiden, welche Werte auf der Konsole
    // ausgegeben werden und welche nicht. Definieren Sie außerdem in der Main()-Methode
    // einen Array mit verschiedenen Strings. Schreiben Sie dann eine Methode IsUpperCase()
    // die true zurückgibt, wenn der übergebene String in Großbuchstaben geschrieben ist
    // und ansonsten false. Testen Sie die Filter-Methode mit der IsUpper-Methode.

    public static void ShowFilteredValues(string[] stringArr, Filter filter)
    {
        foreach(string strg in stringArr)
        {
            if (filter(strg)) { Console.WriteLine(strg); }
        }
    }

    public static bool IsUpperCase(string strg)
    {
        return strg.All(Char.IsUpper);
    }

    public static bool ContainsTheLetterT(string strg)
    {
        foreach (char c in strg.ToLower())
        {
            if (c == 't') { return true; }
        }
        return false;
    }

    static void Main(string[] args)
    {
        string[] strings = [
            "abc",
            "ABC",
            "Tim",
            "TIM"
        ];

        Console.WriteLine("Nur uppercase Strings");
        ShowFilteredValues(strings, strg => IsUpperCase(strg));
        Console.WriteLine();

        // Schreiben Sie darüber hinaus weitere Methoden die zur Signatur des Delegate-Parameters
        // passen. Testen Sie jetzt die Methode ShowFilteredValues(), indem Sie sie mit dem Array und
        // verschiedenen Methoden-Referenzen aufrufen.
        Console.WriteLine("Nur Strings mit mit dem Buchstaben T");
        ShowFilteredValues(strings, strg => ContainsTheLetterT(strg));
    }
}
