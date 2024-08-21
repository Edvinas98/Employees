using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Core.Contracts;
using Employees.Core.Repositories;
using Employees.Core.Services;
using Employees.EntryPoint.Services;
using MongoDB.Driver;

namespace Employees.EntryPoint
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IMongoClient mongoClient = new MongoClient("mongodb+srv://EdvinasPocius:st83OA8OyHilnaNS@cluster.pjckb.mongodb.net/?retryWrites=true&w=majority&appName=Cluster");
            IEmployeeDbRepository employeeRepository = new EmployeeDbRepository(mongoClient);
            IEmployeeService employeeService = new EmployeeService(employeeRepository);
            MenuUI menu = new MenuUI(employeeService);
            await menu.LaunchMenu();
        }
    }
}
