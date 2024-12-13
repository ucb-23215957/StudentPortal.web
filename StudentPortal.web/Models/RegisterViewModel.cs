using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{

    [MaxLength(25)]
    public required string Username { get; set; }

    [DataType(DataType.Password)]
    public required string Password { get; set; }
    public string ConfirmPassword { get; internal set; }
}
