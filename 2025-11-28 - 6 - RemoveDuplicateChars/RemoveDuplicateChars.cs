// 2025-11-28 - 6 - RemoveDuplicateChars

// Entwickeln Sie die Methode string RemoveDuplicateChars(string s) die aus einem übergebenen String alle doppelten Zeichen entfernt. 
// Die Groß-/Kleinschreibung soll dabei ignoriert werden.

// Bsp.:	RemoveDuplicateChars("Otto")  =>  "Ot"
// 		RemoveDuplicateChars("Michael")  =>  "Michael"


using System.Text;

string RemoveDuplicateChars(string strg)
{
    string seenLetters = "";    // String, wegen der Contains-Methode
    StringBuilder sb = new StringBuilder();

    foreach (char c in strg)
    {
        string s = c.ToString().ToLower();
        if (!seenLetters.Contains(s)) {
            seenLetters += s;
            sb.Append(c);
        }
    }
    return sb.ToString();
}

Console.WriteLine("Otto    => " + RemoveDuplicateChars("Otto"));
Console.WriteLine("Michael => " + RemoveDuplicateChars("Michael"));