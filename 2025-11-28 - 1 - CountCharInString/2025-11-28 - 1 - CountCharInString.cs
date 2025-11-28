// 2025-11-29 - 1 - Strings

// Schreiben Sie eine Methode· int CountCharInString(string s, char c), der man einen beliebigen String und einen beliebigen Character übergeben kann. Die Methode soll als Rückgabewert liefern, wie oft der übergebene Character im übergebenen String vorkommt.

#pragma warning disable CS8321 // Local function is declared but never used
int CountCharInString(string strg, char comparison, bool caseSensitive = false)
{
    int sum = 0;

    strg = (!caseSensitive) ? strg : strg.ToLower();

    foreach (char c in strg)
    {
        if (c == comparison) { sum++; }
    }

    return sum;
}
#pragma warning restore CS8321 // Local function is declared but never used


int CountCharInString2(string strg, char comparison, bool caseSensitive = false)
{
    strg = (!caseSensitive) ? strg : strg.ToLower();
    return strg.Count(c => c == comparison);
}



Console.WriteLine(CountCharInString2("Ananas", 'a'));
Console.WriteLine(CountCharInString2("Ananas", 'a', true));
