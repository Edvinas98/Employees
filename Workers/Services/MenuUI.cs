using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Employees.Core.Services;
using Employees.Core.Contracts;
using Employees.Core.Enums;
using System.Diagnostics.Metrics;

namespace Employees.EntryPoint.Services
{
    public class MenuUI
    {
        private IEmployeeService _employeeService;
        public MenuUI(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task LaunchMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Show all employees");
                Console.WriteLine("2. Add an employee");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");
                GetInput(out string choice);
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        List<Employee> employees = await _employeeService.GetEmployees();
                        if (employees.Count == 0)
                        {
                            Console.WriteLine("There are no employees in the list");
                            break;
                        }
                        foreach (Employee employee in employees)
                        {
                            Console.WriteLine(employee);
                            Console.WriteLine();
                        }
                        break;
                    case "2":
                        Console.Write("Enter if this employee is an office employee (true/false): ");
                        GetInput(out bool bOffice);
                        Console.Write("Enter name: ");
                        GetInput(out string name);
                        Console.Write("Enter surnamename: ");
                        GetInput(out string surname);
                        Console.Write("Enter age: ");
                        GetInput(out byte age);
                        if (bOffice)
                        {
                            Console.WriteLine("Available departments: ");
                            for(byte i = (byte)Enum.GetValues(typeof(Departments)).Cast<Departments>().First(); i <= (byte)Enum.GetValues(typeof(Departments)).Cast<Departments>().Last(); i++)
                            {
                                Console.WriteLine($"{i} - {(Departments)i}");
                            }
                            Console.Write("Select a dapartment: ");
                            GetInput(out byte department, (byte)Enum.GetValues(typeof(Departments)).Cast<Departments>().First(), (byte)Enum.GetValues(typeof(Departments)).Cast<Departments>().Last());
                            Console.WriteLine();
                            if (await _employeeService.AddEmployee(new OfficeEmployee(name, surname, age, department)))
                                Console.WriteLine("Employee was added successfully");
                            else
                                Console.WriteLine("Employee with such data is already in the list!");
                        }
                        else
                        {
                            Console.WriteLine("Available shifts: ");
                            for (byte i = (byte)Enum.GetValues(typeof(Shifts)).Cast<Shifts>().First(); i <= (byte)Enum.GetValues(typeof(Shifts)).Cast<Shifts>().Last(); i++)
                            {
                                Console.WriteLine($"{i} - {(Shifts)i}");
                            }
                            Console.Write("Select a shift: ");
                            GetInput(out byte shift, (byte)Enum.GetValues(typeof(Shifts)).Cast<Shifts>().First(), (byte)Enum.GetValues(typeof(Shifts)).Cast<Shifts>().Last());
                            Console.WriteLine();
                            if (await _employeeService.AddEmployee(new ProductionEmployee(name, surname, age, shift)))
                                Console.WriteLine("Employee was added successfully");
                            else
                                Console.WriteLine("Employee with such data is already in the list!");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Wrong input.");
                        Console.WriteLine();
                        break;
                }
                Console.WriteLine();
            }
        }

        public static void GetInput(out string input)
        {
            while (true)
            {
                input = Console.ReadLine() ?? string.Empty;
                if (input != "")
                    return;
                else
                    Console.Write("Error, try again: ");
            }
        }

        public static void GetInput(out bool input)
        {
            while (true)
            {
                if (!bool.TryParse(Console.ReadLine(), out input))
                    Console.Write("Error, try again: ");
                else
                    return;
            }
        }

        public static void GetInput(out byte input)
        {
            while (true)
            {
                if (!byte.TryParse(Console.ReadLine(), out input) || input <= 0)
                    Console.Write("Error, try again: ");
                else
                    return;
            }
        }

        public static void GetInput(out byte input, byte min, byte max)
        {
            while (true)
            {
                if (!byte.TryParse(Console.ReadLine(), out input) || input < min || input > max)
                    Console.Write("Error, try again: ");
                else
                    return;
            }
        }
    }
}
