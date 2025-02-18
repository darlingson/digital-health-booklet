using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using digital_health_passport_api.Data;
using digital_health_passport_api.Data.Entities;
using digital_health_passport_api.Models.Dtos;

namespace digital_health_passport_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration config, ApplicationDbContext context, ILogger<AuthController> logger)
        {
            _config = config;
            _context = context;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (registerDto == null)
                {
                    return BadRequest("Registration data is required");
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(registerDto.Email) || 
                    string.IsNullOrWhiteSpace(registerDto.Password) ||
                    string.IsNullOrWhiteSpace(registerDto.Name))
                {
                    return BadRequest("Email, password, and name are required fields");
                }

                // Check if email exists
                if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                {
                    return Conflict("Email already exists");
                }

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = registerDto.Email,
                    Name = registerDto.Name,
                    PasswordHash = HashPassword(registerDto.Password),
                    Role = registerDto.Role,
                    ProfessionalId = registerDto.ProfessionalId
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                var token = await GenerateToken(user);
                
                return Ok(new
                {
                    token,
                    user = new
                    {
                        id = user.Id,
                        email = user.Email,
                        name = user.Name,
                        role = user.Role,
                        professionalId = user.ProfessionalId
                    }
                });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred while registering user: {Email}", registerDto.Email);
                return StatusCode(500, "An error occurred while saving the user");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred during registration for user: {Email}", registerDto.Email);
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (loginDto == null)
                {
                    return BadRequest("Login data is required");
                }

                if (string.IsNullOrWhiteSpace(loginDto.Email) || 
                    string.IsNullOrWhiteSpace(loginDto.Password))
                {
                    return BadRequest("Email and password are required");
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
                {
                    return BadRequest("Invalid email or password");
                }

                var token = await GenerateToken(user);
                
                return Ok(new
                {
                    token,
                    user = new
                    {
                        id = user.Id,
                        email = user.Email,
                        name = user.Name,
                        role = user.Role,
                        professionalId = user.ProfessionalId
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login for user: {Email}", loginDto.Email);
                return StatusCode(500, "An error occurred during login");
            }
        }

        private string HashPassword(string password)
        {
            try
            {
                using var sha256 = SHA256.Create();
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while hashing password");
                throw new InvalidOperationException("Error processing password", ex);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            try
            {
                return HashPassword(password) == hash;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while verifying password");
                throw new InvalidOperationException("Error verifying password", ex);
            }
        }

        private async Task<string> GenerateToken(User user)
        {
            try
            {
                var key = _config["Jwt:Key"];
                if (string.IsNullOrEmpty(key))
                {
                    throw new InvalidOperationException("JWT key is not configured");
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating JWT token for user: {UserId}", user.Id);
                throw new InvalidOperationException("Error generating authentication token", ex);
            }
        }
    }
}