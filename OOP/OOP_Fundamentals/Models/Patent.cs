using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Fundamentals.Models
{
    public class Patent
    {
        public int Id { get; set; }

        public int UniqueId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime DatePublished { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
