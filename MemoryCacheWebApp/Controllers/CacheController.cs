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
    public class CacheController : Controller
    {
        IMemoryCache _memoryCache;
        MemoryCacheContext _memoryCacheContext;
        public CacheController(IMemoryCache memoryCache, MemoryCacheContext memoryCacheContext)
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
                }).ToList();
                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1),
                    Priority = CacheItemPriority.High,
                };
                _memoryCache.Set<List<Products>>("products", products, options);
            }
            return View(products);
        }
    }
}