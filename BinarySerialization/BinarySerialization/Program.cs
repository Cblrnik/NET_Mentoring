using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinarySerialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var emp1 = new Employee
            {
                EmpoyeeName = "Roman"
            };

            var emp2 = new Employee
            {
                EmpoyeeName = "Some Name"
            };

            var dep = new Department()
            {
                DepartmentName = "Some Dep",
                Employees = new List<Employee>()
                {
                    emp1,
                    emp2
                }
            };

            Serialize(dep, "Department.bin");
            var newDepatrment = Deserialize("Department.bin");

            var depClone = newDepatrment.Clone();

            Console.WriteLine(object.ReferenceEquals(depClone, newDepatrment));
        }

        public static void Serialize(Department dep, string fileName)
        {
            using var ms = File.OpenWrite(fileName);

            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, dep);
        }

        public static Department Deserialize(string fileName)
        {
            var formatter = new BinaryFormatter();

            using var fs = File.Open(fileName, FileMode.Open);

            var obj = formatter.Deserialize(fs);
            var dep = (Department)obj;

            return dep;
        }
    }
}
