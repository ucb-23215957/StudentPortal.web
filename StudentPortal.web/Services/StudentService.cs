using StudentPortal.web.Models.Entities;
using StudentPortal.web.Data; // Assuming your ApplicationDbContext is in Data folder
using Microsoft.EntityFrameworkCore;

namespace StudentPortal.web.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _db;

        public StudentService(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _db.Students.FindAsync(id);
        }

        public async Task UpdateStudent(Student student)
        {
            _db.Students.Update(student);
            await _db.SaveChangesAsync();
        }
    }
}
