using ePizzaHub.Services.Interfaces;
using ePizzaHub.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace ePizzaHub.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IItemService _itemService;
        IMemoryCache _memoryCache;
        public HomeController(ILogger<HomeController> logger, IItemService service, IMemoryCache memoryCache)
        {
            _itemService = service;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            string cacheKey = "catalog";
            var items = _memoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(12);
                entry.SlidingExpiration = TimeSpan.FromMinutes(15);
                return _itemService.GetItems();
            });

            //var items = _itemService.GetItems();

            try
            {
                int x = 2, y = 0;
                int z = x / y;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}