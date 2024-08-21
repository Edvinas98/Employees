using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Core.Repositories;

namespace Employees.Core.Contracts
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployees();
        Task<bool> AddEmployee(Employee employee);
    }
}
