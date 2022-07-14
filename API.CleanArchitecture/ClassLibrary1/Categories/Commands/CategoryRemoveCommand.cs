using Clean.Architecture.Domain.Entities;
using MediatR;

namespace Clean.Architecture.Application.Categories.Commands
{
    public abstract class CategoryRemoveCommand : IRequest<Category>
    {
        public int Id { get; set; }

        protected CategoryRemoveCommand(int id)
        {
            Id = id;
        }
    }
}
