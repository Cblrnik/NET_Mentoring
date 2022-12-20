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
            Deserialize("Department.bin");
        }

        public static void Serialize(Department dep, string fileName)
        {
            var ms = File.OpenWrite(fileName);

            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, dep);
            ms.Flush();
            ms.Close();
            ms.Dispose();
        }

        public static Department Deserialize(string fileName)
        {
            var formatter = new BinaryFormatter();

            var fs = File.Open(fileName, FileMode.Open);

            var obj = formatter.Deserialize(fs);
            var dep = (Department)obj;
            fs.Flush();
            fs.Close();
            fs.Dispose();

            return dep;
        }
    }
}
