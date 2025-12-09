using System.ComponentModel.DataAnnotations.Schema;

namespace EllinMMCProject.Models
{
    public class TrainerTopic
    {
        public int Id { get; set; }
        public string Title { get; set; }

       
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
    }
}
