using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CostomSerialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var emp1 = new Employee
            {
                Name = "Roman",
                DepartmentName = "Some Dep Name"
            };

            Serialize(emp1, "Department.bin");
            Deserialize("Department.bin");
        }

        public static void Serialize(Employee dep, string fileName)
        {
            var ms = File.OpenWrite(fileName);

            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, dep);
            ms.Flush();
            ms.Close();
            ms.Dispose();
        }

        public static Employee Deserialize(string fileName)
        {
            var formatter = new BinaryFormatter();

            var fs = File.Open(fileName, FileMode.Open);

            var obj = formatter.Deserialize(fs);
            var dep = (Employee)obj;
            fs.Flush();
            fs.Close();
            fs.Dispose();

            return dep;
        }
    }
}
