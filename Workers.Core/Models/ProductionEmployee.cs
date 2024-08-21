using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Core.Enums;

namespace Employees.Core.Contracts
{
    public class ProductionEmployee : Employee
    {
        public Shifts Shift { get; set; }

        public ProductionEmployee(string name, string surname, byte age, byte shift) : base(name, surname, age)
        {
            Shift = (Shifts)shift;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nShift: {Shift}";
        }
    }
}
