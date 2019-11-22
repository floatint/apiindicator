using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace apiindserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private Models.DataContext DbContext { set; get; }

        public ProjectsController(Models.DataContext context)
        {
            DbContext = context;
        }

        //GET
        //URL : api/projects
        //Get all projects
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllProjects()
        {
            var projectsList = DbContext.Projects.ToList();
            var projectsDTOList = new List<Models.DTO.ProjectView>();
            foreach(var project in projectsList)
            {
                projectsDTOList.Add(new Models.DTO.ProjectView {
                    Id = project.Id,
                    Name = project.Name,
                    Version = project.Version.Name
                });
            }
            return Ok(projectsDTOList);
        }

        //GET
        //URL : api/projects/{id}
        //Get {id} project info
        [HttpGet("{id:long}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetProject(long id)
        {
            var project = DbContext.Projects
                .Include(x => x.Testers)
                .Include(x => x.Products)
                .FirstOrDefault(x => x.Id == id);
            if (project == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, id);
            }
            var projectInfo = new Models.DTO.ProjectInfo();
            foreach(var tester in project.Testers)
            {
                projectInfo.Testers.Add(new Models.DTO.UserInfo {
                    Id = tester.Id,
                    Login = tester.Login,
                    Role = tester.Role
                });
            }
            foreach(var product in project.Products)
            {
                projectInfo.Products.Add(new Models.DTO.ProductInfo {
                    Id = product.Id,
                    Name = product.Name
                });
            }
            return Ok(projectInfo);
        }

        //GET
        //URL api/projects/{id}/products
        //Get product's list of {id} project

        //POST
        //URL : api/projects/add
        //Add new project
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProject([FromBody] Models.DTO.NewProject newProject)
        {
            if (ModelState.IsValid)
            {
                var version = await DbContext.Versions.FirstOrDefaultAsync(x => x.Name == newProject.Version);
                if (version == null)
                {
                    version = new Models.Version { Name = newProject.Version };
                    DbContext.Versions.Add(version);
                    await DbContext.SaveChangesAsync();
                }
                DbContext.Projects.Add(new Models.Project
                {
                    Name = newProject.Name,
                    Version = await DbContext.Versions.FirstOrDefaultAsync(x => x.Name == newProject.Version)
                });
                await DbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}