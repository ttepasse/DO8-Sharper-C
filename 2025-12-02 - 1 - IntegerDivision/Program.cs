using System.Data.Common;

namespace _2025_12_2___1___Divide_By_Zero_Exception;

class Program
{
    static int IntegerDivision(string dividend_string, string divisor_string)
    {
        if (dividend_string == String.Empty)
        {
            throw new ArgumentException("Eingabe des Dividends dürfen nicht leer sein.", "dividend");
        }
        else if (divisor_string == String.Empty)
        {
            throw new ArgumentException("Eingabe des Divisors darf nicht leer sein.", "divisor");
        }

        int dividend, divisor;

        try
        {
            dividend = int.Parse(dividend_string);
            divisor = int.Parse(divisor_string);
        }
        catch (FormatException)
        {
            throw;
        }

        if (divisor == 0)
        {
            // throw new ArithmeticException();
            throw new DivideByZeroException();
        }

        return dividend / divisor;
    }


    static void TestIntegerDivision(string dividend, string divisor)
    {
        int result;

        try
        {
            result = IntegerDivision(dividend, divisor);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("ArgumentException: " + ex.Message);
            return;
        }
        catch (FormatException ex)
        {
            Console.WriteLine("FormatException: " + ex.Message);
            return;
        }
        catch (ArithmeticException ex)
        {
            Console.WriteLine("ArithmeticException: " + ex.Message);
            return;
        }

        Console.WriteLine($"Division geglückt: {dividend}/{divisor} = {result}");
    }



    static void Main(string[] args)
    {
        Console.WriteLine("Test: Leere Strings");
        TestIntegerDivision("", "");
        
        Console.WriteLine("\nTest: Keine Zahlen");
        TestIntegerDivision("foo", "bar");

        Console.WriteLine("\nTest: Division durch 0");
        TestIntegerDivision("11", "0");

        Console.WriteLine("\nTest: Endlich richtige Division");
        TestIntegerDivision("33", "11");
    }
}
