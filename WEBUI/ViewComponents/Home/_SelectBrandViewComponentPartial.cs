using BLL.Abstract;
using BLL.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WEBUI.ViewComponents.Home
{
	public class _SelectBrandViewComponentPartial:ViewComponent
	{
		private readonly IBrandService _brandService;

        public _SelectBrandViewComponentPartial(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _brandService.GetAllAsync());
        }
    }
}
