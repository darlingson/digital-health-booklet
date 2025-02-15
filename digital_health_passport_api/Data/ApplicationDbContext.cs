using Microsoft.EntityFrameworkCore;

namespace digital_health_passport_api.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}