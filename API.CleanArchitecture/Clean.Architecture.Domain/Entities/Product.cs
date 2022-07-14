using Clean.Architecture.Domain.Validation;

namespace Clean.Architecture.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        public int CategoryId { get; set; }
        public Category Categorys { get; set; }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidationDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Id Invalid");
            Id = id;
            ValidationDomain(name, description, price, stock, image);
        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidationDomain(name, description, price, stock, image);
            CategoryId = categoryId;
        }

        private void ValidationDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid Name");
            DomainExceptionValidation.When(name.Length < 3, "Invalid Name");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid Description");
            DomainExceptionValidation.When(description.Length < 5, "Invalid Description");
            
            DomainExceptionValidation.When(price < 0, "Invalid Price");
            
            DomainExceptionValidation.When(stock < 0, "Invalid Stock");
            
            DomainExceptionValidation.When(image?.Length > 250, "Invalid Image");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
        }
    }
}
