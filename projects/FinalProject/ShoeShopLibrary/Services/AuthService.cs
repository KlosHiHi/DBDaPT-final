using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoeShopLibrary.Contexts;
using ShoeShopLibrary.DTOs;
using ShoeShopLibrary.Models;
using ShoeShopLibrary.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoeShopLibrary.Services
{
    public class AuthService(ShoeShopDbContext context)
    {
        private readonly ShoeShopDbContext _context = context;
        private int _jwtActiveMinutes = 15;

        private bool VerifyPassword(string password, string passwordHash)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);

        private bool IsAuthCorrect(string password, User user)
        {
            if (VerifyPassword(password, user.Password))
                return true;
            return false;
        }

        public async Task<string?> AuthUserAsync(LoginRequest request)
        {
            string login = request.Login;
            string password = request.Password;

            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
                return null;

            var user = await GetUserByLoginAsync(login);
            if (user is null)
                return null;

            return IsAuthCorrect(password, user) ?
                await GenerateToken(user) :
                null;
        }

        private async Task<string> GenerateToken(User user)
        {
            int id = user.UserId;
            string login = user.Login;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var role = await GetUserRoleAsync(login);

            var claims = new Claim[]
            {
                new ("id", id.ToString()),
                new ("login", login),
                new ("role", role.Name),
            };

            var token = new JwtSecurityToken(
                signingCredentials: credentials,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtActiveMinutes),
                issuer: AuthOptions.issuer,
                audience: AuthOptions.audience);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> GetUserByLoginAsync(string login)
            => await _context.Users
                .FirstOrDefaultAsync(u => u.Login == login) ?? null!;

        private async Task<Role?> GetUserRoleAsync(string login)
        {
            var user = await _context.Users
                .Include(c => c.Role)
                .FirstOrDefaultAsync(cu => cu.Login == login);

            return user is not null ?
                 user.Role :
                 null;
        }
    }
}
