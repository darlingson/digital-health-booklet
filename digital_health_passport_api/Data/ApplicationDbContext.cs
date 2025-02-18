using digital_health_passport_api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace digital_health_passport_api.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    
    }
    public DbSet<Patient> Patients { get; set; }
}