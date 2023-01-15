using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Fundamentals.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string[] Authors { get; set; }

        public int NumberOfPages { get; set; }

        public string Publisher { get; set; }

        public DateTime DatePublished { get; set; }
    }
}
