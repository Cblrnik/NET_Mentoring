using System;

namespace OOP_Fundamentals.Entities
{
    public class LocalizedBook : Document
    {
        public LocalizedBook(int id, string title, string[] authors, DateTime datePublished, string iSBN, int numberOfPages, string originalPublisher, string countryOfLocalization, string localPublisher)
            : base(id, title, authors, datePublished)
        {
            ISBN = iSBN;
            NumberOfPages = numberOfPages;
            OriginalPublisher = originalPublisher;
            CountryOfLocalization = countryOfLocalization;
            LocalPublisher = localPublisher;
        }

        public string ISBN { get; }

        public int NumberOfPages { get; }

        public string OriginalPublisher { get; }

        public string CountryOfLocalization { get; }

        public string LocalPublisher { get; }

        public override string ToString()
        {
            return base.ToString() + $", ISBN: {ISBN}, NumberOfPages: {NumberOfPages}, OriginalPublisher: {OriginalPublisher}, "
                + $"CountryOfLocalization: {CountryOfLocalization}, LocalPublisher: {LocalPublisher}";
        }
    }
}
