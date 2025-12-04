namespace _2025_12_4___1___Quest_Dictionary;


public class Item(string name, string description)
{
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
}


public class QuestReward(int xp, List<Item> items)
{
    public int XP { get; private set; } = xp;
    public List<Item> Items { get; private set; } = items;
}


public class Journey
{
    public Dictionary<string, QuestReward> buchDerAufgaben { get; } = new Dictionary<string, QuestReward>();
    private Queue<string> _queue = new Queue<string>();

    public void AddTask(string description, QuestReward qr)
    {
        buchDerAufgaben[description] = qr;
        _queue.Enqueue(description);
    }

    public string? NextTask()
    {
        try
        {
            return _queue.Dequeue();
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
}


public class Hobbit(string name, Journey journey)
{
    public string Name { get; private set; } = name;
    public Journey journey { get; private set; } = journey;
    public readonly Dictionary<string, Item> Inventory = new Dictionary<string, Item>();

    // The Fool of a Took müsste eigentlich negativ sein.
    public int WeisheitsLVL { get; private set; } = 0;
    public int LVL { get; private set; } = 1;

    public void AddItem(Item item)
    {
        if (!Inventory.ContainsKey(item.Name))
        {
            Inventory.Add(item.Name, item);
        }
    }

    public bool AufgabeErledigen()
    {
        QuestReward qr;
        string? task = journey.NextTask();

        if (task == null)
        {
            Console.WriteLine($"{Name} ist am Ziel");
            return false;
        }
        else
        {
            qr = journey.buchDerAufgaben[task];
        }

        foreach (Item item in qr.Items)
        {
            if (!Inventory.ContainsValue(item))
            {
                Console.WriteLine($"{Name} hat leider nicht das benötigte Item \"{item.Name}\" für den Quest \"{task}\".");
                return false;
            }
        }

        Console.WriteLine($"{Name} hat die benötigten Items für die Aufgabe \"{task}\" und gewinnt {qr.XP} XP.");

        WeisheitsLVL += qr.XP;
        LVL++;
        return true;
    }
}




class Program
{
    static void Main(string[] args)
    {
        var itemDB = new Dictionary<string, Item>();

        itemDB["Uniform der Wache von Gondor"] = new Item("Uniform der Wache von Gondor",
            "Ein weißes Abzeichen auf schwarzem Mantel, das ihn als Diener der Stadt ausweist.");
        
        itemDB["Lanze der Wache"] = new Item("Lanze der Wache",
            "Kein Mittel zum Kampf, sondern ein Zeichen seiner Verpflichtung.");
        
        itemDB["Brot der Stadt"] =  new Item("Brot der Stadt",
            "Lembas-ähnlich, um die Nachtwache zu überstehen");
        
        itemDB["Fackel der Wachen"] = new Item("Fackel der Wachen",
             "Um das erste Feuer zu entzünden");

        itemDB["Harz der ewigen Kiefer"] = new Item("Harz der ewigen Kiefer",
            "Beschleunigt die Entzündung");
        
        itemDB["Wasser aus dem Brunnen des Hofes"] = new Item("Wasser aus dem Brunnen des Hofes",
            "Um Faramir zu benetzen und wachzuhalten.");
        
        itemDB["Trompete der Stadtwache"] = new Item("Trompete der Stadtwache",
            "Um Verstärkung zu rufen.");
        itemDB["Schwert \"Kurzbeil\""] = new Item("Schwert \"Kurzbeil\"",
            "Nicht zum Töten, sondern um den Weg freizukämpfen");



        Journey journey = new Journey();

        journey.AddTask("Die Wache für Bergil",
            new QuestReward(100, new List<Item> { itemDB["Uniform der Wache von Gondor"],
                                                  itemDB["Lanze der Wache"],
                                                  itemDB["Brot der Stadt"] }

        ));

        journey.AddTask("Die Flammen der Hoffnung",
            new QuestReward(300, new List<Item> { itemDB["Fackel der Wachen"],
                                                  itemDB["Harz der ewigen Kiefer"] }
        ));

        journey.AddTask("Der Dienst bei Denethor",
            new QuestReward(250, new List<Item> { itemDB["Wasser aus dem Brunnen des Hofes"],
                                                  itemDB["Trompete der Stadtwache"],
                                                  itemDB["Schwert \"Kurzbeil\""] }
        ));


        Hobbit pippin = new Hobbit("Peregrin Took", journey);

        // Fehler: Pippin hat noch keine Items
        // pippin.AufgabeErledigen();

        // Erster Quest
        pippin.AddItem(itemDB["Uniform der Wache von Gondor"]);
        pippin.AddItem(itemDB["Lanze der Wache"]);
        pippin.AddItem(itemDB["Brot der Stadt"]);
        pippin.AufgabeErledigen();

        pippin.AddItem(itemDB["Fackel der Wachen"]);
        pippin.AddItem(itemDB["Harz der ewigen Kiefer"]);
        pippin.AufgabeErledigen();

        // Sollte nicht funktionieren, da Pippin nicht die benötigten Gegenstände hat.
        pippin.AddItem( new Item("Laserschwert", "In grün"));
        pippin.AufgabeErledigen();

        // (Ja, das ist ein Fehler, weil der Quest wieder in die Queue sollte. Aber das ist mir jetzt egal.)

        // Die Queue ist nun leer, und Pippin ist am Ziel.
        pippin.AufgabeErledigen();

        
        Console.WriteLine($"Pippin  XP: {pippin.WeisheitsLVL}  LVL: {pippin.LVL}");
    }
}
