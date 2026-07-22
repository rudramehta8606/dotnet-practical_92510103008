using System;
using System.Collections.Generic;

// ============================================================
// Student Admission Management Module
// OOP Concepts: Class, Object, Constructor, Access Modifiers
// ============================================================

namespace StudentAdmissionManagement
{
    // ----- Enum for Admission Status -----
    public enum AdmissionStatus
    {
        Pending,
        Approved,
        Rejected
    }

    // =========================================================
    // BASE CLASS: Person
    // Demonstrates: Class, Access Modifiers (private, protected, public)
    // =========================================================
    public class Person
    {
        // Private fields — accessible only within this class
        private string name;
        private DateTime dateOfBirth;
        private string contactNumber;

        // Protected field — accessible in this class and derived classes
        protected string email;

        // ----- Default Constructor -----
        public Person()
        {
            name = "Unknown";
            dateOfBirth = DateTime.MinValue;
            contactNumber = "N/A";
            email = "N/A";
        }

        // ----- Parameterized Constructor -----
        public Person(string name, DateTime dateOfBirth, string contactNumber, string email)
        {
            this.name = name;
            this.dateOfBirth = dateOfBirth;
            this.contactNumber = contactNumber;
            this.email = email;
        }

        // Public Properties — controlled access to private fields
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public string ContactNumber
        {
            get { return contactNumber; }
            set { contactNumber = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        // Public method to calculate age
        public int GetAge()
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age--;
            return age;
        }

        // Public virtual method — can be overridden in derived classes
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"  Name           : {name}");
            Console.WriteLine($"  Date of Birth  : {dateOfBirth:dd-MM-yyyy}");
            Console.WriteLine($"  Age            : {GetAge()}");
            Console.WriteLine($"  Contact Number : {contactNumber}");
            Console.WriteLine($"  Email          : {email}");
        }
    }

    // =========================================================
    // DERIVED CLASS: Student (inherits from Person)
    // Demonstrates: Inheritance, Constructor Chaining
    // =========================================================
    public class Student : Person
    {
        // Private fields specific to Student
        private static int studentCounter = 1000;
        private int studentId;
        private string course;
        private double percentage;
        private AdmissionStatus status;
        private DateTime applicationDate;

        // ----- Default Constructor — calls base default constructor -----
        public Student() : base()
        {
            studentId = ++studentCounter;
            course = "Not Assigned";
            percentage = 0.0;
            status = AdmissionStatus.Pending;
            applicationDate = DateTime.Now;
        }

        // ----- Parameterized Constructor — calls base parameterized constructor -----
        public Student(string name, DateTime dob, string contact, string email,
                       string course, double percentage)
            : base(name, dob, contact, email)
        {
            studentId = ++studentCounter;
            this.course = course;
            this.percentage = percentage;
            this.status = AdmissionStatus.Pending;
            this.applicationDate = DateTime.Now;
        }

        // ----- Copy Constructor -----
        public Student(Student other)
            : base(other.Name, other.DateOfBirth, other.ContactNumber, other.Email)
        {
            studentId = ++studentCounter;
            this.course = other.course;
            this.percentage = other.percentage;
            this.status = other.status;
            this.applicationDate = other.applicationDate;
        }

        // Public Properties
        public int StudentId
        {
            get { return studentId; }
        }

        public string Course
        {
            get { return course; }
            set { course = value; }
        }

        public double Percentage
        {
            get { return percentage; }
            set
            {
                if (value >= 0 && value <= 100)
                    percentage = value;
                else
                    Console.WriteLine("  [Error] Percentage must be between 0 and 100.");
            }
        }

        public AdmissionStatus Status
        {
            get { return status; }
        }

        public DateTime ApplicationDate
        {
            get { return applicationDate; }
        }

        // Public method to approve / reject admission based on percentage
        public void ProcessAdmission(double cutoff)
        {
            if (percentage >= cutoff)
                status = AdmissionStatus.Approved;
            else
                status = AdmissionStatus.Rejected;
        }

        // Override DisplayInfo from base class
        public override void DisplayInfo()
        {
            Console.WriteLine($"\n  ──── Student ID : {studentId} ────");
            base.DisplayInfo();  // call Person.DisplayInfo()
            Console.WriteLine($"  Course         : {course}");
            Console.WriteLine($"  Percentage     : {percentage:F2}%");
            Console.WriteLine($"  Status         : {status}");
            Console.WriteLine($"  Applied On     : {applicationDate:dd-MM-yyyy}");
        }
    }

    // =========================================================
    // CLASS: AdmissionManager
    // Manages a collection of Student objects
    // Demonstrates: Composition, Encapsulation
    // =========================================================
    public class AdmissionManager
    {
        // Private list — encapsulated, not directly accessible outside
        private List<Student> students;
        private double cutoffPercentage;

        // ----- Constructor -----
        public AdmissionManager(double cutoffPercentage)
        {
            students = new List<Student>();
            this.cutoffPercentage = cutoffPercentage;
        }

        // Public property for cutoff
        public double CutoffPercentage
        {
            get { return cutoffPercentage; }
            set { cutoffPercentage = value; }
        }

        // Add a new student application
        public void AddStudent(Student student)
        {
            students.Add(student);
            Console.WriteLine($"  [+] Student '{student.Name}' added with ID {student.StudentId}.");
        }

        // Process all pending admissions against the cutoff
        public void ProcessAllAdmissions()
        {
            Console.WriteLine($"\n  Processing admissions (Cutoff: {cutoffPercentage}%)...");
            foreach (Student s in students)
            {
                if (s.Status == AdmissionStatus.Pending)
                    s.ProcessAdmission(cutoffPercentage);
            }
            Console.WriteLine("  All pending applications processed.");
        }

        // Display all students
        public void DisplayAllStudents()
        {
            Console.WriteLine("\n  ══════════════════════════════════════════════");
            Console.WriteLine("           ALL STUDENT RECORDS");
            Console.WriteLine("  ══════════════════════════════════════════════");
            if (students.Count == 0)
            {
                Console.WriteLine("  No student records found.");
                return;
            }
            foreach (Student s in students)
                s.DisplayInfo();
        }

        // Display students filtered by admission status
        public void DisplayByStatus(AdmissionStatus status)
        {
            Console.WriteLine($"\n  ── Students with Status: {status} ──");
            bool found = false;
            foreach (Student s in students)
            {
                if (s.Status == status)
                {
                    s.DisplayInfo();
                    found = true;
                }
            }
            if (!found)
                Console.WriteLine("  No students found with this status.");
        }

        // Search student by ID
        public Student SearchById(int id)
        {
            foreach (Student s in students)
            {
                if (s.StudentId == id)
                    return s;
            }
            return null;
        }

        // Get total counts
        public void DisplaySummary()
        {
            int pending = 0, approved = 0, rejected = 0;
            foreach (Student s in students)
            {
                if (s.Status == AdmissionStatus.Pending) pending++;
                else if (s.Status == AdmissionStatus.Approved) approved++;
                else rejected++;
            }

            Console.WriteLine("\n  ══════════════════════════════════════════════");
            Console.WriteLine("           ADMISSION SUMMARY");
            Console.WriteLine("  ══════════════════════════════════════════════");
            Console.WriteLine($"  Total Applications : {students.Count}");
            Console.WriteLine($"  Pending            : {pending}");
            Console.WriteLine($"  Approved           : {approved}");
            Console.WriteLine($"  Rejected           : {rejected}");
            Console.WriteLine($"  Cutoff Percentage  : {cutoffPercentage}%");
        }
    }

    // =========================================================
    // MAIN PROGRAM — Menu-driven console application
    // =========================================================
    class Program
    {
        static void Main(string[] args)
        {
            AdmissionManager manager = new AdmissionManager(cutoffPercentage: 60.0);

            // Pre-load sample students using parameterized constructors
            manager.AddStudent(new Student("Rudra Mehta", new DateTime(2004, 5, 15),
                "9876543210", "rudra@example.com", "B.Tech CSE", 85.5));
            manager.AddStudent(new Student("Priya Sharma", new DateTime(2005, 2, 20),
                "9123456780", "priya@example.com", "B.Tech IT", 55.0));
            manager.AddStudent(new Student("Aman Patel", new DateTime(2004, 11, 8),
                "9988776655", "aman@example.com", "BCA", 72.3));

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n  ══════════════════════════════════════════════");
                Console.WriteLine("     STUDENT ADMISSION MANAGEMENT SYSTEM");
                Console.WriteLine("  ══════════════════════════════════════════════");
                Console.WriteLine("  1. Add New Student");
                Console.WriteLine("  2. Display All Students");
                Console.WriteLine("  3. Process All Admissions");
                Console.WriteLine("  4. Search Student by ID");
                Console.WriteLine("  5. View Approved Students");
                Console.WriteLine("  6. View Rejected Students");
                Console.WriteLine("  7. View Pending Students");
                Console.WriteLine("  8. Display Admission Summary");
                Console.WriteLine("  9. Change Cutoff Percentage");
                Console.WriteLine("  0. Exit");
                Console.Write("\n  Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewStudent(manager);
                        break;
                    case "2":
                        manager.DisplayAllStudents();
                        break;
                    case "3":
                        manager.ProcessAllAdmissions();
                        break;
                    case "4":
                        SearchStudent(manager);
                        break;
                    case "5":
                        manager.DisplayByStatus(AdmissionStatus.Approved);
                        break;
                    case "6":
                        manager.DisplayByStatus(AdmissionStatus.Rejected);
                        break;
                    case "7":
                        manager.DisplayByStatus(AdmissionStatus.Pending);
                        break;
                    case "8":
                        manager.DisplaySummary();
                        break;
                    case "9":
                        ChangeCutoff(manager);
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("\n  Exiting... Goodbye!");
                        break;
                    default:
                        Console.WriteLine("  [!] Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Helper method to add a new student from user input
        static void AddNewStudent(AdmissionManager manager)
        {
            Console.WriteLine("\n  ── Add New Student ──");

            Console.Write("  Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("  Enter Date of Birth (dd-MM-yyyy): ");
            DateTime dob;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null,
                System.Globalization.DateTimeStyles.None, out dob))
            {
                Console.Write("  [!] Invalid format. Enter DOB (dd-MM-yyyy): ");
            }

            Console.Write("  Enter Contact Number: ");
            string contact = Console.ReadLine();

            Console.Write("  Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("  Enter Course: ");
            string course = Console.ReadLine();

            Console.Write("  Enter Percentage: ");
            double percentage;
            while (!double.TryParse(Console.ReadLine(), out percentage) || percentage < 0 || percentage > 100)
            {
                Console.Write("  [!] Invalid. Enter Percentage (0-100): ");
            }

            // Creating an Object using parameterized constructor
            Student newStudent = new Student(name, dob, contact, email, course, percentage);
            manager.AddStudent(newStudent);
        }

        // Helper method to search for a student
        static void SearchStudent(AdmissionManager manager)
        {
            Console.Write("\n  Enter Student ID to search: ");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                Student found = manager.SearchById(id);
                if (found != null)
                    found.DisplayInfo();
                else
                    Console.WriteLine("  [!] Student not found.");
            }
            else
            {
                Console.WriteLine("  [!] Invalid ID format.");
            }
        }

        // Helper method to change the cutoff percentage
        static void ChangeCutoff(AdmissionManager manager)
        {
            Console.Write("\n  Enter new Cutoff Percentage: ");
            double newCutoff;
            if (double.TryParse(Console.ReadLine(), out newCutoff) && newCutoff >= 0 && newCutoff <= 100)
            {
                manager.CutoffPercentage = newCutoff;
                Console.WriteLine($"  Cutoff updated to {newCutoff}%.");
            }
            else
            {
                Console.WriteLine("  [!] Invalid cutoff value.");
            }
        }
    }
}
