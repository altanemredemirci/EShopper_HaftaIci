using BLL.Abstract;
using BLL.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WEBUI.ViewComponents.Home
{
	public class _SelectCategoryViewComponentPartial : ViewComponent
	{
		private readonly ICategoryService _categoryService;

		public _SelectCategoryViewComponentPartial(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var models = await _categoryService.GetCategories();
			return View(models);
		}
	}
}
