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
using AutoMapper;

namespace apiindserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private Models.DataContext DbContext { set; get; }
        private IMapper Mapper { set; get; }
        //private AppSettings AppSettings { set; get; }

        public UsersController(Models.DataContext context, IMapper mapper) 
        {
            DbContext = context;
            Mapper = mapper;
            //AppSettings = AppSettings;
        }

        //GET
        //URL : api/users
        //Get all users list
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var usersList = await DbContext.Users.Include(x => x.Roles).ToListAsync();
            return Ok(Mapper.Map<List<Models.User>, List<Models.DTO.User>>(usersList));
        }

        //GET
        //URL : api/users/{id}/
        //Get {id} user's info
        [HttpGet("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await DbContext.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                return Ok(Mapper.Map<Models.DTO.User>(user));
                //return Ok(new Models.DTO.UserView { Login = user.Login, Role = user.Role});
            }
            return StatusCode(StatusCodes.Status404NotFound, id);
        }

        //POST
        //URL : api/users/
        //Register the new user
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser([FromBody] Models.DTO.Auth.NewUser newUser)
        {
            if (ModelState.IsValid)
            {
                var role = DbContext.Roles.FirstOrDefault(x => x.Id == newUser.RoleID);
                if (role == null)
                {
                    role = DbContext.Roles.FirstOrDefault(x => x.Name == "User");
                }
                //TODO: fix role set
                var user = new Models.User
                {
                    Login = newUser.Login,
                    Password = Hash(newUser.Password),
                    Roles = new List<Models.UserRole> {  }
                };

                await DbContext.Users.AddAsync(user);
                await DbContext.SaveChangesAsync();
                return Ok(Mapper.Map<Models.DTO.User>(user));
                //return Ok(new Models.DTO.UserView{
                //    Login = user.Login,
                //    Role = user.Role
                //}
                //);
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
            var user = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, string.Format("User with ID = {0} not found", id));
            }
            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync();
            return Ok(Mapper.Map<Models.DTO.User>(user));
            //return Ok(new Models.DTO.UserView {
            //    Login = user.Login,
            //    Role = user.Role
            //});
        }

        //PUT
        //URL : api/users/{id}
        //Update {id} user's information.
        [HttpPut("id:long")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] Models.DTO.User userObj)
        {
            if (ModelState.IsValid)
            {
                var user = DbContext.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id == id);
                if (user == null)
                    return StatusCode(StatusCodes.Status404NotFound, id);
                var role = DbContext.Roles.FirstOrDefault(x => x.Id == userObj.RoleId);
                //Collectio
                if (role == null)
                {
                    //role = user.Roles;
                }

                user.Name = userObj.Name;
                //user.Roles.Add(role);
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