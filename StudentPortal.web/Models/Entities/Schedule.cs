using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPortal.web.Models.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        public required string SubjectEDPCode { get; set; }
        public required string SubjectCode { get; set; }
        public required string Description { get; set; }

        public required string TimeStart { get; set; }
        public required string TimeEnd { get; set; }
        public required string Days { get; set; }
        public required string Section { get; set; }
        public required string Room { get; set; }
        public required string Year { get; set; }
        public required string AmPm { get; set; }
        [NotMapped]
        public object SubjectEntry { get; internal set; }
    }
}
