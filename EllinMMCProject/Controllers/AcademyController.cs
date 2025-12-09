using EllinMMCProject.DAL;
using EllinMMCProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EllinMMCProject.Controllers
{
    public class AcademyController : Controller
    {
        private readonly AppDbContext _db;
        public AcademyController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
          
            List<Academy> academies = _db.Academy.Include(x=>x.academyTopics).ToList();
            return View(academies);
        }
    }
}
