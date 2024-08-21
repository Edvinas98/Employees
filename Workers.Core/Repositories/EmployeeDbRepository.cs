using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Core.Contracts;
using MongoDB.Driver;

namespace Employees.Core.Repositories
{
    public class EmployeeDbRepository : IEmployeeDbRepository
    {
        private readonly IMongoCollection<OfficeEmployee> _officeEmployeeDatabase;
        private readonly IMongoCollection<ProductionEmployee> _productionEmployeeDatabase;

        public EmployeeDbRepository(IMongoClient mongoClient)
        {
            _officeEmployeeDatabase = mongoClient.GetDatabase("employees").GetCollection<OfficeEmployee>("office_employees_database");
            _productionEmployeeDatabase = mongoClient.GetDatabase("employees").GetCollection<ProductionEmployee>("production_employees_database");
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            if(await CheckNewEmployee(employee))
                return false;

            if(employee is OfficeEmployee)
                await _officeEmployeeDatabase.InsertOneAsync((OfficeEmployee)employee);
            else
                await _productionEmployeeDatabase.InsertOneAsync((ProductionEmployee)employee);
            return true;
        }

        private async Task<bool> CheckNewEmployee(Employee employee)
        {
            int count = 0;
            if (employee is OfficeEmployee)
            {
                count = (await _officeEmployeeDatabase.FindAsync<OfficeEmployee>(x => x.Name == employee.Name && x.Surname == employee.Surname && x.Age == employee.Age && x.Department == ((OfficeEmployee)employee).Department)).ToList().Count;
            }
            else
            {
                count = (await _productionEmployeeDatabase.FindAsync<ProductionEmployee>(x => x.Name == employee.Name && x.Surname == employee.Surname && x.Age == employee.Age && x.Shift == ((ProductionEmployee)employee).Shift)).ToList().Count;
            }
            return count > 0;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            var officeEmployees = _officeEmployeeDatabase.FindAsync<OfficeEmployee>(x => x.Id.ToString() != "");
            var productionEmployees = _productionEmployeeDatabase.FindAsync<ProductionEmployee>(x => x.Id.ToString() != "");

            await Task.WhenAll(officeEmployees, productionEmployees);

            employees.AddRange(officeEmployees.Result.ToList());
            employees.AddRange(productionEmployees.Result.ToList());

            return employees;
        }
    }
}
