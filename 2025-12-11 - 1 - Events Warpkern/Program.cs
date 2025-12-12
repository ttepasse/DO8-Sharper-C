// Erstellen Sie die Klasse WarpKern. Die Klasse hat als Eigenschaft eine warpkernTemperatur, die sich ständig (zufällig) ändern kann. Die Klasse WarpKern löst einen Event aus, wenn sich die Temperatur ändert und löst einen weiteren Event aus, wenn die Temperatur über 500 Grad steigt.
// Erstellen Sie zusätzlich die Klasse WarpKernKonsole, welche die Aufgabe hat, die Temperatur-Änderungen und die Warnmeldung auf der Brücke in der Konsole darzustellen.
// Verwenden Sie ein EventArgs-Objekt, welchem Sie die alte und die neue Temperatur des Warpkerns, sowie die aktuelle Uhrzeit (bekommt man über die Structure DateTime) übergeben. Geben Sie diese Daten in der WarpKernKonsole aus.


using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace _2025_12_11___1___Events_Warpkern;

// Erstellen Sie die Klasse WarpKern. Die Klasse hat als Eigenschaft eine warpkernTemperatur,
// die sich ständig (zufällig) ändern kann. Die Klasse WarpKern löst einen Event aus,
// wenn sich die Temperatur ändert und löst einen weiteren Event aus, wenn die Temperatur über 500 Grad steigt.


public abstract class WarpEventArgs : EventArgs
{
    public abstract int OldTemperature { get; }
    public abstract int NewTemperature { get; }
}

public class WarpTemperatureChangeEventArgs : WarpEventArgs
{
    public override int OldTemperature { get; }
    public override int NewTemperature { get; }

    public WarpTemperatureChangeEventArgs(int oldTemp, int newTemp)
    {
        OldTemperature = oldTemp;
        NewTemperature = newTemp;
    }
}

public class WarpWarningEventArgs : WarpEventArgs
{
    public override int OldTemperature { get; }
    public override int NewTemperature { get; }

    public WarpWarningEventArgs(int oldTemp, int newTemp)
    {
        OldTemperature = oldTemp;
        NewTemperature = newTemp;
    }
}


public class WarpKern
{
    // Eigentlich von 2.500.000 Kelvin aufwärts laut Memory Alpha
    private static int _min = 400;
    private static int _max = 550;

    public int WarpkernTemperatur { get; private set; } = _min;

    private Random random = new Random();

    public event EventHandler<WarpTemperatureChangeEventArgs>? TemperatureChange;
    public event EventHandler<WarpWarningEventArgs>? TemperatureToHigh;

    public void Run()
    {
        while (true)
        {
            Thread.Sleep(2000);

            int newTemp = random.Next(_min, _max);

            if (newTemp >= 500 && TemperatureToHigh != null)
            {
                TemperatureToHigh.Invoke(this, new WarpWarningEventArgs(WarpkernTemperatur, newTemp));
            }
            else if (TemperatureChange != null)
            {
                TemperatureChange.Invoke(this, new WarpTemperatureChangeEventArgs(WarpkernTemperatur, newTemp));
            }

            WarpkernTemperatur = newTemp;
        }
    }
}


public class WarpKernKonsole
{
    private WarpKern _warpkern;

    public WarpKernKonsole(WarpKern wk)
    {
        _warpkern = wk;
        _warpkern.TemperatureChange += OnTemperatureChange;
        _warpkern.TemperatureToHigh += OnTemperatureTooHigh;
    }

    public void OnTemperatureChange(object? sender, WarpTemperatureChangeEventArgs ea) => Render(ea, false);
    public void OnTemperatureTooHigh(object? sender, WarpWarningEventArgs ea)          => Render(ea, true);

    public void Render(WarpEventArgs ea, bool warning)
    {
        var now = DateTime.Now;
        Console.Clear();
        Console.WriteLine($"### Warpkern-Konsole #####################");
        Console.WriteLine($"#                                        #");
        Console.WriteLine($"#  Uhrzeit:      {now.Hour}:{now.Minute.ToString().PadLeft(2, '0')}                   #");
        Console.WriteLine($"#  Temperatur:   {ea.NewTemperature} °K                  #");
        Console.WriteLine($"#                                        #");
        if (warning)
        {
            Console.WriteLine($"#  RED ALERT: Kerntemperatur zu hoch!    #");
        }
        else
        {
            Console.WriteLine($"#                                        #");
        }
        Console.WriteLine($"#                                        #");
        Console.WriteLine($"##########################################");
        Console.WriteLine();
        Console.WriteLine("(Ctrl-C zum beenden)");
    }
}



class Program
{
    static void Main(string[] args)
    {
        var wk = new WarpKern();
        var wkk = new WarpKernKonsole(wk);

        wk.Run();
    }
}
