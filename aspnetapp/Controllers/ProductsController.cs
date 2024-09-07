﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using aspnetapp.Models;

namespace aspnetapp.Controllers
{
    public class ProductsController(ILogger<ProductsController> logger) : Controller
    {
        private readonly ILogger<ProductsController> _logger = logger;

        public IActionResult Index()
        {
            return View(new ProductCollection());
        }

        public IActionResult Insert()
        {
            return View(nameof(Insert), new Product());
        }

        [HttpPost]
        public IActionResult Insert(Product product)
        {
            var collection = new ProductCollection();
            try
            {
                collection.Insert(product);
                ViewBag.Message = $"Successfully added {product.Brand} {product.Model}";
            }
            catch (HttpRequestException exception)
            {
                ViewBag.Message = "ERROR: " + exception.Message;
            }

            return View(product);
        }

        public IActionResult Find()
        {
            return View(nameof(Find), new ProductCollection());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}