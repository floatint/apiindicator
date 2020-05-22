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
    public class CriteriasController : ControllerBase
    {
        private Models.DataContext DbContext { set; get; }
        private IMapper Mapper { set; get; }

        public CriteriasController(Models.DataContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        //GET
        //URL : api/criterias
        //Get all criterias
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCriterias()
        {
            var data = await DbContext.Criterias.ToListAsync();
            return Ok(Mapper.Map<IList<Models.Criteria>, IList<Models.DTO.Criteria>>(data));
        }

        [HttpGet("{diff:double}")]
        public async Task<IActionResult> GetCriteriaByDiff([FromQuery] double diff, [FromHeader] long projectId)
        {
            //try get concrete project criteria
            var criteria = await DbContext.Criterias.FirstOrDefaultAsync(x => (x.ProjectId == projectId) && (diff >= x.MinDiffPercent && diff <= x.MaxDiffPercent));
            //if project hasn't criteria
            if (criteria == null)
            {
                //try get common criteria
                criteria = await DbContext.Criterias.FirstOrDefaultAsync(x => diff >= x.MinDiffPercent && diff <= x.MaxDiffPercent);
                if (criteria == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, diff);
                }
            }
            return Ok(criteria);
        }


        //POST
        //URL : api/criterias/
        //Create new criteria
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCriteria([FromBody] Models.DTO.Criteria newCriteria)
        {
            if (ModelState.IsValid)
            {
                if (newCriteria.Name == null || newCriteria.Name.Length == 0)
                    return StatusCode(StatusCodes.Status400BadRequest, "Criteria name is null");
                if (newCriteria.ProjectId == null)
                {
                    var existsCriteria = await DbContext.Criterias.FirstOrDefaultAsync(x => x.Name == newCriteria.Name);
                    if (existsCriteria != null)
                        return StatusCode(StatusCodes.Status400BadRequest, "Criteria already exists");
                }
                var criteria = Mapper.Map<Models.DTO.Criteria, Models.Criteria>(newCriteria);
                DbContext.Criterias.Add(criteria);
                await DbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //PUT
        //URL : api/criterias/{id}
        //Update criteria
        [HttpPut("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCriteria(long id, [FromBody] Models.DTO.Criteria criteriaData)
        {
            var criteria = DbContext.Criterias.FirstOrDefault(x => x.Id == id);
            if (criteria == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, id);
            }
            var newCriteria = Mapper.Map<Models.DTO.Criteria, Models.Criteria>(criteriaData);
            newCriteria.Id = id;
            DbContext.Criterias.Update(newCriteria);
            await DbContext.SaveChangesAsync();
            return Ok(id);
        }

        //DELETE
        //URL : api/criterias/{id}
        //Delete {id} criteria
        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCriteria(long id)
        {
            var criteria = DbContext.Criterias.FirstOrDefault(x => x.Id == id);
            if (criteria == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, id);
            }
            DbContext.Criterias.Remove(criteria);
            await DbContext.SaveChangesAsync();
            return Ok();
        }

    }
}