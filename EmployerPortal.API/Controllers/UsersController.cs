using EmployerPortal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployerPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EmployerPortalDbContext _context;
        private readonly ILogger<UsersController> _logger;


        public UsersController(EmployerPortalDbContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                _logger.LogWarning("Login attempt with empty username.");
                return BadRequest(new { message = "Username is required." });
            }

            try
            {
                var user = await _context.Users
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync(u => u.Username == username);

                if (user == null)
                    return NotFound(new { message = "Invalid username." });

                return Ok(new { message = $"Welcome {user.Username}" });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during login.");
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
    }
}
