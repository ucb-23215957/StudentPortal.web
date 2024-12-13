using System.ComponentModel.DataAnnotations;
using StudentPortal.web.Models.Entities;

namespace StudentPortal.web.Models
{
    public class CreateStudentViewModel
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public required string Firstname { get; set; } 
        [MaxLength(25)]
        public required string Lastname { get; set; } 
        public required string Middlename { get; set; }

        public required string Course { get; set; }
        public int? Year { get; set; }
    }
}
