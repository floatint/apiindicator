using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiindserver.Models.DTO
{
    public class NewProduct
    {
        [Required(ErrorMessage = "Project ID not defined")]
        public long ProjectID { set; get; }
        [Required(ErrorMessage = "Product name not defined")]
        public string ProductName { set; get; }
    }
}
