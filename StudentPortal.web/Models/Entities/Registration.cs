using System.ComponentModel.DataAnnotations;

namespace StudentPortal.web.Models.Entities
{
    public class Registration
    {
        public Registration() // Parameterless constructor
        {
            // No need to do anything here
        }
        public int Id { get; set; }
         [MaxLength(25)]
        public required string Username { get; set; }

        [MaxLength(100)]
        public required string Password { get; set; }
        [MaxLength(100)]

        public required string ConfirmPassword { get; set; }
    }
}
