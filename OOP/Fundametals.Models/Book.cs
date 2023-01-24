namespace OOP_Fundamentals.Models
{
    public class Book : Document
    {
        public string ISBN { get; set; }

        public int NumberOfPages { get; set; }

        public string Publisher { get; set; }

        public override string ToString()
        {
            return base.ToString() + $", ISBN: {ISBN}, NumberOfPages: {NumberOfPages}, Publisher: {Publisher}";
        }
    }
}
