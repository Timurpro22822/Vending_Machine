class Program
{
    static void Main(string[] args)
    {
        VendingMachine machine = new VendingMachine();

        string choice;
        do
        {
            machine.ShowMenu();
            choice = Console.ReadLine();
            machine.ProcessUserInput(choice);
        } while (true);
    }
}