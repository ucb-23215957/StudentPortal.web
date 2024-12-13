using System.ComponentModel.DataAnnotations;

namespace StudentPortal.web.Models.Entities
{
    public class Enrollment
    {
        [Key]
        public required string EDPCode { get; set; }
        public required string SubjectCode { get; set; }
        public required string StartTime { get; set; }
        public  required string EndTime { get; set; }
        public required string Days { get; set; }
        public required string Room { get; set; }
        public int Units { get; set; }
    }
}
