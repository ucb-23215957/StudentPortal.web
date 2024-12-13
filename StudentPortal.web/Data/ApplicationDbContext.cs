using Microsoft.EntityFrameworkCore;
using StudentPortal.web.Models.Entities;
namespace StudentPortal.web.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
                
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<EnrollmentEntry> EnrollmentEntry { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }

        public DbSet<SubjectEntry> AddingSubjectEntries { get; set; }

        public DbSet<Schedule> Schedule { get; set; }


    }
}
