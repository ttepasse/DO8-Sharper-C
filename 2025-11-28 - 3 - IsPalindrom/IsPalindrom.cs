// 2025-11-28 - 3 - IsPalindrom

// Entwickeln Sie eine Methode bool IsPalindrom(string s) die prüft, ob ein übergebener String ein Palindrom ist. Ein Palindrom ist ein Text, der vorwärts und rückwärts gelesen werden kann. Ignorieren Sie dabei die Groß-/Kleinschreibung.


bool IsPalindrom(string strg)
{
    strg = strg.ToLower();

    for (int start = 0, end = strg.Length-1; start < end; start++, end--)
    {
        if (strg[start] != strg[end])
        {
            return false;
        }
    }
    return true;
}


Console.WriteLine($"Anna: {IsPalindrom("Anna")}");
Console.WriteLine($"Otto: {IsPalindrom("Otto")}");
Console.WriteLine($"Tim: {IsPalindrom("Tim")}");