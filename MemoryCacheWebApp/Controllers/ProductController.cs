using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoryCacheWebApp.Context;
using MemoryCacheWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MemoryCacheWebApp.Controllers
{
    public class ProductController : Controller
    {
        IMemoryCache _memoryCache;
        MemoryCacheContext _memoryCacheContext;
        public ProductController(IMemoryCache memoryCache, MemoryCacheContext memoryCacheContext)
        {
            _memoryCacheContext = memoryCacheContext;
            _memoryCache = memoryCache;
        }
        List<Products> products;
        public ActionResult Index()
        {

            if (_memoryCache.Get<List<Products>>("products") != null)
            {
                products = _memoryCache.Get<List<Products>>("products");
            }
            else
            {
                products = _memoryCacheContext.Products.Select(x => new Products()
                {
                    ProductID = x.ProductID,
                    ProductNumber = x.ProductNumber,
                    Name = x.Name
                }).OrderByDescending(x=> x.ProductID).Take(10).ToList();
                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1),
                    Priority = CacheItemPriority.High,
                };
                _memoryCache.Set<List<Products>>("products", products, options);
            }
            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Products product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _memoryCacheContext.Products.Add(product);
            _memoryCacheContext.SaveChanges();
            _memoryCache.Remove("products");
            return RedirectToAction("Index", "Product");
        }
    }
}