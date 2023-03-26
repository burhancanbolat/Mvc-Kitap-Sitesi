using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using Kitaplar.Data;
using X.PagedList;

namespace Kitaplar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;
        public BooksController(
            ApplicationDbContext context
            )
        {
            this.context = context;
        }
        public IActionResult Index(int? page)
        {
            var model = context.Books.OrderBy(p => p.Name).ToPagedList(page ?? 1, 10);
            return View(model);
        }
        
       public async Task<IActionResult> Create()
        {
            ViewBag.Genres = new SelectList(await context.Genres.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");
            return View();
        }
         
        [HttpPost]

        public async Task<IActionResult> Create(Book model)
        {
            if (model.ImageFile is not null)
            {
                using var image = await Image.LoadAsync(model.ImageFile.OpenReadStream());
                //using var ms = new MemoryStream();
                //BlobClient blobClient = new BlobClient("sdvsv", "Container1", "Films");

                image.Mutate(p => p.Resize(new ResizeOptions
                {
                    Size = new Size(500, 740),
                    Mode = ResizeMode.Crop
                }));
                //image.SaveAsJpeg(ms);
                //var response = await blobClient.UploadAsync(ms);
                //model.Image = response.Value.BlobSequenceNumber.ToString();
                model.Image = image.ToBase64String(JpegFormat.Instance);

            }

            context.Books.Add(model);
            context.SaveChanges();
            TempData["success"] = "Kitap ekleme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Genres = new SelectList(await context.Genres.OrderBy(p => p.Name).ToListAsync(), "Id", "Name");
            var model = context.Books.Find(id);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Book model)
        {
            if (model.ImageFile is not null)
            {
                using var image = await Image.LoadAsync(model.ImageFile.OpenReadStream());
              

                image.Mutate(p => p.Resize(new ResizeOptions
                {
                    Size = new Size(500, 740),
                    Mode = ResizeMode.Crop
                }));
              
                model.Image = image.ToBase64String(JpegFormat.Instance);

            }
            context.Books.Update(model);
            context.SaveChanges();
            TempData["success"] = "Kitap güncelleme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await context.Books.FindAsync(id);
            context.Books.Remove(model);
            context.SaveChanges();
            TempData["success"] = "Kitap silme işlemi başarıyla tamamlanmıştır";
            return RedirectToAction(nameof(Index));
            
                     
        }
    }
}
       
               
            
       
            

