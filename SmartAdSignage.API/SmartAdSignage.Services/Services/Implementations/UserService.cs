using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository.Repositories.Interfaces;
using SmartAdSignage.Services.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SmartAdSignage.Services.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usersRepository;
        private readonly IConfiguration _configuration;
        private User? _user;

        public UserService(IUserRepository usersRepository, IConfiguration configuration)
        {
            this._usersRepository = usersRepository;
            this._configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            var result = await _usersRepository.CreateUserAsync(user, password);
            if (!result.Succeeded)
            {
                return result;
            }    
            await _usersRepository.AddRoleToUserAsync(user, "User");
            return result;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            _user = await _usersRepository.GetByUsernameAsync(username);
            var result = _user != null && await _usersRepository.CheckPasswordForUserAsync(_user, password);
            return result;
        }

        public async Task<string[]?> GenerateTokensAsync()
        {
            var token = await GenerateAccessTokenAsync();
            var refreshToken = GenerateRefreshTokenAsync();
            _user.RefreshToken = refreshToken;
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(GetConfiguration("refreshTokenExpiresInDays")/*["refreshTokenExpiresInDays"]*/));
            await _usersRepository.UpdateUser(_user);
            return new string[] { token, refreshToken };
        }

        private async Task<string> GenerateAccessTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<string[]?> RefreshTokensAsync(string accessToken, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            _user = await _usersRepository.GetByUsernameAsync(username);
            if (_user is null || _user.RefreshToken != refreshToken || _user.RefreshTokenExpiryTime <= DateTime.Now)
                return null;
            var newToken = await GenerateAccessTokenAsync();
            var newRefreshToken = GenerateRefreshTokenAsync();
            _user.RefreshToken = newRefreshToken;
            await _usersRepository.UpdateUser(_user);
            /*await _usersRepository.UpdateUser(_user);*/
            return new string[] { newToken, newRefreshToken };
        }

        public async Task<IdentityResult> RevokeToken(string username)
        {
            _user = await _usersRepository.GetByUsernameAsync(username);
            if (_user == null)
                return null;

            _user.RefreshToken = null;
            _user.RefreshTokenExpiryTime = DateTime.MinValue;
            var res = await _usersRepository.UpdateUser(_user);
            return res;
        }

        public async Task<IEnumerable<User?>> GetUsersAsync()
        {
            var users = await _usersRepository.GetAllAsync();
            return users;
        }

        public async Task<IdentityResult> RegisterAdminAsync(User user, string password)
        {
            var result = await _usersRepository.CreateUserAsync(user, password);
            await _usersRepository.AddRoleToUserAsync(user, "Admin");
            return result;
        }

        private SigningCredentials GetSigningCredentials()
        {
            /*var jwtConfig = GetConfiguration();*/
            var key = Encoding.UTF8.GetBytes(GetConfiguration("secret")/* jwtConfig["secret"]*/);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };
            var roles = await _usersRepository.GetRolesForUserAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            /*var jwtSettings = GetConfiguration();*/
            var tokenOptions = new JwtSecurityToken
            (
                issuer: GetConfiguration("validIssuer") /*jwtSettings["validIssuer"]*/,
                audience: GetConfiguration("validAudience") /*jwtSettings["validAudience"]*/,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(GetConfiguration("expiresInMinutes")/*jwtSettings["expiresInMinutes"]*/)),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        public string? GetConfiguration(string setting) 
        {
            return _configuration.GetSection("JwtConfig")[setting];
        }

        private string GenerateRefreshTokenAsync()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                string token = Convert.ToBase64String(randomNumber);
                return token;
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetConfiguration("secret")/*["secret"]*/)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            var result = await _usersRepository.GetByUsernameAsync(userName);
            return result;
        }

        public async Task<IdentityResult> DeleteUserByNameAsync(string userName)
        {
            var user = await _usersRepository.GetByUsernameAsync(userName);
            if (user == null)
                return null;
            return await _usersRepository.DeleteUserAsync(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(string userName, User user)
        {
            var existingUser = await _usersRepository.GetByUsernameAsync(userName);
            if (existingUser == null)
                return null;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.CompanyName = user.CompanyName;
            existingUser.Email = user.Email;
            existingUser.UserName = user.Email;
            return await _usersRepository.UpdateUser(existingUser);
        }
    }
}
