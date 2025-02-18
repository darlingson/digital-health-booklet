namespace digital_health_passport_api.Models.Dtos;

public class RegisterDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Name { get; set; }
    public required string Role { get; set; }
    public string? ProfessionalId { get; set; }
}