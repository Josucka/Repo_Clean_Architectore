using Clean.Architecture.Application.Categories.Commands;
using Clean.Architecture.Domain.Entities;
using Clean.Architecture.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Architecture.Application.Categories.Handlers
{
    public class CategoryCreateCommandHandler : IRequestHandler<CategoryUpdateCommand, Category>
    {
        private readonly ICategoryRepository _repository;

        public CategoryCreateCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Category> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.Name);
            if (category == null)
                throw new ApplicationException($"Error creating entity");
            else
            {
                return await _repository.CreateAsync(category);
            }
        }
    }
}
