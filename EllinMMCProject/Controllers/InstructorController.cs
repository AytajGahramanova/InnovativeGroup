using EllinMMCProject.DAL;
using EllinMMCProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EllinMMCProject.Controllers
{
    public class InstructorController : Controller
    {
        private readonly AppDbContext _db;
        public InstructorController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Trainer> trainers = _db.Trainers.Include(x=>x.TrainerTopics).ToList();
          
            return View(trainers);
        }
    }
}
