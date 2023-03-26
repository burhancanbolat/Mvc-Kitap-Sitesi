using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Kitaplar.Areas.Admin.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Kitaplar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : Controller
    { 
    private readonly ApplicationDbContext context;

    public DashboardController(
        ApplicationDbContext context
        )
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var model = new DashboardViewModel
        {
            Genres = context.Genres.Count(),
            Books = context.Books.Count()
        };
        return View(model);
    }

   
}
}

