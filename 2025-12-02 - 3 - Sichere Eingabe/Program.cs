// Aufgabe Sichere Eingabe:
// Schreiben Sie eine statische Klasse Eingabe.
// Die Klasse soll verschiedene statische Methoden zur sicheren Eingabe von Werten wie z.B. Integer oder Double enthalten.
// Den Methoden soll jeweils ein Text übergeben werden, der zur Eingabe angezeigt wird. Die Methoden sollen dann so lange eine Eingabe vom Nutzer fordern, bis ein gültiger Wert eingegeben wurde. Innerhalb der Methoden sollen ggf. auftretende Exceptions sauber gefangen und behandelt werden. Abschließend geben die Methoden den eingegebenen Wert an den Aufrufer zurück.
//     a) Beginnen Sie mit der Methode Integer zur sicheren Eingabe von Integer-Werten. Geben Sie dem Benutzer bei evtl. auftretenden Fehlern eine aussagekräftige Meldung in der Konsole aus und fordern Sie ihn erneut zur Eingabe auf.
//     b) Schreiben Sie eine zweite Methode Integer (Methodenüberladung), die zusätzlich zum Text zwei Parameter für den Min- und den Max-Wert der Eingabe entgegennimmt. Die Methode soll dann solange zur Eingabe auffordern, bis eine Zahl im definierten Bereich eingegeben wurde.
//     c) Erweitern Sie Ihre Klasse um Methoden für z.B. Double- und Float-Eingaben.

namespace _2025_12_2___3___Sichere_Eingabe;


public class Input
{
    public static int Integer()
    {
        int number;
        string input;

        while (true)
        {
            try
            {
                Console.Write("Eine Zahl bitte: ");
                input = Console.ReadLine()!;
                if (input == String.Empty)
                {
                    throw new ArgumentException();
                }
                number = int.Parse(input);
                break;
            }
            catch (ArgumentNullException)
            {
                // Ist das überhaupt möglich?
                Console.WriteLine("Die Eingabe darf nicht null sein.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Die Eingabe darf nicht leer sein.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Die Eingabe muss aus Ziffern bestehen.");
            }
        }
        return number;
    }


    public static int Integer(int minimum, int maximum)
    {
        int number = 0;
        string input;

        while (true)
        {
            try
            {
                Console.Write("Eine Zahl bitte: ");
                input = Console.ReadLine()!;
                
                if (input == String.Empty)
                {
                    throw new ArgumentException();
                }
                
                number = int.Parse(input);

                if (minimum > number || number > maximum)
                {
                    Console.WriteLine($"Zahl muss zwischen {minimum} und {maximum} sein.");
                    continue;
                }

                break;
            }
            catch (ArgumentNullException)
            {
                // Ist das überhaupt möglich?
                Console.WriteLine("Die Eingabe darf nicht null sein.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Die Eingabe darf nicht leer sein.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Die Eingabe muss aus Ziffern bestehen.");
            }
        }

        return number;
    }


    public static double Double()
    {
        double number = 0;
        string input;

        while (true)
        {
            try
            {
                Console.Write("Eine Zahl bitte: ");
                input = Console.ReadLine()!;
                
                if (input == String.Empty)
                {
                    throw new ArgumentException();
                }
                if (input.Contains("."))
                {
                    throw new FormatException();
                }
                
                number = Convert.ToDouble(input);

                break;
            }
            catch (ArgumentNullException)
            {
                // Ist das überhaupt möglich?
                Console.WriteLine("Die Eingabe darf nicht null sein.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Die Eingabe darf nicht leer sein.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Die Eingabe muss aus Ziffern oder einem Komma bestehen.");
            }
        }

        return number;
    }

    // public static double
}



class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        double d = Input.Double();
        Console.WriteLine(d);
    }
}
