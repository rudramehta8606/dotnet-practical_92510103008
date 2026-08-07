using System;

class Expense
{
    public string ExpenseName;
    public double Amount;

    // Method to enter expense details
    public void Input()
    {
        Console.Write("Enter Expense Name: ");
        ExpenseName = Console.ReadLine();

        Console.Write("Enter Expense Amount: ");
        Amount = Convert.ToDouble(Console.ReadLine());
    }

    // Method to display expense details
    public void Display()
    {
        Console.WriteLine("Expense Name : " + ExpenseName);
        Console.WriteLine("Amount       : Rs. " + Amount);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Expense e = new Expense();

        try
        {
            Console.WriteLine("===== Expense Tracking Module =====");
            Console.WriteLine();

            e.Input();

            Console.WriteLine();
            Console.WriteLine("----- Expense Details -----");
            e.Display();

            Console.WriteLine();
            Console.WriteLine("Total Expense: Rs. " + e.Amount);
        }

        catch (FormatException)
        {
            Console.WriteLine("Error: Please enter numbers only for the expense amount.");
        }

        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        finally
        {
            Console.WriteLine();
            Console.WriteLine("Thank you for using Expense Tracker.");
        }
    }
}

