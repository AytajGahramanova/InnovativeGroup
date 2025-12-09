namespace EllinMMCProject.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Positions { get; set; }
        public string Sitat {  get; set; }

        public List<TrainerTopic> TrainerTopics { get; set; } = new List<TrainerTopic>();


    }
}
