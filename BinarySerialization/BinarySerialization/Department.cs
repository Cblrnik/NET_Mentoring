using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinarySerialization
{
    [Serializable]
    public class Department
    {
        public string DepartmentName { get; set; }
        public List<Employee> Employees { get; set; }

        public Department Clone()
        {
            using var ms = new MemoryStream();

            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, this);

            ms.Position = 0;

            var obj = formatter.Deserialize(ms);
            return (Department)obj;
        }
    }
}
