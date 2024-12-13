using System.ComponentModel.DataAnnotations;

namespace StudentPortal.web.Models.Entities
{
    public class EnrollmentEntry
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(25)]
        
        public required string Name { get; set; }
        public required string Course { get; set; }
        public int? Year { get; set; }
        public int EDPCode { get; set; }
        public required string SubjectCode { get; set; }
        public required string TimeStart { get; set; }
        public required string TimeEnd { get; set; }
        public required string Days { get; set; }
        public required int Room { get; set; }
        public required string Units { get; set; }
        public required string Encoder { get; set; }
        public required string Date { get; set; }
        public required int TotalUnits { get; set; }


    }

}
