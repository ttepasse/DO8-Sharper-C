// Aufgabe Job-Liste:

// Sie sollen eine kleine Verwaltung für Jobs schreiben.
// Ein Job soll dabei durch ein Objekt der Klasse Job dargestellt werden,
// welche die Attribute Bezeichnung, Auftraggeber und Dauer enthält.
// Die Attribute sollen beim Erstellen eines neuen Jobs über den Konstruktor gefüllt werden.

// Für die Verwaltung der Jobs soll es eine eigene Klasse JobVerwaltung geben. Die Klasse besitzt ein privates Attribut vom Typ Queue<Job>, um die einzelnen Jobs zu speichern. Darüber hinaus gibt es drei Methoden:
//     • Die Methode AddJob(), um einen Job hinzuzufügen. Die Methode nimmt ein Job-Objekt entgegen, reiht es in die Queue ein und meldet dann, wie viele Jobs aktuell in der Liste sind.
//     • Die Methode GetJobDone(), um einen Job zu erledigen. Die Methode gibt den nächsten anstehenden Job auf der Konsole aus, löscht diesen dann aus der Queue und meldet anschließend die Anzahl noch vorhandener Jobs.
//     • Eine Methode ShowAllJobs(), die alle aktuell gespeicherten Jobs auf der Konsole ausgibt.

using System.Net.WebSockets;

namespace _2025_12_3___1___Job_Liste;


public class Job
{
    public string Bezeichnung { get; private set; }
    public string Auftraggeber { get; private set; }
    public string Dauer { get; private set; }

    public Job(string bezeichnung, string auftraggeber, string dauer)
    {
        Bezeichnung = bezeichnung;
        Auftraggeber = auftraggeber;
        Dauer = dauer;
    }

    public override string ToString()
    {
        return $"<Job '{Bezeichnung}' ({Auftraggeber}, Dauer: {Dauer})";
    }
}


public class JobVerwaltung
{
    private Queue<Job> _jobs = new Queue<Job>();

    public void AddJob(Job job)
    {
        _jobs.Enqueue(job);
        Console.WriteLine($"AddJob: {job}");
    }

    public Job GetJobDone()
    {
        Job job = _jobs.Dequeue();
        Console.WriteLine($"GetJobDone: {job}");
        Console.WriteLine($"Noch {_jobs.Count} Jobs.");
        return job;
    }

    public void ShowAllJobs()
    {
        Console.WriteLine();
        Console.WriteLine($"Noch {_jobs.Count} Jobs in der Verwaltung");
        foreach (Job job in _jobs)
        {
            Console.WriteLine($"  {job}");
        }
        Console.WriteLine();
    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.Clear();

        JobVerwaltung jw = new JobVerwaltung();

        jw.AddJob( new Job("Ring nach Bree bringen", "Gandalf", "1 Monat") );
        jw.AddJob( new Job("Von Bree nach Bruchtal", "Aragorn", "2 Wochen") );
        jw.AddJob( new Job("Erholung und Essen", "Elrond", "2 Wochen") );

        jw.ShowAllJobs();

        jw.GetJobDone();
        jw.ShowAllJobs();

        jw.GetJobDone();
        jw.GetJobDone();

        jw.ShowAllJobs();
    }
}
