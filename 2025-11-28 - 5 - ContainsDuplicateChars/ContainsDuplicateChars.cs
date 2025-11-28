// 2025-11-28 - 6 - ContainsDuplicateChars

// Entwickeln Sie eine Methode bool ContainsDuplicateChars(string s) die prüft, ob in einem übergebenen String Zeichen mehrfach vorkommen. Ignorieren Sie dabei die Groß-/Kleinschreibung.

// Bsp.:	ContainsDuplicateChars("Otto")  =>  true
//          ContainsDuplicateChars("Micha")  =>  false


using System.Text;

bool ContainsDuplicateChars(string strg)
{
    strg = strg.ToLower();

    string letters = "";

    foreach (char c in strg)
    {
        if (letters.Contains(c))
        {
            return true;
        } else
        {
            letters += c;
        }
    }
    return false;
}

Console.WriteLine("Otto:  " + ContainsDuplicateChars("Otto"));
Console.WriteLine("Micha: " + ContainsDuplicateChars("Micha"));