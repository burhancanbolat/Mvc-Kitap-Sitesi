using Kitaplar.Data;
using Kitaplar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using X.PagedList;

namespace Kitaplar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        public HomeController(
            ILogger<HomeController>  logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MostViews = await context.Books.OrderByDescending(p => p.Price).Take(15).ToListAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public async Task<IActionResult> Book(int id)
        {
            var book = await context.Books.SingleOrDefaultAsync(p => p.Id == id);


            return View(book);
        }      
        public async Task<IActionResult> Genre(int id,int?page)
        {
            var genre = await context.Genres.SingleOrDefaultAsync(p => p.Id == id);
            ViewBag.Genre = genre;
            var model = genre.Books.ToPagedList(page ?? 1, 15);
            return View(model);

            
        }
        public async Task<IActionResult> Search(string keyword, int? page)
        {
            ViewBag.Keyword = keyword;
            var model = (await context.Books.Where(p => p.Name.Contains(keyword)).ToListAsync()).ToPagedList(page ?? 1, 12);
            return View(model);
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}