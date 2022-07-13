using Clean.Architecture.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean.Architecture.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetById(int? id);
        Task Add(ProductDTO productDTO);
        Task Update(ProductDTO productDTO);
        Task Remove(int? id);
    }
}
