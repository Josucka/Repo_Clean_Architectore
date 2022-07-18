using Clean.Architecture.Application.DTOs;
using Clean.Architecture.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean.Architecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _category;

        public CategoriesController(ICategoryService category)
        {
            _category = category;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var category = await _category.GetCategories();
            if (category == null)
                return NotFound("Category not found.");
            return Ok(category);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _category.GetById(id);
            if (category == null)
                return NotFound("Category not found.");
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
                return BadRequest("Invalid Data");
            await _category.Add(categoryDTO);

            return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.Id }, categoryDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
                return BadRequest();
            if (categoryDTO == null)
                return BadRequest();

            await _category.Update(categoryDTO);
            return Ok(categoryDTO);
        }

        [HttpDelete("id:int")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await _category.GetById(id);
            if (category == null)
                return NotFound("Category not found.");

            await _category.Remove(id);
            return Ok(category);
        }
    }
}
