using EllinMMCProject.DAL;
using EllinMMCProject.Models;
using EllinMMCProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EllinMMCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
           _db = db; 
        }


        public IActionResult Index()
        {

            HomeVM homeVM = new HomeVM
            {
                sliders = _db.sliders.ToList(),
                courses = _db.Courses.ToList(),
                stations = _db.statistic.ToList(),
                graduate = _db.Graduate.ToList(),
                
            };
            
            return View(homeVM);
        }

        // Kurs detalları
       
        public IActionResult Detail(int id)
        {
            var course = _db.Courses
                .Include(c => c.Topics)
                .Include(c => c.Images)
                .Include(c => c.Videos)
                .FirstOrDefault(c => c.Id == id);

            if (course == null) return NotFound();

            return View(course);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
