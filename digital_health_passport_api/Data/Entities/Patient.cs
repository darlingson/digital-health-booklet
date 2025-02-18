using System.ComponentModel.DataAnnotations;
namespace digital_health_passport_api.Data.Entities;

public class Patient
{
    [Key]
    public int Id { get; set; }
    public required string  FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required string NationalId { get; set; }
    public required long PhoneNumber { get; set; }
}