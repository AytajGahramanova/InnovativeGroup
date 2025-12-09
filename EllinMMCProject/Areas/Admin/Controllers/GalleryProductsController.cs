using EllinMMCProject.DAL;
using EllinMMCProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EllinMMCProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryProductsController : Controller
    {
        public readonly IWebHostEnvironment _webHostEnvironment;
        public readonly AppDbContext _db;
        public GalleryProductsController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<GalleryProduct> galleryProducts = _db.GalleryProducts.Include(x=>x.GalleryCategories).ToList();
            return View(galleryProducts);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _db.GalleryCategories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GalleryProduct galleryProduct, int[] selectOption)
        {
            if(!ModelState.IsValid)
            {
                galleryProduct.GalleryCategories = new List<GalleryCategory>();

                foreach (var item in selectOption)
                {
                    if(item !=  null)
                    {
                        var findCategory = _db.GalleryCategories.Find(item);

                        if (findCategory != null)
                        {
                            galleryProduct.GalleryCategories.Add(findCategory);
                        }
                    }
                }

                if (galleryProduct.formFile != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + galleryProduct.formFile.FileName;

                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await galleryProduct.formFile.CopyToAsync(stream);
                    }

                    galleryProduct.Image = "/uploads/" + uniqueFileName;
                }
                _db.GalleryProducts.Add(galleryProduct);
                _db.SaveChanges();
                return RedirectToAction("Index", "GalleryProducts");
            }
            return View(galleryProduct);
        }

        [HttpGet]
        public IActionResult Read(int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            GalleryProduct galleryProduct = _db.GalleryProducts.Include(x=>x.GalleryCategories).FirstOrDefault(c => c.Id == id);
            if (galleryProduct == null)
            {
                return NotFound();
            }
            return View(galleryProduct);
        }
    }
}
