  using System;

class Student
{
    // Private Variables
    private string name, field, city, bus, school;
    private double tenth, twelfth;
    private int age, rank;

    // Constructor
    public Student()
    {
        Console.WriteLine("===== Student Admission Management System =====");
    }

    // Input Method
    public void Input()
    {
        Console.Write("Enter Student Name: ");
        name = Console.ReadLine();

        Console.Write("Enter Field for Admission: ");
        field = Console.ReadLine();

        Console.Write("Enter 10th Percentage: ");
        tenth = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter 12th Percentage: ");
        twelfth = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Age: ");
        age = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter City: ");
        city = Console.ReadLine();

        Console.Write("Bus Required (Yes/No): ");
        bus = Console.ReadLine();

        Console.Write("Enter Merit Rank: ");
        rank = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter School Name: ");
        school = Console.ReadLine();
    }

    // Display Method
    public void Display()
    {
        Console.WriteLine("\n----- Student Details -----");
        Console.WriteLine("Name : " + name);
        Console.WriteLine("Field : " + field);
        Console.WriteLine("10th Percentage : " + tenth);
        Console.WriteLine("12th Percentage : " + twelfth);
        Console.WriteLine("Age : " + age);
        Console.WriteLine("City : " + city);
        Console.WriteLine("Bus Required : " + bus);
        Console.WriteLine("Merit Rank : " + rank);
        Console.WriteLine("School Name : " + school);

        Console.WriteLine("\n----- Admission Result -----");

        if (tenth >= 50 && twelfth >= 50 && age >= 17 && rank <= 500)
        {
            Console.WriteLine("Status : Eligible for Admission");
        }
        else
        {
            Console.WriteLine("Status : Not Eligible");
            Console.WriteLine("Reason:");

            if (tenth < 50)
                Console.WriteLine("- 10th Percentage is below 50%");

            if (twelfth < 50)
                Console.WriteLine("- 12th Percentage is below 50%");

            if (age < 17)
                Console.WriteLine("- Age must be 17 years or above");

            if (rank > 500)
                Console.WriteLine("- Merit Rank is above 500");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Student s = new Student();   // Object Creation

        s.Input();
        s.Display();

        Console.ReadKey();
    }
}
