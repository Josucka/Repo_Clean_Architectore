using Clean.Architecture.Domain.Entities;
using MediatR;

namespace Clean.Architecture.Application.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public int Id { get; set; }

        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
