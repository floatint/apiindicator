using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace apiindserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private Models.DataContext DbContext { set; get; }
        private AppSettings AppSettings { set; get; }

        public AuthController(Models.DataContext context, IOptions<AppSettings> settings)
        {
            DbContext = context;
            AppSettings = settings.Value;
        }

        //POST
        //URL : api/users/login
        //User's authentication
        [HttpPost("login")]
        public IActionResult Login([FromBody] Models.DTO.Auth.LoginUser userObj)
        {
            if (ModelState.IsValid)
            {
                var user = DbContext.Users.Include(x => x.Roles).FirstOrDefault(x => x.Login == userObj.Login);
                //var r = user.Role.Name;
                
                if (user != null && user.Password == Hash(userObj.Password))
                {
                    
                    var tokenDescr = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim("UserID", user.Id.ToString()),
                            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                            //new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
                        }),

                        Expires = DateTime.UtcNow.AddHours(6),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JWTSecret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescr);
                    var token = tokenHandler.WriteToken(securityToken);
                    return Ok(new { token });
                }
            }
            return BadRequest(ModelState);
        }

        [NonAction]
        public static string Hash(string password)
        {
            byte[] data = Encoding.Default.GetBytes(password);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(data);
            password = Convert.ToBase64String(result);
            return password;
        }
    }
}