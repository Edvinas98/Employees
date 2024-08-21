using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Core.Enums;

namespace Employees.Core.Contracts
{
    public class OfficeEmployee : Employee
    {
        public Departments Department {  get; set; }

        public OfficeEmployee(string name, string surname, byte age, byte department) : base(name, surname, age)
        {
            Department = (Departments)department;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nDepartment: {Department}";
        }
    }
}
