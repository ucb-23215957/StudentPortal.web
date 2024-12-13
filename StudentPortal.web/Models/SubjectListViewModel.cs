namespace StudentPortal.web.Models
{
    public class SubjectListViewModel
    {
        public required SubjectEntryViewModel NewSubject { get; set; } // For the form
        public required IEnumerable<SubjectEntryViewModel> Subjects { get; set; } 
    }
}
