using Clean.Architecture.Domain.Entities;
using MediatR;

namespace Clean.Architecture.Application.Products.Commands
{
    public class ProductRemoveCommmand : IRequest<Product>
    {
        public int Id { get; set; }

        public ProductRemoveCommmand(int id)
        {
            Id = id;
        }
    }
}
