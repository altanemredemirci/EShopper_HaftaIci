using BLL.Abstract;
using BLL.Concrete;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEBUI.Models;

namespace WEBUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;

		public HomeController(IProductService productService, IBrandService brandService)
		{
            _productService = productService;
            _brandService = brandService;
		}

		public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync(i=> i.IsPopular);
            return View(products);
        }

        public async Task<IActionResult> Products()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
        
        public async Task<IActionResult> ProductsByBrand(int id)
        {
            var brand = await _brandService.GetProductsByBrandAsync(id);
            return View(brand.Products);
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _productService.GetOneAsync(i => i.Id == id);
            if (product == null)
            {
                return NotFound(); // Return 404 or redirect to another page
            }
            return View(product);
        }
    }
}
