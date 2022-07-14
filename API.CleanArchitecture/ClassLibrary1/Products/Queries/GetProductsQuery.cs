using Clean.Architecture.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Clean.Architecture.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
