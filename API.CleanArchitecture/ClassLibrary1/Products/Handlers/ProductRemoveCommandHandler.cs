using Clean.Architecture.Application.Products.Commands;
using Clean.Architecture.Domain.Entities;
using Clean.Architecture.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Architecture.Application.Products.Handlers
{
    public class CategoryRemoveCommandHandler : IRequestHandler<ProductRemoveCommmand, Product>
    {
        private readonly IProductRepository _repository;

        public CategoryRemoveCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> Handle(ProductRemoveCommmand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
                throw new ApplicationException($"Error could not be Found.");
            else
            {
                var result = await _repository.RemoveAsync(product);
                return result;
            }
        }
    }
}
