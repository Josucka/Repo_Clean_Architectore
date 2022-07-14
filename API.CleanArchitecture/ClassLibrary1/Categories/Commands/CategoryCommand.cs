using Clean.Architecture.Domain.Entities;
using MediatR;

namespace Clean.Architecture.Application.Categories.Commands
{
    public abstract class CategoryCommand : IRequest<Category>
    {
        public string Name { get; set; }
    }
}
