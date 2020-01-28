using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiindserver.Models.DTO
{
    public class UpdatedProduct
    {
        [Required(ErrorMessage = "Product name not defined")]
        public string Name { set; get; }
    }
}
