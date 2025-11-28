// 2025-11-28 - 6 - ContainsDuplicateChars

// Entwickeln Sie eine Methode bool ContainsDuplicateChars(string s) die prüft, ob in einem übergebenen String Zeichen mehrfach vorkommen. Ignorieren Sie dabei die Groß-/Kleinschreibung.

// Bsp.:	ContainsDuplicateChars("Otto")  =>  true
//          ContainsDuplicateChars("Micha")  =>  false


bool ContainsDuplicateChars(string strg)
{
    string letters = "";

    foreach (char c in strg)
    {
        char ch = Char.ToLower(c);
        if (letters.Contains(ch))
        {
            return true;
        } else
        {
            letters += ch;
        }
    }
    return false;
}

Console.WriteLine("Otto:  " + ContainsDuplicateChars("Otto"));
Console.WriteLine("Micha: " + ContainsDuplicateChars("Micha"));