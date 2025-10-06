using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Dal.Interfaces;
using Logic.Models;
using Logic.Services.LoginUser.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Logic.Services.LoginUser;

/// <inheritdoc/>
public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _tokenRepository;
    private readonly IConfiguration _configuration; // для доступа к секретному ключу и настройкам JWT

    /// <summary>
    /// Конструктор
    /// </summary>
    public LoginUserUseCase(
        IUserRepository userRepository, 
        IRefreshTokenRepository tokenRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _configuration = configuration;
    }

    public async Task<(UserDto user, string accessToken, string refreshToken)> ExecuteAsync(string emailOrUsername, string password)
    {
        var user = await _userRepository.GetByEmailAsync(emailOrUsername) 
                   ?? await _userRepository.GetByUsernameAsync(emailOrUsername);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            throw new InvalidCredentialException("Неверный логин или пароль");
        }

        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["AccessTokenMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        var refreshTokenValue = Guid.NewGuid().ToString();
        var refreshToken = new Dal.Models.RefreshToken
        {
            Id = Guid.NewGuid(),
            TokenHash = BCrypt.Net.BCrypt.HashPassword(refreshTokenValue),
            UserId = user.Id,
            Created = DateTimeOffset.UtcNow,
            Expires = DateTimeOffset.UtcNow.AddDays(int.Parse(jwtSettings["RefreshTokenDays"]))
        };

        await _tokenRepository.AddAsync(refreshToken);

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            VkLink = user.VkLink,
            TgLink = user.TgLink,
            Description = user.Description,
            SkillLevel = user.SkillLevel
        };

        return (userDto, accessToken, refreshTokenValue);
    }
}
