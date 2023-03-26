using Kitaplar;
using Microsoft.AspNetCore.Mvc;

namespace Kitaplar.Components
{
    public class GenreMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext context;

        public GenreMenuViewComponent(
            ApplicationDbContext context
            )
        {
            this.context = context;
        }

        public IViewComponentResult Invoke()
        {
            var model = context.Genres.ToList();
            return View(model);
        }
    }
}
