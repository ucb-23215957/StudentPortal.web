using System.ComponentModel.DataAnnotations;

namespace StudentPortal.web.Models.Entities
{
    public class SubjectEntry
    {
        [Key]
        public int Id { get; set; }
        public required string Subject { get; set; }
        [MaxLength(50)] 

        public required string Description { get; set; }
        [MaxLength(50)]

        public required string Units { get; set; }
        public required string Offering { get; set; }
        public required string Category { get; set; }
        public required string CourseCode { get; set; }
        public required string CurriculumYear { get; set; }

        public required string SubjectCode { get; set; }
        public required string PreCorReq { get; set; }
    }
}
