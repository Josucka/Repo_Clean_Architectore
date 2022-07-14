using AutoMapper;
using Clean.Architecture.Application.DTOs;
using Clean.Architecture.Application.Interfaces;
using Clean.Architecture.Domain.Entities;
using Clean.Architecture.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean.Architecture.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task Add(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.CreateAsync(category);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            var categorie = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(categorie);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task Remove(int? id)
        {
            var category = _categoryRepository.GetByIdAsync(id).Result;
            await _categoryRepository.RemoveAsync(category);
        }

        public async Task Update(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
