using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JsonSerialization
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

            Serialize(dep, "Deparment.json");
            var newDep = Deserialize("Deparment.json");
        }

        public static void Serialize(Department dep, string fileName)
        {
            var jsonString = JsonSerializer.Serialize(dep);
            File.WriteAllText(fileName, jsonString);
        }

        public static Department Deserialize(string fileName)
        {
            var jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<Department>(jsonString);
        }
    }
}
