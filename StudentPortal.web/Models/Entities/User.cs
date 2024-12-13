using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPortal.web.Models.Entities
{
    [Table("Usr")]
    public class User
    {
        public User() // Parameterless constructor
        {
            // No need to do anything here
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public required string Username { get; set; }

        [Required]
        [StringLength(100)] // Length adjusted to store hashed passwords
        public required string Password { get; set; }

    }
}
