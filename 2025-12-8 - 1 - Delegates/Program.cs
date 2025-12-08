//  • Erstellen Sie eine Klasse namens Calculator, die vier grundlegende arithmetische
//    Operationen (Addition, Subtraktion, Multiplikation, Division) als Methoden implementiert. 
//  • Jede dieser Methoden soll zwei int-Parameter entgegennehmen und das entsprechende
//    Ergebnis zurückgeben. 
//  • Anschließend definieren Sie zwei Delegaten: einen Func<int, int, int>-Delegaten 
//    namens operation und einen Action<int, int>-Delegaten namens displayResult. 
//  • Weisen Sie dem operation-Delegaten die Methode Add zu und rufen Sie sie mit den Werten 4 
//    und 2 auf, um das Ergebnis zu berechnen.
//  • Speichern Sie das Ergebnis in einer Variablen. 
//  • Anschließend weisen Sie dem displayResult-Delegaten eine anonyme Methode zu,
//    die die berechnete Summe ausgibt. 
//  • Rufen Sie den displayResult-Delegaten auf, um die Ausgabe zu erzeugen. 


namespace _2025_12_8___1___Delegates;


delegate int Operation(int a, int b);

delegate void DisplayResult(int a);


public class Calculator
{
    public static int Add(int a, int b) { return a + b; }
    public static int Subtract(int a, int b) { return a - b; }
    public static int Multiply(int a, int b) { return a * b; }
    public static int Divide(int a, int b) { return a / b; }
}


class Program
{
    static void Main(string[] args)
    {
        // Weisen Sie dem operation-Delegaten die Methode Add zu und rufen Sie sie mit den Werten 4 und 2 auf, um das Ergebnis zu berechnen.
        Operation op = Calculator.Add;

        // Speichern Sie das Ergebnis in einer Variablen. 
        int result = op(2, 4);

        //Anschließend weisen Sie dem displayResult-Delegaten eine anonyme Methode zu,
        // die die berechnete Summe ausgibt.
        DisplayResult print = (int x) => Console.WriteLine(x);

        //  • Rufen Sie den displayResult-Delegaten auf, um die Ausgabe zu erzeugen
        print(result);
    }
}
