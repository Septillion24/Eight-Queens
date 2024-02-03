

class Program
{

    static void Main()
    {
        Algorithm algorithm = new();
        // algorithm.displayCurrentState();

        bool win = false;
        do
        {
            win = algorithm.doNextCalculationStep();
        }
        while (!win);
        Console.WriteLine("\n\nFound final state.");
        algorithm.displayCurrentState();
        Console.WriteLine("\nState changes: " + algorithm.getStateChanges() + "\nRestarts: " + algorithm.getRestarts());



    }
}