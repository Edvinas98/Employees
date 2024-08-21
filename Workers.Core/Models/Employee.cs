using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Employees.Core.Contracts
{
    public class Employee
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte Age { get; set; }

        public Employee(string name, string surname, byte age)
        {
            Name = name;
            Surname = surname;
            Age = age;
        }

        public Employee()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Age = 0;
        }

        public override string ToString()
        {
            return $"{Name} {Surname}".PadRight(30) + $"Age: {Age} years";
        }
    }
}
