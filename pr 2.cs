using System;

// Interface
interface IEmployee
{
    void Display();
}

// Parent Class
class Employee
{
    public int Id;
    public string Name;
    public double Salary;

    public void Input()
    {
        Console.Write("Enter Employee ID: ");
        Id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Employee Name: ");
        Name = Console.ReadLine();

        Console.Write("Enter Employee Salary: ");
        Salary = Convert.ToDouble(Console.ReadLine());
    }
}

// Child Class (Inheritance)
class EmployeeDetails : Employee, IEmployee
{
    public void Display()
    {
        Console.WriteLine("\n----- Employee Details -----");
        Console.WriteLine("Employee ID    : " + Id);
        Console.WriteLine("Employee Name  : " + Name);
        Console.WriteLine("Employee Salary: " + Salary);
    }
}

// Main Class
class Program
{
    static void Main(string[] args)
    {
        EmployeeDetails emp = new EmployeeDetails();

        // Inherited method
        emp.Input();

        // Polymorphism using Interface
        IEmployee obj = emp;
        obj.Display();

        Console.WriteLine("\nThank You!");
    }
}