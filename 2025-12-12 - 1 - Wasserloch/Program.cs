// Aufgabe Wasserloch:
// In Afrika gibt es für die wild lebenden Tiere Wasserlöcher an denen sie sich regelmäßig versammeln und trinken. Leider ist dies auch ein beliebter Ort für Raubkatzen, die auf Nahrungssuche sind. Um die Gefahr gefressen zu werden für die Tiere zu minimieren, gibt es an jedem Wasserloch ein Wächtertier, das Alarm schlägt, wenn sich Raubkatzen nähern. Diesen Alarm können allerdings nur die Tiere empfangen, die sich vorher beim Wächtertier angemeldet haben.
// Kommt nun eine Raubkatze in die Nähe des Wasserlochs, dann schlägt das Wächtertier Alarm. Dabei verständigt es alle Tiere, die sich vorher bei ihm registriert haben. Die einzelnen Wildtiere haben dann unterschiedliche Taktiken auf dieses Ereignis zu reagieren. Die einen fliehen, andere formieren sich zum Kampf und wieder andere versuchen sich zu verstecken oder zu tarnen.
// Damit die registrierten Tiere auch wissen, wer den Alarm schlägt und was genau los ist, sendet das Wächtertier beim Auslösen des Alarms seine eigene Kennung und eine zusätzliche Info was passiert ist.
//     a) Schreiben Sie zunächst eine Klasse RaubkatzeEventArgs die Sie von der Klasse Event­Args ableiten. Die Klasse enthält lediglich ein öffentliches Property string Info.
//     b) Schreiben Sie dann eine Klasse Wächtertier. Die Klasse enthält ein öffentliches Event RaubkatzeKommt vom Typ EventHandler<RaubkatzeEventArgs>. Außerdem gibt es eine Methode OnRaubkatzeKommt(), die den Angriff einer Raubkatze simuliert. Wird die Methode aufgerufen, wird zunächst ein neues RaubkatzeEventArgs-Objekt erzeugt mit der Info „Raubkatze in der Nähe“. Anschließend wird (sofern sich Tiere registriert haben) das Event ausgelöst, wobei das Wächtertier seine eigene Referenz und das RaubkatzeEventArgs-Objekt übergibt.
//     c) Schreiben Sie jetzt Klassen für die Flucht-, Kampf- und Tarntiere. Diese enthalten jeweils eine zum Verhalten passende Methode (fliehen(), kämpfen(), …), mit der sie sich beim Wächtertier registrieren können. Die Methoden-Signatur muss daher dem EventHandler entsprechen und einen Object-Parameter und einen RaubkatzeEventArgs-Parameter haben. In der Methode soll dann über eine Konsolen-Ausgabe das Verhalten der Tiere dargestellt werden. Dabei soll auch die Info aus dem RaubkatzeEventArgs-Objekt ausgegeben werden.


namespace _2025_12_12___1___Wasserloch;

// a) Schreiben Sie zunächst eine Klasse RaubkatzeEventArgs die Sie von der Klasse Event­Args ableiten.
// Die Klasse enthält lediglich ein öffentliches Property string Info.

public class RaubkatzeEventArgs : EventArgs
{
    public string Info { get; }

    public RaubkatzeEventArgs(string info)
    {
        Info = info;
    }
}


//  b) Schreiben Sie dann eine Klasse Wächtertier. Die Klasse enthält ein öffentliches Event RaubkatzeKommt vom Typ EventHandler<RaubkatzeEventArgs>. Außerdem gibt es eine Methode OnRaubkatzeKommt(), die den Angriff einer Raubkatze simuliert. Wird die Methode aufgerufen, wird zunächst ein neues RaubkatzeEventArgs-Objekt erzeugt mit der Info „Raubkatze in der Nähe“. Anschließend wird (sofern sich Tiere registriert haben) das Event ausgelöst, wobei das Wächtertier seine eigene Referenz und das RaubkatzeEventArgs-Objekt übergibt.

public class Wächtertier
{
    public event EventHandler<RaubkatzeEventArgs>? RaubkatzeKommt;

    public void OnRaubkatzeKommt()
    {
        Console.WriteLine("Raubkatze kommt, Wächtertier schlägt Alarm:");
        if (RaubkatzeKommt != null)
        {
            RaubkatzeKommt.Invoke(this, new RaubkatzeEventArgs("Raubkatze in der Nähe"));
        }
    }
}


// c) Schreiben Sie jetzt Klassen für die Flucht-, Kampf- und Tarntiere. Diese enthalten jeweils eine zum Verhalten passende Methode (fliehen(), kämpfen(), …), mit der sie sich beim Wächtertier registrieren können. Die Methoden-Signatur muss daher dem EventHandler entsprechen und einen Object-Parameter und einen RaubkatzeEventArgs-Parameter haben. In der Methode soll dann über eine Konsolen-Ausgabe das Verhalten der Tiere dargestellt werden. Dabei soll auch die Info aus dem RaubkatzeEventArgs-Objekt ausgegeben werden.

public class Fluchttier
{
    private string _typ;
    private Wächtertier _wächtertier;

    public Fluchttier(string typ, Wächtertier wächtetier)
    {
        _typ = typ;
        _wächtertier = wächtetier;
        wächtetier.RaubkatzeKommt += Fliehen;
    }

    public void Fliehen(object? sender, RaubkatzeEventArgs ea)
    {
        Console.WriteLine($"  {ea.Info}: {_typ} flieht");
    }
}


public class Kampftier
{
    private string _typ;
    private Wächtertier _wächtertier;

    public Kampftier(string typ, Wächtertier wächtetier)
    {
        _typ = typ;
        _wächtertier = wächtetier;
        wächtetier.RaubkatzeKommt += Kämpft;
    }

    public void Kämpft(object? sender, RaubkatzeEventArgs ea)
    {
        Console.WriteLine($"  {ea.Info}: {_typ} kämpft");
    }
}

public class Tarntier
{
    private string _typ;
    private Wächtertier _wächtertier;

    public Tarntier(string typ, Wächtertier wächtetier)
    {
        _typ = typ;
        _wächtertier = wächtetier;
        wächtetier.RaubkatzeKommt += Tarnen;
    }

    public void Tarnen(object? sender, RaubkatzeEventArgs ea)
    {
        Console.WriteLine($"  {ea.Info}: {_typ} tarnt");
    }
}




class Program
{
    static void Main(string[] args)
    {
        var wt = new Wächtertier();
        
        var salamander = new Fluchttier("Salamander", wt);
        var triceratops = new Kampftier("Triceratops", wt);
        var chamäleons = new Tarntier("Chamäleon", wt);

        wt.OnRaubkatzeKommt();
    }
}
