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
    public class UsersController : ControllerBase
    {

        private Models.DataContext DbContext { set; get; }
        //private AppSettings AppSettings { set; get; }

        public UsersController(Models.DataContext context) 
        {
            DbContext = context;
            //AppSettings = AppSettings;
        }

        //GET
        //URL : api/users
        //Get all users list
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var usersList = await DbContext.Users.Include(x => x.Role).ToListAsync();
            var usersDTOList = new List<Models.DTO.UserInfo>();
            foreach(var user in usersList)
            {
                usersDTOList.Add(new Models.DTO.UserInfo { Id = user.Id, Login = user.Login, Role = user.Role });
            }
            return Ok(usersDTOList);
        }

        //GET
        //URL : api/users/{id}/
        //Get {id} user's info
        [HttpGet("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await DbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                return Ok(new Models.DTO.UserView { Login = user.Login, Role = user.Role});
            }
            return StatusCode(StatusCodes.Status404NotFound, id);
        }

        //POST
        //URL : api/users/
        //Register the new user
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] Models.DTO.RegisterUser newUser)
        {
            if (ModelState.IsValid)
            {
                var role = DbContext.Roles.FirstOrDefault(x => x.Id == newUser.RoleID);
                if (role == null)
                {
                    role = DbContext.Roles.FirstOrDefault(x => x.Name == "User");
                }
                var user = new Models.User
                {
                    Login = newUser.Login,
                    Password = Hash(newUser.Password),
                    Role = role
                };

                DbContext.Users.Add(user);
                await DbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //DELETE
        //URL : api/users/{id}
        //Delete {id} user
        [HttpDelete("id:long")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                DbContext.Users.Remove(new Models.User { Id = id });
                await DbContext.SaveChangesAsync();
                return Ok();
            } 
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //PUT
        //URL : api/users/{id}
        //Update {id} user's information.
        [HttpPut("id:long")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] Models.DTO.UpdateUser userObj)
        {
            if (ModelState.IsValid)
            {
                var user = DbContext.Users.Include(x => x.Role).FirstOrDefault(x => x.Id == id);
                if (user == null)
                    return StatusCode(StatusCodes.Status404NotFound, id);
                var role = DbContext.Roles.FirstOrDefault(x => x.Id == userObj.RoleId);
                if (role == null)
                {
                    role = user.Role;
                }

                user.Login = userObj.Login;
                user.Role = role;
                DbContext.Users.Update(user);
                await DbContext.SaveChangesAsync();
                return Ok(userObj);
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