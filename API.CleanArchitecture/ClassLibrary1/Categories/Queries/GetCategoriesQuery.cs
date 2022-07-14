using Clean.Architecture.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Clean.Architecture.Application.Categories.Queries
{
    public class GetCategoriesQuery : IRequest<IEnumerable<Category>>
    {
    }
}
