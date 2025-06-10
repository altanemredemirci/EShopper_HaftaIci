using BLL.Abstract;
using BLL.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WEBUI.ViewComponents.Home
{
	public class _SelectCategoryBrandViewComponentPartial:ViewComponent
	{
		private readonly ICategoryService _categoryService;

		public _SelectCategoryBrandViewComponentPartial(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var models = await _categoryService.GetAllAsync();
			return View(models);
		}
	}
}
