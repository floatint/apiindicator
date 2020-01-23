using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiindserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private Models.DataContext DbContext { set; get; }

        public ProductsController(Models.DataContext context)
        {
            DbContext = context;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProjectProducts(long id)
        {
            var project = await DbContext.Projects
                                         .Include(x => x.Products)
                                         .FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, string.Format("Project with ID = {0} not found", id));
            }
            return Ok(project.Products);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct([FromBody] Models.DTO.NewProduct newProduct)
        {
            if (ModelState.IsValid)
            {
                var project = await DbContext.Projects.FirstOrDefaultAsync(x => x.Id == newProduct.ProjectID);
                if (project == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, string.Format("Project with ID = {0} not found", newProduct.ProjectID));
                }
            }
            return BadRequest(ModelState);
        }
    }
}