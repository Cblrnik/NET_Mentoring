using System;

namespace OOP_Fundamentals.Models
{
    public abstract class Document
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string[] Authors { get; set; }

        public DateTime DatePublished { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Authors: {string.Join(", ", Authors)}, DatePublished: {DatePublished}";
        }
    }
}
