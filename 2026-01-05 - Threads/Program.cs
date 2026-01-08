namespace _2026_01_05___Threads;



public class CancellableThread
{
    private Thread thread;
    private CancellationTokenSource cts;

    public CancellableThread(Action<CancellationToken> action)
    {
        this.cts = new CancellationTokenSource();
        this.thread = new Thread(() => action(cts.Token));
    }

    public void Start() => this.thread.Start();

    public void Cancel()
    {
        Console.WriteLine($"Cancelanfrage an Thread #{thread.ManagedThreadId}");
        this.cts.Cancel();
    }
}


public static class ListExtension
{
    public static void CancelAndRemoveRandomThread(this List<CancellableThread> self)
    {
        Random randy = new Random();
        int index = randy.Next(self.Count);
        self[index].Cancel();
        self.RemoveAt(index);
    }
}


class Program
{
    static Random randy = new Random();

    // Schreiben Sie eine statische Methode, die in einer for-Schleife 100 mal Informationen über den aktuellen Thread ausgibt. Dazu sollen die ManagedThreadId, die Priorität und der Status des Threads gehören. Zusätzlich soll die Methode innerhalb der Schleife eine zufällige Zeit zwischen 100 und 500 Millisekunden „schlafen“. Nach Beendigung der Schleife soll die Methode dann ausgeben, dass sie das Ende erreicht hat.

    static void PrintThreadInfo(CancellationToken ct)
    {
        Thread t = Thread.CurrentThread;
        bool doesNotIgnoreCancellation = randy.Next(4) % 3 == 0;    // 75% Wahrscheinlichkeit

        foreach (var i in Enumerable.Range(0, 100))
        {
            if (doesNotIgnoreCancellation && ct.IsCancellationRequested)
            {
                Console.WriteLine($"Thread #{t.ManagedThreadId,2}; Status {t.ThreadState}: CANCELLED");
                return;
            }
            else
            {
                Console.WriteLine($"Thread #{t.ManagedThreadId,2} ignoriert Cancellation-Anfrage");
            }

            Thread.Sleep(randy.Next(100, 501));
            Console.WriteLine($"Thread #{t.ManagedThreadId,2}; Priorität: {t.Priority}; Status: {t.ThreadState}");
        }
        Console.WriteLine($"Thread {t.ManagedThreadId,2}: Finished");
    }


    static void Main(string[] args)
    {
        // b) Legen Sie im Hauptprogramm eine Liste von Threads an und fügen Sie dieser Liste 10 Threads hinzu, die alle Ihre Methode mit der Schleife starten. Starten Sie alle Threads in der Liste.

        var threads = Enumerable.Range(0, 10)
                      .Select((i) => new CancellableThread(PrintThreadInfo))
                      .ToList();

        threads.ForEach((t) => t.Start());


        // c) Erweitern Sie das Hauptprogramm, so dass nach dem Start aller Threads ein zufälliger Thread aus der Liste ausgewählt und abgebrochen wird. Der abgebrochene Thread soll dann aus der Liste entfernt werden. Dieser Vorgang soll sich nach einer zufälligen Wartezeit zwischen 100 und 500 Millisekunden solange wiederholen, bis keine Threads mehr in der Liste vorhanden sind.

        while (threads.Count > 0)
        {
            Thread.Sleep(randy.Next(100, 501));
            threads.CancelAndRemoveRandomThread();
        }

    }
}
