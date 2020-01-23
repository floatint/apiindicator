using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiindserver.Models.DTO
{
    public class UpdatedProject
    {
        [Required(ErrorMessage = "Project name not defined")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Project version not defined")]
        public string Version { set; get; }
    }
}
