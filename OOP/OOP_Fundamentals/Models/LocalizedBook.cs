using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Fundamentals.Models
{
    public class LocalizedBook
    {
        public int Id { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string[] Authors { get; set; }

        public int NumberOfPages { get; set; }

        public string OriginalPublisher { get; set; }

        public string CountryOfLocalization { get; set; }

        public string LocalPublisher { get; set; }

        public DateTime DatePublished { get; set; }
    }
}
