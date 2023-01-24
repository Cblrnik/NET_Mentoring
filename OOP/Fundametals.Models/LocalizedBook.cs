namespace OOP_Fundamentals.Models
{
    public class LocalizedBook : Document
    {
        public string ISBN { get; set; }

        public int NumberOfPages { get; set; }

        public string OriginalPublisher { get; set; }

        public string CountryOfLocalization { get; set; }

        public string LocalPublisher { get; set; }

        public override string ToString()
        {
            return base.ToString() + $", ISBN: {ISBN}, NumberOfPages: {NumberOfPages}, OriginalPublisher: {OriginalPublisher}, "
                + $"CountryOfLocalization: {CountryOfLocalization}, LocalPublisher: {LocalPublisher}";
        }
    }
}
