using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace digital_health_passport_api.Data.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; }  // "patient" or "professional"
        public string? ProfessionalId { get; set; }
        
        // Navigation property
        public int? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
    }
}