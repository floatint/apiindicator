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
    public class LogsController : ControllerBase
    {
        private Models.DataContext DataContext { set; get; }
        private IMapper Mapper { set; get; }

        public LogsController(Models.DataContext context, IMapper mapper)
        {
            DataContext = context;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogRecords()
        {
            var records = await DataContext.LogRecords.ToArrayAsync();
            return Ok(Mapper.Map<IList<Models.LogRecord>, List<Models.DTO.LogRecord>>(records));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewLogRecord([FromBody] Models.DTO.LogRecord logRecord)
        {
            if (ModelState.IsValid)
            {
                var product = await DataContext.Products.FirstOrDefaultAsync(x => x.Id == logRecord.ProductID);
                if (product == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, string.Format("Product with ID = {0} not found", logRecord.ProductID));
                }
                var project = await DataContext.Projects.FirstOrDefaultAsync(x => x.Id == logRecord.ProjectID);
                if (project == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, string.Format("Project with ID = {0} not found", logRecord.ProjectID));
                }
                var tester = await DataContext.Users.FirstOrDefaultAsync(x => x.Id == logRecord.TesterID);
                if (tester == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, string.Format("User with ID = {0} not found", logRecord.TesterID));
                }
                var version = await DataContext.Versions.FirstOrDefaultAsync(x => x.Name == logRecord.Version);
                if (version == null)
                {
                    version = new Models.Version { Name = logRecord.Version };
                    await DataContext.Versions.AddAsync(version);
                    await DataContext.SaveChangesAsync();
                }
                var newLogRecord = new Models.LogRecord
                {
                    Product = product,
                    Project = project,
                    URL = logRecord.URL,
                    Version = version,
                    DateTime = logRecord.DateTime,
                    Tester = tester
                };
                await DataContext.LogRecords.AddAsync(newLogRecord);
                await DataContext.SaveChangesAsync();
                return Ok(newLogRecord);
            }
            return BadRequest(ModelState);
        }
    }
}