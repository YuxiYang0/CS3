using System;
using System.Collections.Generic;

namespace DemoOOP
{
    // Abstraction
    public interface IPersonService
    {
        int CalculateAge();
        decimal CalculateSalary();
        void AddAddress(string address);
        List<string> GetAddresses();
    }

    public interface IStudentService : IPersonService
    {
        void AddCourse(Course course, char grade);
        double CalculateGPA();
    }

    public interface IInstructorService : IPersonService
    {
        void SetDepartment(Department department);
        int CalculateExperience();
    }

    // Encapsulation
    public abstract class Person : IPersonService
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        private List<string> _addresses = new List<string>();
        protected decimal _salary;

        public Person(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public int CalculateAge()
        {
            return DateTime.Now.Year - DateOfBirth.Year;
        }

        public abstract decimal CalculateSalary();

        public void AddAddress(string address)
        {
            _addresses.Add(address);
        }

        public List<string> GetAddresses()
        {
            return _addresses;
        }
    }

    public class Student : Person, IStudentService
    {
        private Dictionary<Course, char> _courses = new Dictionary<Course, char>();

        public Student(string name, DateTime dateOfBirth) : base(name, dateOfBirth)
        {
        }

        public override decimal CalculateSalary()
        {
            throw new NotImplementedException("Students do not have a salary.");
        }

        public void AddCourse(Course course, char grade)
        {
            _courses.Add(course, grade);
        }

        public double CalculateGPA()
        {
            double totalPoints = 0;
            foreach (var grade in _courses.Values)
            {
                totalPoints += GradeToPoint(grade);
            }
            return totalPoints / _courses.Count;
        }

        private double GradeToPoint(char grade)
        {
            switch (grade)
            {
                case 'A': return 4.0;
                case 'B': return 3.0;
                case 'C': return 2.0;
                case 'D': return 1.0;
                default: return 0.0;
            }
        }
    }

    public class Instructor : Person, IInstructorService
    {
        public DateTime JoinDate { get; set; }
        public Department Department { get; private set; }
        private decimal _bonus;

        public Instructor(string name, DateTime dateOfBirth, DateTime joinDate, decimal salary, decimal bonus) : base(name, dateOfBirth)
        {
            JoinDate = joinDate;
            _salary = salary;
            _bonus = bonus;
        }

        public override decimal CalculateSalary()
        {
            return _salary + _bonus;
        }

        public void SetDepartment(Department department)
        {
            Department = department;
        }

        public int CalculateExperience()
        {
            return DateTime.Now.Year - JoinDate.Year;
        }
    }

    public class Course
    {
        public string Name { get; set; }
        public List<Student> EnrolledStudents { get; private set; } = new List<Student>();

        public Course(string name)
        {
            Name = name;
        }

        public void EnrollStudent(Student student)
        {
            EnrolledStudents.Add(student);
        }
    }

    public class Department
    {
        public string Name { get; set; }
        public Instructor Head { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Course> Courses { get; private set; } = new List<Course>();

        public Department(string name, Instructor head, decimal budget, DateTime startDate, DateTime endDate)
        {
            Name = name;
            Head = head;
            Budget = budget;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }
    }

    // Inheritance & Polymorphism
    public class Color
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Alpha { get; set; }

        public Color(int red, int green, int blue, int alpha = 255)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public int GetGrayscaleValue()
        {
            return (Red + Green + Blue) / 3;
        }
    }

    public class Ball
    {
        public int Size { get; private set; }
        public Color Color { get; set; }
        private int _throwCount;

        public Ball(int size, Color color)
        {
            Size = size;
            Color = color;
            _throwCount = 0;
        }

        public void Pop()
        {
            Size = 0;
        }

        public void Throw()
        {
            if (Size > 0)
            {
                _throwCount++;
            }
        }

        public int GetThrowCount()
        {
            return _throwCount;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            Ball ball1 = new Ball(10, new Color(255, 0, 0));
            Ball ball2 = new Ball(15, new Color(0, 255, 0));

            ball1.Throw();
            ball1.Throw();
            ball2.Throw();

            ball1.Pop();
            ball1.Throw(); 

            Console.WriteLine($"Ball 1 throw count: {ball1.GetThrowCount()}");
            Console.WriteLine($"Ball 2 throw count: {ball2.GetThrowCount()}");

            Student student = new Student("John Doe", new DateTime(2000, 1, 1));
            Instructor instructor = new Instructor("Jane Smith", new DateTime(1980, 5, 15), new DateTime(2005, 9, 1), 50000m, 10000m);

            student.AddCourse(new Course("Math"), 'A');
            student.AddCourse(new Course("Science"), 'B');

            instructor.SetDepartment(new Department("Computer Science", instructor, 100000m, new DateTime(2021, 1, 1), new DateTime(2021, 12, 31)));

            Console.WriteLine($"Student GPA: {student.CalculateGPA()}");
            Console.WriteLine($"Instructor Salary: {instructor.CalculateSalary()}");
            Console.WriteLine($"Instructor Experience: {instructor.CalculateExperience()} years");
        }
    }
}
