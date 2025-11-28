// 2025-11-28 - 2 - AnalyseString

// Schreiben Sie eine Methode void AnalyseString(string s), der man einen beliebigen String übergeben kann. Die Methode soll den String analysieren und anschließend auf der Konsole ausgeben wie viele Vokale (a, e, i, o, u), Konsonanten (b, c, d, f, ...), Umlaute (ä, ö, ü, ß), Ziffern (0, 1, 2, 3, ...) und sonstige Zeichen (also der Rest) enthalten waren. Beachten Sie dabei, dass Buchstaben groß oder klein geschrieben sein können.
// Versuchen Sie zur Bewertung der einzelnen Zeichen die Methode Contains() einzusetzen


void AnalyseString(string strg)
{
    uint vocals = 0;
    uint consonants = 0;
    uint umlauts = 0;
    uint digits = 0;
    uint rest = 0;

    foreach (char c in strg.ToLower())
    {
        if      ("aeiou".Contains(c))                 { vocals++; }
        else if ("bcdfghjklmnpqrstvwxyz".Contains(c)) { consonants++; }
        else if ("äöüß".Contains(c))                  { umlauts++; }
        else if ("0123456789".Contains(c))            { digits++; }
        else                                          { rest++; }
    }

    Console.WriteLine("Vokale:".PadRight(15) + vocals);
    Console.WriteLine("Konsonanten:".PadRight(15) + consonants);
    Console.WriteLine("Umlaute:".PadRight(15) + umlauts);
    Console.WriteLine("Ziffern:".PadRight(15) + digits);
    Console.WriteLine("Rest:".PadRight(15) + rest);
}

AnalyseString("In einer Höhle in der Erde wohnte ein Hobbit.");