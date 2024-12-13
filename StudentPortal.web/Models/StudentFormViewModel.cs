using Microsoft.AspNetCore.Mvc;
using StudentPortal.web.Models.Entities;

namespace StudentPortal.web.Models
{
    public class StudentFormViewModel
    {
        // This will hold the data for a new student
        public required CreateStudentViewModel NewStudent { get; set; }

        // This will hold the list of existing students
        public required List<Student> Students { get; set; }
    }
}
