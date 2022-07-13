using Clean.Architecture.Application.Categories.Commands;
using Clean.Architecture.Domain.Entities;
using Clean.Architecture.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Architecture.Application.Categories.Handlers
{
    public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand, Category>
    {
        private readonly ICategoryRepository _repository;

        public CategoryUpdateCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Category> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);

            if (category == null)
                throw new ApplicationException($"Error could not be Found.");
            else
            {
                category.Update(request.Name);
                return await _repository.UpdateAsync(category);
            }
        }
    }
}
