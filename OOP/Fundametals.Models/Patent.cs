using System;

namespace OOP_Fundamentals.Entities
{
    public class Patent : Document
    {
        public Patent(int id, string title, string[] authors, DateTime datePublished, int uniqueId, string author, DateTime expirationDate)
            : base(id, title, authors, datePublished)
        {
            UniqueId = uniqueId;
            Author = author;
            ExpirationDate = expirationDate;
        }

        public int UniqueId { get;  }

        public string Author { get; }

        public DateTime ExpirationDate { get; }

        public override string ToString()
        {
            return base.ToString() + $", UniqueId: {UniqueId}, Author: {Author}, ExpirationDate: {ExpirationDate}";
        }
    }
}
