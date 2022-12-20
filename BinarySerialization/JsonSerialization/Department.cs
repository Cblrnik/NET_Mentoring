using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JsonSerialization
{
    public class Department
    {
        public string DepartmentName { get; set; }

        [JsonPropertyName("DepEmployees")]
        public List<Employee> Employees { get; set; }
    }
}
