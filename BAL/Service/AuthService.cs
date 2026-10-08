using DAO.IDAO;
using Futsal_Management.Domain.GenericResponse;
using Futsal_Management.Domain.Model;
using Futsal_Management.Domain.ViewModel;
using Futsal_Management.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Futsal_Management.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthDao _authDao;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<string> _passwordHasher;

        public AuthService(
            IAuthDao authDao,
            IConfiguration configuration)
        {
            _authDao = authDao;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<string>();
        }

        public async Task<ResponseResult<LoginResponseDto>> Login(
            LoginDto loginDto)
        {
            try
            {
                if (loginDto == null)
                {
                    return ResponseResult<LoginResponseDto>.Failure(
                        null,
                        "Please enter email and password");
                }

                var user = await _authDao.GetUserByEmailAsync(
                    loginDto.Email!);

                if (user == null)
                {
                    return ResponseResult<LoginResponseDto>.Failure(
                        null,
                        "Invalid email or password");
                }

                var passwordResult =
                    _passwordHasher.VerifyHashedPassword(
                        user.Email!,
                        user.Password!,
                        loginDto.Password!);

                if (passwordResult == PasswordVerificationResult.Failed)
                {
                    return ResponseResult<LoginResponseDto>.Failure(
                        null,
                        "Invalid email or password");
                }

                if (user.UserGroupId != 6 &&
                    user.UserGroupId != 3)
                {
                    return ResponseResult<LoginResponseDto>.Failure(
                        null,
                        "You are not authorized to login");
                }

                var token = GenerateToken(user);

                var response = new LoginResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    UserGroupId = user.UserGroupId,
                    Token = token
                };

                return ResponseResult<LoginResponseDto>.Success(
                    response,
                    "Login successful");
            }
            catch (Exception ex)
            {
                return ResponseResult<LoginResponseDto>.Failure(
                    null,
                    $"Login error: {ex.Message}");
            }
        }

        private string GenerateToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new Exception(
                    "JWT Key is not configured");
            }

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Name ?? string.Empty),

                new Claim(
                    ClaimTypes.Email,
                    user.Email ?? string.Empty),

                new Claim(
                    "UserGroupId",
                    user.UserGroupId.ToString()),

                new Claim(
                    ClaimTypes.Role,
                    user.UserGroupId.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expiryMinutes = Convert.ToDouble(
                _configuration["Jwt:ExpiryMinutes"] ?? "60");

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    expiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}