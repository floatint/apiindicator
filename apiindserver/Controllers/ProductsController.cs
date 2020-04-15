using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace apiindserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private Models.DataContext DbContext { set; get; }
        private IMapper Mapper { set; get; }

        public ProductsController(Models.DataContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
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

        [HttpPost("{id:long}")]
        public async Task<IActionResult> AddNewProduct(long id, [FromBody] string newProductName)
        {
            if (ModelState.IsValid)
            {
                var project = await DbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, string.Format("Project with ID = {0} not found", id));
                }
                var product = new Models.Product
                {
                    Name = newProductName
                };
                await DbContext.Products.AddAsync(product);
                project.Products.Add(product);
                DbContext.Projects.Update(project);
                await DbContext.SaveChangesAsync();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] string newProductName)
        {
            if (ModelState.IsValid)
            {
                var product = await DbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, string.Format("Product with ID = {0} not found", id));
                }
                product.Name = newProductName;
                DbContext.Products.Update(product);
                await DbContext.SaveChangesAsync();
                return Ok(product);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await DbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return StatusCode(StatusCodes.Status404NotFound, id);
            DbContext.Products.Remove(product);
            await DbContext.SaveChangesAsync();
            return Ok();

        }
    }
}