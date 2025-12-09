using EllinMMCProject.DAL;
using EllinMMCProject.Models;
using EllinMMCProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EllinMMCProject.Controllers
{
    public class GaleryController : Controller
    {
        private readonly AppDbContext _db;
        public GaleryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
           

            GalleryVM galleryVM = new GalleryVM()
            {
                GalleryCategories = _db.GalleryCategories.ToList(),
                GalleryProducts = _db.GalleryProducts.ToList(),
                Stories = _db.Stories.ToList(),
            };


            return View(galleryVM);
        }
    }
}
