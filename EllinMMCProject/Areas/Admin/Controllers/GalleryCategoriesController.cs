using EllinMMCProject.DAL;
using EllinMMCProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EllinMMCProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryCategoriesController : Controller
    {
        private readonly AppDbContext _db;

        public GalleryCategoriesController(AppDbContext db)
        {
         _db = db;   
        }
        public IActionResult Index()
        {
            List<GalleryCategory> categories = _db.GalleryCategories.ToList();
            return View(categories);
        }
    }
}
