// 2025-11-28 - 4 - IsPalindromWithout Spaces

// Verändern Sie die Methode aus Aufgabe a), so dass diese Leerzeichen im Text ignoriert.
// Bsp.:	IsPalindrom("Dreh mal am Herd")  =>  true

bool IsPalindrom(string strg)
{
    strg = strg.ToLower().Replace(" ", "");

    for (int start = 0, end = strg.Length-1; start < end; start++, end--)
    {
        if (strg[start] != strg[end])
        {
            return false;
        }
    }
    return true;
}


Console.WriteLine("Dreh mal am Herd " + IsPalindrom("Dreh mal am Herd"));
Console.WriteLine("Ich hab Hunger   " + IsPalindrom("Ich hab Hunger"));