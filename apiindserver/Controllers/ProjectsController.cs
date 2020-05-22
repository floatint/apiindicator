using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace apiindserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private Models.DataContext DbContext { set; get; }
        private IMapper Mapper { set; get; }

        public ProjectsController(Models.DataContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        //GET
        //URL : api/projects
        //Get all projects
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProjects()
        {
            
            return Ok(await DbContext.Projects.Include(x => x.Version).ToArrayAsync());
        }

        //GET
        //URL : api/projects/{id}
        //Get {id} project info
        [HttpGet("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProject(long id)
        {
            var proj = await DbContext.Projects
                                      .Include(x => x.Products)
                                      .Include(x => x.Testers)
                                      .Include(x => x.Version)
                                      .FirstOrDefaultAsync(x => x.Id == id);
            if (proj == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, string.Format("Project with ID = {0} not found", id));
            }
            return Ok(proj);
        }

        //GET
        //URL api/projects/{id}/products
        //Get product's list of {id} project

        //POST
        //URL : api/projects/add
        //Add new project
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProject([FromBody] Models.DTO.Project newProject)
        {
            if (ModelState.IsValid)
            {
                var project = new Models.Project
                {
                    Name = newProject.Name,
                    Version = newProject.Version
                };
                await DbContext.Projects.AddAsync(project);
                await DbContext.SaveChangesAsync();
                return Ok(project);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProject([FromBody] Models.DTO.Project upProj, long id)
        {
            if (ModelState.IsValid)
            {
                var project = await DbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, string.Format("Project with ID = {0} not found", 0));
                }
                project.Name = upProj.Name;
                project.Version = upProj.Version;
                DbContext.Projects.Update(project);
                await DbContext.SaveChangesAsync();
                return Ok(project);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var project = await DbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, string.Format("Project with ID = {0} not found", id));
            }
            return Ok(project);
        }

    }
}