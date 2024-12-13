using StudentPortal.web.Models.Entities;

namespace StudentPortal.web.Services
{
    public interface IStudentService
    {
        Task<Student> GetStudentById(int id);
        Task UpdateStudent(Student student);
    }
}
