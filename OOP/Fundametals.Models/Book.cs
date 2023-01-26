using System;
using System.Text.Json.Serialization;

namespace OOP_Fundamentals.Entities
{
    public class Book : Document
    {
        public Book(int id, string title, string[] authors, DateTime datePublished, string iSBN, int numberOfPages, string publisher)
            : base( id, title, authors, datePublished)
        {
            ISBN = iSBN;
            NumberOfPages = numberOfPages;
            Publisher = publisher;
        }

        public string ISBN { get; }

        public int NumberOfPages { get; }

        public string Publisher { get; }

        public override string ToString()
        {
            return base.ToString() + $", ISBN: {ISBN}, NumberOfPages: {NumberOfPages}, Publisher: {Publisher}";
        }
    }
}
