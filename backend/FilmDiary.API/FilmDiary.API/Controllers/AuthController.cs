using FilmDiary.API.Common;
using FilmDiary.API.Data;
using FilmDiary.API.DTOs;
using FilmDiary.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace FilmDiary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<object>.ErrorResponse("Geçersiz veri."));

            var exists = await _context.Users
                .AnyAsync(u => u.UserName == dto.UserName);

            if (exists)
                return BadRequest(ApiResponse<object>.ErrorResponse("Bu kullanıcı zaten var."));

            var passwordHash = HashPassword(dto.Password);

            var user = new User
            {
                UserName = dto.UserName,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<object>.SuccessResponse(
                null,
                "Kullanıcı başarıyla oluşturuldu."
            ));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if (user == null)
                return BadRequest(ApiResponse<object>.ErrorResponse("Kullanıcı bulunamadı."));

            var passwordHash = HashPassword(dto.Password);

            if (user.PasswordHash != passwordHash)
                return BadRequest(ApiResponse<object>.ErrorResponse("Şifre yanlış."));

            var token = CreateToken(user);

            return Ok(ApiResponse<object>.SuccessResponse(token, "Giriş başarılı."));
        }
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        private string CreateToken(User user)
        {
            var jwtSettings = HttpContext.RequestServices
                .GetRequiredService<IConfiguration>()
                .GetSection("Jwt");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new System.Security.Claims.Claim("username", user.UserName)
    };

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}