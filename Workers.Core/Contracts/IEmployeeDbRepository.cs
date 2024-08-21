using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Core.Contracts
{
    public interface IEmployeeDbRepository
    {
        Task<bool> AddEmployee(Employee employee);
        Task<List<Employee>> GetEmployees();
    }
}
