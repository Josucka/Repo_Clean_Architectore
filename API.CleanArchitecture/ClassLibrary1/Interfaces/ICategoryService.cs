using Clean.Architecture.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean.Architecture.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetById(int? id);
        Task Add(CategoryDTO categoryDTO);
        Task Update(CategoryDTO categoryDTO);
        Task Remove(int? id);
    }
}
