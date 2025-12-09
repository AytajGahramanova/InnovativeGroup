namespace EllinMMCProject.Models
{
    public class AcademyTopic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AcademyId { get; set; }
        public Academy Academy { get; set; }
    }
}
