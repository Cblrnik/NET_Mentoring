namespace Task1
{
    public class Product
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public double Price { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is Product)
            {
                Product prod = obj as Product;
                return this.Price == prod.Price && this.Name.Equals(prod.Name);
            }

            return false;
        }
    }
}
