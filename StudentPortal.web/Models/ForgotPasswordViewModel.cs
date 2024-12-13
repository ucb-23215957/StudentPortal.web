using System.ComponentModel.DataAnnotations;

namespace StudentPortal.web.Models
{
    public class ForgotPasswordViewModel
    {
        
        public required string Username { get; set; }

        [DataType(DataType.Password)]
        public required string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }
    }
}
