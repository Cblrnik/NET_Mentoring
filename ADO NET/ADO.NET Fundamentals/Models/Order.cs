using ADO.NET_Fundamentals.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.NET_Fundamentals.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int ProductId { get; set; }
    }
}
