using System.ComponentModel.DataAnnotations;

namespace digital_health_passport_api.Models;

public class PatientModel
{
    public int Id { get; set; }
    
    [Required]
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;
    
    [Required]
    public DateOnly DateOfBirth { get; set; }
    
    [Required]
    public string NationalId { get; set; } = null!;
    
    [Required]
    public long PhoneNumber { get; set; }
}