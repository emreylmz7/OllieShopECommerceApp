﻿using Microsoft.AspNetCore.Mvc;
using OllieShop.DtoLayer.CatalogDtos.Category;
using OllieShop.WebUI.Services.ApiServices;
using OllieShop.WebUI.Services.CatalogServices.CategoryServices;
using OllieShop.WebUI.Services.TokenServices;

namespace OllieShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public _NavbarUILayoutComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            return View(categories);
        }
    }
}
