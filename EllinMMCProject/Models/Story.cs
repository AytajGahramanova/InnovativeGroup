using System.ComponentModel.DataAnnotations.Schema;

namespace EllinMMCProject.Models
{
    public class Story
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Positions { get; set; }
        public string Company { get; set; }
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? formFile { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
