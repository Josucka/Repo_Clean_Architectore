using AutoMapper;
using Clean.Architecture.Application.DTOs;
using Clean.Architecture.Application.Interfaces;
using Clean.Architecture.Domain.Entities;
using Clean.Architecture.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean.Architecture.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Add(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.CreateAsync(product);
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> GetProductCategory(int? id)
        {
            var product = await _productRepository.GetProductCategoryAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var product = await _productRepository.GetProductAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(product);
        }

        public async Task Remove(int? id)
        {
            var product = _productRepository.GetByIdAsync(id).Result;
            await _productRepository.RemoveAsync(product);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productRepository.UpdateAsync(product);
        }
    }
}
