using System.Collections.Generic;
using System.Xml.Serialization;

namespace XMLSerialization
{
    public class Department
    {
        [XmlAttribute]
        public string DepartmentName { get; set; }

        [XmlArray]
        public List<Employee> Employees { get; set; }
    }
}
