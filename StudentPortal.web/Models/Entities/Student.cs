using System.ComponentModel.DataAnnotations;

namespace StudentPortal.web.Models.Entities
{
    public class Student
    {
        [Key]
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
