using System.Text;
using System.Text.Json;

namespace _2026_01_09___Fakestore;


public class Rating
{
    public float rate { get; set; }
    public int count { get; set; }
}

public class Product
{
    public int id { get; set; }
    public string title { get; set; } = "";
    public decimal price { get; set; }
    public string description { get; set; } = "";
    public string category { get; set; } = "";
    public string image { get; set; } = "";
    public Rating? rating { get; set; }
}


public class TUIList(IEnumerable<Product> items)
{
    private List<Product> products = items.ToList();

    public void DisplayProduct(Product product)
    {
        Console.Clear();

        Console.WriteLine(product.title);
        Console.WriteLine($"  Beschreibung: {product.description}");
        Console.WriteLine($"  Preis:        {product.price} €");
        Console.WriteLine($"  Kategorie:    {product.category}");
        Console.WriteLine($"  Rating:       {product.rating!.rate}");

        Console.ReadKey();
    }

    public void Show()
    {
        Console.Clear();

        TUIMenu menu = new();

        int maxTitleLength = products.Select((p) => p.title.Length).Max();

        foreach (Product product in products)
        {
            string line = $"{product.title.PadRight(maxTitleLength)} {product.price,7:F2} €    Rating: {product.rating!.rate,2:F2}    Kategorie: {product.category}";
            menu.AddMenuItem(line, () => DisplayProduct(product));
        }

        menu.Show();
    }
}


public class TUIMenuItem(string content, Action action)
{
    public string Content { get; set; } = content;
    public Action Action { get; set; } = action;
}


public class TUIMenu
{
    private int counter = 1;
    private SortedList<int, TUIMenuItem> items = new();

    public void AddMenuItem(string text, Action action)
    {
        items.Add(counter, new TUIMenuItem(text, action));
        counter++;
    }

    public void Show()
    {
        {
            while (true)
            {
                Console.Clear();

                foreach (int key in this.items.Keys)
                {
                    Console.WriteLine($"[{key,2}]  {items[key].Content}");
                }
                Console.WriteLine();
                Console.WriteLine("[ 0]  Quit");

                Console.WriteLine();
                Console.Write("> ");

                try
                {
                    string input = Console.ReadLine()!;

                    if (input.ToLower() == "0") { return; }

                    int number = Convert.ToInt32(input);
                    if (items.ContainsKey(number))
                    {
                        items[number].Action();
                    }
                }
                catch { continue; }
            }
        }
    }
}


public class Program
{
    static List<Product> productList = new();

    static async Task GetFakestoreData()
    {
        using HttpClient client = new();

        string url = "https://fakestoreapi.com/products";

        Console.WriteLine("Requesting Data ...");

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string jsonData = await response.Content.ReadAsStringAsync();

            productList = JsonSerializer.Deserialize<List<Product>>(jsonData)!;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }


    public static void SortByTitle()
    {
        new TUIList(productList.OrderBy((p) => p.title)).Show();
    }


    public static void SortByCategory()
    {
        new TUIList(productList.OrderBy((p) => p.category)).Show();
    }


    public static void SortByRating()
    {
        new TUIList(productList.OrderBy((p) => p.rating!.rate)).Show();
    }


    public static void SortByMinPrice()
    {
        new TUIList(productList.OrderBy((p) => p.price)).Show();
    }


    public static void SortByMaxPrice()
    {
        new TUIList(productList.OrderByDescending((p) => p.price)).Show();
    }



    public static async Task Main()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;

        await GetFakestoreData();


        TUIMenu m = new();
        m.AddMenuItem("Nach Titel sortieren", SortByTitle);
        m.AddMenuItem("Nach Kategorien sortieren", SortByCategory);
        m.AddMenuItem("Nach Rating sortieren", SortByRating);
        m.AddMenuItem("Nach min. Preis sortieren", SortByMinPrice);
        m.AddMenuItem("Nach max. Preis sortieren", SortByMaxPrice);

        m.Show();
    }
}
