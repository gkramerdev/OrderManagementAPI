using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OrderManagementAPI.DTO;
using OrderManagementAPI.Models;
using OrderManagementAPI.Repositories;

namespace OrderManagementAPI.Services
{
    public class UserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRespository;

        public UserService(IUserRepository userRespository, IConfiguration configuration)
        {
            _userRespository = userRespository;
            _configuration = configuration;

        }

        public async Task<UserModel> CreateUserAsync(UserRequestDTO userRequestDTO)
        {
            var user = new UserModel
            {
                Name = userRequestDTO.Name,
                Email = userRequestDTO.Email,
                Senha = userRequestDTO.Senha,

            };

            return await _userRespository.CreateUserAsync(user);
        }

        public async Task<string> AuthenticateAsync(string email, string senha)
        {
            var user = await _userRespository.GetUserByEmailAsync(email);
            if (user == null || user.Senha != senha)
            {
                throw new UnauthorizedAccessException("Email ou senha inválidos");

            }

            var token = GenerateJwtToken(user);
            return token.ToString();
        }

        private string GenerateJwtToken(UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}

