using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XMLSerialization
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

            Serialize(dep, "Deparment.xml");
            var newDep = Deserialize("Deparment.xml");
        }

        public static void Serialize(Department dep, string fileName)
        {
            var serializer = new XmlSerializer(typeof(Department));
            using var writer = new StreamWriter(fileName);
            serializer.Serialize(writer, dep);
        }

        public static Department Deserialize(string fileName)
        {
            var serializer = new XmlSerializer(typeof(Department));


            using var writer = new StreamReader(fileName);
            var depObj = serializer.Deserialize(writer);

            return (Department)depObj;
        }
    }
}
