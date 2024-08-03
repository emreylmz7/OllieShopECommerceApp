﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OllieShop.DtoLayer.CatalogDtos.ProductStock;
using OllieShop.WebUI.Services.CatalogServices.ColorServices;
using OllieShop.WebUI.Services.CatalogServices.ProductServices;
using OllieShop.WebUI.Services.CatalogServices.ProductStockServices;
using OllieShop.WebUI.Services.CatalogServices.SizeServices;

namespace OllieShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductStock")]
    public class ProductStockController : Controller
    {
        private readonly IProductStockService _productStockService;
        private readonly IProductService _productService;
        private readonly IColorService _colorService;
        private readonly ISizeService _sizeService;

        public ProductStockController(IProductStockService productStockService, IProductService productService, IColorService colorService, ISizeService sizeService)
        {
            _productStockService = productStockService;
            _productService = productService;
            _colorService = colorService;
            _sizeService = sizeService;
        }

        [Route("Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productStocks = await _productStockService.GetProductStocksWithDetails();
            return View(productStocks);
        }

        [Route("CreateProductStock")]
        [HttpGet]
        public async Task<IActionResult> CreateProductStock()
        {
            await PopulateViewDataForProductStock();
            return View();
        }

        [HttpPost]
        [Route("CreateProductStock")]
        public async Task<IActionResult> CreateProductStock(CreateProductStockDto createProductStockDto)
        {
            if (ModelState.IsValid)
            {
                await _productStockService.CreateProductStockAsync(createProductStockDto);
                return RedirectToAction("Index");
            }

            await PopulateViewDataForProductStock();
            return View(createProductStockDto);
        }

        [Route("DeleteProductStock/{id}")]
        [HttpGet]
        public async Task<IActionResult> DeleteProductStock(string id)
        {
            await _productStockService.DeleteProductStockAsync(id);
            return RedirectToAction("Index");
        }

        [Route("UpdateProductStock/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProductStock(string id)
        {
            await PopulateViewDataForProductStock();

            var productStock = await _productStockService.GetByIdProductStockAsync(id);
            if (productStock == null)
            {
                return NotFound();
            }

            var productStockDto = new UpdateProductStockDto
            {
                ProductStockId = productStock.ProductStockId,
                ProductId = productStock.ProductId,
                SizeId = productStock.SizeId,
                ColorId = productStock.ColorId,
                Stock = productStock.Stock,
            };
            return View(productStockDto);
        }

        [HttpPost]
        [Route("UpdateProductStock/{id}")]
        public async Task<IActionResult> UpdateProductStock(UpdateProductStockDto updateProductStockDto)
        {
            if (ModelState.IsValid)
            {
                await _productStockService.UpdateProductStockAsync(updateProductStockDto);
                return RedirectToAction("Index");
            }

            await PopulateViewDataForProductStock();
            return View(updateProductStockDto);
        }

        private async Task PopulateViewDataForProductStock()
        {
            var sizes = await _sizeService.GetAllSizeAsync();
            ViewBag.Sizes = sizes.Select(size => new SelectListItem
            {
                Text = size.Name,
                Value = size.SizeId
            }).ToList();

            var colors = await _colorService.GetAllColorAsync();
            ViewBag.Colors = colors.Select(color => new SelectListItem
            {
                Text = color.Name,
                Value = color.ColorId
            }).ToList();

            var products = await _productService.GetAllProductAsync();
            ViewBag.Products = products.Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId
            }).ToList();
        }
    }
}
