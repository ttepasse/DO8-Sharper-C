// Erweitern Sie die Methode aus Aufgabe e) indem Sie einen char[] als weiteren Parameter akzeptieren. In diesem char[] soll man alle möglichen Zeichen angeben können, nach denen ein Großbuchstabe folgen soll.

// Bsp.:	char[] chars = new char[] { ' ', '-' }
//   		Capitalize("c#-profi", chars)  =>  "C#-Profi"


string CapitalizeWord(string s)
{
    char[] result = s.ToLower().ToCharArray();
    result[0] = char.ToUpper(result[0]);
    return new string(result);
}

string CapitalizeOnce(string s, char c)
{
    return String.Join(c, s.Split(c).Select(word => CapitalizeWord(word)));
}

string Capitalize(string s, char[] chararray)
{
    foreach (char c in chararray)
    {
        s = CapitalizeOnce(s, c);
    }
    return s;
}

char[] chars = ['a', ' '];
Console.WriteLine(Capitalize("rantanplan mag ananas", chars));

Console.WriteLine("rantanplan mag ananas".Split('a'));