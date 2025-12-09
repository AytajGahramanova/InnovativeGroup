using EllinMMCProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EllinMMCProject.DAL
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options) { }
		public DbSet<Slider> sliders { get; set; }
		public DbSet<Statistic> statistic { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<CourseTopic> CourseTopics { get; set; }
		public DbSet<CourseImage> CourseImages { get; set; }
		public DbSet<CourseVideo> CourseVideos { get; set; }

		public DbSet<Graduate> Graduate { get; set; }

		public DbSet<Academy> Academy { get; set; }
		public DbSet<AcademyTopic> AcademyTopics { get; set; }

		public DbSet<Trainer> Trainers { get; set; }
		public DbSet<TrainerTopic> TrainersTopic { get; set; }

		public DbSet<GalleryCategory> GalleryCategories { get; set; }
		public DbSet<GalleryProduct> GalleryProducts { get; set; }

		public DbSet<Story> Stories { get; set; }

		public DbSet<Appeal> Appeal { get; set; }

	}
}
