// schreiben Sie eine Methode string Capitalize(string s), die den übergebenen String in einen String umwandelt, bei dem jedes Wort mit einem Großbuchstaben beginnt. Alle anderen Buchstaben sollen Kleinbuchstaben sein.

// Bsp.:	Capitalize("OTTO")  =>  "Otto"
//   		Capitalize("hello world!")  =>  "Hello World!"


string CapitalizeWord(string s)
{
    char[] result = s.ToLower().ToCharArray();
    result[0] = char.ToUpper(result[0]);
    return new string(result);
}

string Capitalize(string s)
{
    return String.Join(" ", s.Split(" ").Select(word => CapitalizeWord(word)));
}

Console.WriteLine($"OTTO          =>  {CapitalizeWord("OTTO")}");
Console.WriteLine($"hello world!  =>  {Capitalize("hello world!")}");
