using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace apiindserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private Models.DataContext DbContext { set; get; }
        

        public RolesController(Models.DataContext context)
        {
            DbContext = context;
        }

        //GET
        //URL : api/roles
        //Get all roles
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllRoles()
        {
            return Ok(DbContext.Roles.ToList());
        }

        //GET
        //URL : api/roles/{id}
        //Get {id} role's info
        [HttpGet("{id:long}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetRole(long id)
        {
            var role = DbContext.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null)
                return StatusCode(StatusCodes.Status404NotFound, id);
            return Ok(role.Name);
        }

        //PUT
        //URL : api/roles/{id}
        //Change {id} role's name
        [HttpPut("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(long id, [FromBody] string roleName)
        {
            var role = DbContext.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null)
                return StatusCode(StatusCodes.Status404NotFound, id);
            role.Name = roleName;
            DbContext.Roles.Update(role);
            await DbContext.SaveChangesAsync();
            return Ok(id);
        }

        //DELETE
        //URL : api/roles/{id}
        //Delete {id} role
        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(long id)
        {
            var role = DbContext.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null)
                return StatusCode(StatusCodes.Status404NotFound, id);
            DbContext.Roles.Remove(role);
            await DbContext.SaveChangesAsync();
            return Ok(role);
        }

        //POST
        //URL : api/roles/add
        //Add new role
        [HttpPost("new")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole([FromBody] string roleName)
        {
            DbContext.Roles.Add(new Models.Role { Name = roleName});
            await DbContext.SaveChangesAsync();
            return Ok();
        }


    }
}