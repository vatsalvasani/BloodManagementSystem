using BloodManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BloodManagement.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DbContext1 _context;
        private readonly IConfiguration _configuration;

        public AuthRepo(DbContext1 context, IConfiguration configuration) { 
            _context = context;
            _configuration = configuration;
        }
        public async Task<long> Register(User1 user,string password) {
            if(await UserExists(user.Email))
            {
                return 0;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.User1.Add(user);
            await _context.SaveChangesAsync();
            return user.User1Id;
    
        }

        public async Task<string> Login(string email,string password)
        {
            var user = await _context.User1.FirstOrDefaultAsync(u => u.Email == email);
            if(user == null)
            {
                return null;
            }
            else if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                return null;
            }
            else
            {
                return CreateToken(user);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        
        
        }

        public async Task<bool> UserExists(string email) {
            User1 user = await _context.User1.FirstOrDefaultAsync(u => u.Email == email);
            if (user!=null) {
                return true;

            }
            return false;
    
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) { 
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        
        
        }
        private string CreateToken(User1 user) {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.User1Id.ToString()),
                new Claim(ClaimTypes.Name,user.Email)
            };
            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if(appSettingsToken == null)
            {
                throw new Exception("AppSettings Token Is Null");
            }
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(365),
                SigningCredentials = signingCredentials
            };
            JwtSecurityTokenHandler tokenHandler= new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

    }
}
