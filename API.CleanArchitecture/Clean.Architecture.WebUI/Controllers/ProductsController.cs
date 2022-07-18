using Clean.Architecture.Application.DTOs;
using Clean.Architecture.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;

namespace Clean.Architecture.WebUI.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _envarionment;

        public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment envarionment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _envarionment = envarionment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var product = await _productService.GetProducts();
            return View(product);
        }

        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.Add(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _productService.GetById(id);

            if (productDto == null)
                return NotFound();

            var categories = await _categoryService.GetCategories();
            ViewBag.ProductId = new SelectList(categories, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(productDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(productDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _productService.GetById(id);
            if (productDto == null)
                return NotFound();

            return View(productDto);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Remove(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var productDto = await _productService.GetById(id);
            if (productDto == null)
                return NotFound();

            var wwwroot = _envarionment.WebRootPath;
            var image = Path.Combine(wwwroot, "images\\" + productDto.Image);
            var exist = System.IO.File.Exists(image);
            ViewBag.ImageExist = exist;

            return View(productDto);

        }
    }
}
