using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required]
    public string Email { get; set; }
}