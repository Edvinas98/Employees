using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Core.Contracts;

namespace Employees.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDbRepository _employeeDbRepository;

        public EmployeeService(IEmployeeDbRepository employeeDbRepository)
        {
            _employeeDbRepository = employeeDbRepository;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeDbRepository.GetEmployees();
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            return await _employeeDbRepository.AddEmployee(employee);
        }
    }
}
