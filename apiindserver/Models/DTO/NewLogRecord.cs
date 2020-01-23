using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiindserver.Models.DTO
{
    public class NewLogRecord
    {
        [Required(ErrorMessage = "Product ID not defined")]
        public long ProductID { set; get; }
        [Required(ErrorMessage = "Project ID not defined")]
        public long ProjectID { set; get; }
        [Required(ErrorMessage = "URL not defined")]
        public string URL { set; get; }
        [Required(ErrorMessage = "Version not defined")]
        public string Version { set; get; }
        [Required(ErrorMessage = "DateTime not defined")]
        public DateTime DateTime { set; get; }
        [Required(ErrorMessage = "Tester ID not defined")]
        public long TesterID { set; get; }
    }
}
