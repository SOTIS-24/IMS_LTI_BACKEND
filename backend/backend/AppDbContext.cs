using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
           .HasMany(t => t.Tests)
           .WithOne()
           .HasForeignKey(t => t.CourseId);

            modelBuilder.Entity<Test>()
                .HasMany(t => t.Questions)
                .WithOne()
                .HasForeignKey(q => q.TestId);

            modelBuilder.Entity<Question>()
                   .HasMany(t => t.Answers)
                   .WithOne()
                   .HasForeignKey(a => a.QuestionId);
        }
    }
}
