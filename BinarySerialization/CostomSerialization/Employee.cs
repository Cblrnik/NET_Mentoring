using System;
using System.Runtime.Serialization;

namespace CostomSerialization
{
    [Serializable]
    internal class Employee : ISerializable
    {
        public string Name { get; set; }

        public string DepartmentName { get; set; }

        public Employee() 
        {
            Name = string.Empty;
            DepartmentName = string.Empty;
        }

        protected Employee(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            Name = info.GetString("Name");
            DepartmentName = info.GetString("DepartmentName");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("Name", Name);
            info.AddValue("DepartmentName", DepartmentName);
        }
    }
}
