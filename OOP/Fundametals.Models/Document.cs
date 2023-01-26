using System;

namespace OOP_Fundamentals.Entities
{
    public abstract class Document
    {
        public Document(int id, string title, string[] authors, DateTime datePublished)
        {
            Id = id;
            Title = title;
            Authors = authors;
            DatePublished = datePublished;
        }

        public int Id { get; }

        public string Title { get; }

        public string[] Authors { get;}

        public DateTime DatePublished { get; }

        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Authors: {string.Join(", ", Authors)}, DatePublished: {DatePublished}";
        }
    }
}
