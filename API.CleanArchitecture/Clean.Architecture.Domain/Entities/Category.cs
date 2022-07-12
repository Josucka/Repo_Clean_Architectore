using Clean.Architecture.Domain.Validation;
using System.Collections.Generic;

namespace Clean.Architecture.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; }

        public Category(string name)
        {
            ValidationDomain(name);
        }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When( id < 0, $"Id Invalid");
            Id = id;
            ValidationDomain(name);
        }

        public void Update(string name)
        {
            ValidationDomain(name);
        }

        public ICollection<Product> Products { get; set; }

        private void ValidationDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), $" Invalid {name}");

            DomainExceptionValidation.When(name.Length < 3, $" Invalid {name}");

            Name = name;
        }
    }
}
