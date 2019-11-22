using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiindserver.Models.DTO
{
    public class NewCriteria
    {
        [Required(ErrorMessage = "Criteria name not defined")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Difference percent value not defined")]
        public double DifferencePercent { set; get; }
        [Required(ErrorMessage = "Criteria color not defined")]
        public int Color { set; get; }
        public long? ProjectId { set; get; }
    }
}
