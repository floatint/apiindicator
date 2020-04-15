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
    public class ReportsController : ControllerBase
    {
        private Models.DataContext DbContext { set; get; }

        public ReportsController(Models.DataContext context)
        {
            DbContext = context;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProjectDifference(long id, [FromBody] string version)
        {
            var project = DbContext.Projects.FirstOrDefault(x => x.Id == id);
            if (project == null)
                return StatusCode(StatusCodes.Status404NotFound, id);
            //get avg recponse time for current project version
            var currVerAvgRespTime = DbContext.LogRecords
                .Include(x => x.Project)
                .Where(x => x.Project == project)
                .Where(x => x.Version.Name == version)
                .Average(x => x.ResponseTime.Milliseconds);
            //get avg response time for other version
            var otherVerAvgRespTime = DbContext.LogRecords
                .Include(x => x.Project)
                .Where(x => x.Project == project)
                .Where(x => x.Version.Name != version)
                .Average(x => x.ResponseTime.Milliseconds);

            //TODO: handle criteria

            return Ok(otherVerAvgRespTime - currVerAvgRespTime);

        }




    }
}