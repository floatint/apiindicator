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
    public class CriteriasController : ControllerBase
    {
        private Models.DataContext DbContext { set; get; }

        public CriteriasController(Models.DataContext context)
        {
            DbContext = context;
        }



        //GET
        //URL : api/criterias
        //Get all criterias
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCriterias()
        {
            return Ok(await DbContext.Criterias.Include(x => x.Project.Id).ToListAsync());

            var criteriasList = await DbContext.Criterias.ToListAsync();
            var criteriasDTOList = new List<Models.DTO.CriteriaInfo>();
            foreach (var criteria in criteriasList)
            {
                Models.DTO.ProjectView criteriaProjectInfo = null;
                if (criteria.Project != null)
                {
                    criteriaProjectInfo = new Models.DTO.ProjectView
                    {
                        Id = criteria.Project.Id,
                        Name = criteria.Project.Name,
                        Version = criteria.Project.Version.Name
                    };
                }
                criteriasDTOList.Add(new Models.DTO.CriteriaInfo {
                    Id = criteria.Id,
                    Name = criteria.Name,
                    DifferencePercent = criteria.DifferencePercent,
                    Color = criteria.Color,
                    Project = criteriaProjectInfo
                });

            }

            return Ok(criteriasDTOList);
        }

        //GET
        //URL : api/criterias/{id}
        //Get {name} criteria
        [Authorize(Roles = "Admin, Tester")]
        public async Task<IActionResult> GetCriteria([FromHeader] long projectId, [FromQuery] string name)
        {

            var criteria = await DbContext.Criterias.Include(x => x.Project.Id == projectId).FirstOrDefaultAsync(x => x.Name == name);
            if (criteria == null)
            {

                //return StatusCode(StatusCodes.Status404NotFound, id);
            }
            var projectInfo = new Models.DTO.ProjectView();
            if (criteria.Project == null)
            {
                projectInfo = null;
            }
            else
            {
                projectInfo.Id = criteria.Project.Id;
                projectInfo.Name = criteria.Project.Name;
                projectInfo.Version = criteria.Project.Version.Name;
            }
            return Ok(new Models.DTO.CriteriaView {
                            Name = criteria.Name,
                            DifferencePercent = criteria.DifferencePercent,
                            Color = criteria.Color,
                            Project = projectInfo
            });
        }

        //POST
        //URL : api/criterias/
        //Create new criteria
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCriteria([FromBody] Models.DTO.NewCriteria newCriteria)
        {
            if (ModelState.IsValid)
            {
                var criteria = new Models.Criteria
                {
                    Name = newCriteria.Name,
                    DifferencePercent = newCriteria.DifferencePercent,
                    Color = newCriteria.Color,
                    Project = DbContext.Projects.FirstOrDefault(x => x.Id == newCriteria.ProjectId)
                };

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
        public async Task<IActionResult> UpdateCriteria(long id, [FromBody] Models.DTO.UpdateCriteria criteriaData)
        {
            var criteria = DbContext.Criterias.FirstOrDefault(x => x.Id == id);
            if (criteria == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, id);
            }
            if (criteriaData.Name != null)
                criteria.Name = criteriaData.Name;
            if (criteriaData.DifferencePercent.HasValue)
                criteria.DifferencePercent = criteriaData.DifferencePercent.Value;
            if (criteriaData.Color.HasValue)
                criteria.Color = criteriaData.Color.Value;
            DbContext.Criterias.Update(criteria);
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