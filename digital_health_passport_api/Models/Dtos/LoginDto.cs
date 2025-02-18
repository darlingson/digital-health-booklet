namespace digital_health_passport_api.Models.Dtos;

public class LoginDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}