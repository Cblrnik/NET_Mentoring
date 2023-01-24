using System;

namespace OOP_Fundamentals.Models
{
    public class Patent : Document
    {
        public int UniqueId { get; set; }

        public string Author { get; set; }

        public DateTime ExpirationDate { get; set; }

        public override string ToString()
        {
            return base.ToString() + $", UniqueId: {UniqueId}, Author: {Author}, ExpirationDate: {ExpirationDate}";
        }
    }
}
