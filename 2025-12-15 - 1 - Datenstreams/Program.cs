using System.Text;

namespace _2025_12_15___1___Datenstreams;

class Program
{
    static void DriveInfoExample(string[] args)
    {
        DriveInfo[] driveInfos = DriveInfo.GetDrives();

        foreach (DriveInfo di in driveInfos)
        {
            Console.Write("Drive: ");
            Console.WriteLine(di.Name);

            Console.Write("Typ: ");
            Console.WriteLine(di.DriveType);

            if (di.IsReady)
            {
                Console.WriteLine($"  Format:        {di.DriveFormat}");
                Console.WriteLine($"  Label:         {di.VolumeLabel}");
                Console.WriteLine($"  Drive size:    {di.TotalSize}");
                Console.WriteLine($"  Free Total:    {di.TotalFreeSpace}");
                Console.WriteLine($"  Free for user: {di.AvailableFreeSpace}");
            }

            Console.WriteLine();
        }
    }


    static void DirectoriesExample()
    {
        string path = "/home/cl902/Schreibtisch/Lokalstick/8 - Gronemann, C#/Code/foo";
        // Directory ist eine statische Alternative zu DirectoryInfo
        DirectoryInfo di = new DirectoryInfo(path);

        if (!di.Exists)
        {
            di.Create();
            di.CreateSubdirectory("test");

            FileSystemInfo[] infos = di.GetFileSystemInfos();

            foreach (var fsi in infos)
            {
                Console.WriteLine(fsi.Name);
            }

            Console.ReadLine();
            di.Delete(true);

        }
        else
        {
            Console.WriteLine("Verzeichnis xistiert bereits");
        }
    }


    static void FileExample()
    {
        FileInfo fi = new FileInfo("text.txt");
        if (!fi.Exists)
        {
            using(StreamWriter sw = fi.CreateText())
            {
                sw.WriteLine("Ein wenig Inhalt");
            }
        }
        Console.WriteLine(fi.FullName);
        Console.ReadLine();

        fi.MoveTo("foo.txt");
        Console.WriteLine(fi.FullName);
        Console.ReadLine();

        fi.CopyTo("bar.txt");
        Console.WriteLine(fi.FullName);
        Console.ReadLine();

        fi.Delete();
    }


    static void PathExample()
    {
        string path = Directory.GetCurrentDirectory();
        Console.WriteLine(path);
        string[] parts = path.Split(Path.DirectorySeparatorChar);

        foreach (string s in parts)
        {
            Console.WriteLine(s);
        }

        StringBuilder sb = new();
        sb.Append(parts[0]);
        for (int i = 1; i < parts.Length; i++)
        {
            sb.Append(Path.DirectorySeparatorChar);
            sb.Append(parts[i]);
        }
        string newPath = sb.ToString();
        Console.WriteLine(newPath);

        string newJoin = String.Join(Path.DirectorySeparatorChar, parts);
        Console.WriteLine(newJoin);

        bool error = newPath.Intersect(Path.GetInvalidPathChars())
                            .Any();
        Console.WriteLine(error);


        using (FileSystemWatcher watcher = new FileSystemWatcher(path))
        {
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;

            watcher.Created += Watcher_Changed;
            watcher.Changed += Watcher_Changed;
            watcher.Deleted += Watcher_Changed;

            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Watcher ist an: ");
            Console.ReadLine();
        }

    }

    static void Watcher_Changed(object source, FileSystemEventArgs ea)
    {
        Console.WriteLine($"Datei: {ea.Name} {ea.ChangeType}");
    }


    static void DatastreamExample()
    {
        // Datei erstellen
        string fileName = "StreamWriter.txt";
        FileStream fs = File.Create(fileName);

        // Text in die Datei schreiben
        StreamWriter writer = new StreamWriter(fs);
        writer.WriteLine("Ein wenig Text bla bla bla bla");
        writer.Close();

        // Text aus Datei lesen
        using (StreamReader reader = File.OpenText(fileName))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        // Datei wieder löschen
        File.Delete(fileName);

        
        // FileStream wieder schließen
        fs.Close();
    }




    static void Main()
    {
        // DriveInfoExample();
        // DirectoriesExample();
        // FileExample();
        // PathExample();
        DatastreamExample();
    }

}
