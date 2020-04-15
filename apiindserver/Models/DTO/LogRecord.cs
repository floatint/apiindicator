using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiindserver.Models.DTO
{
    public class LogRecord
    {
        [Required(ErrorMessage = "Product ID not defined")]
        public long ProductID { set; get; }
        [Required(ErrorMessage = "Project ID not defined")]
        public long ProjectID { set; get; }
        [Required(ErrorMessage = "URL not defined")]
        public string URL { set; get; }
        [Required(ErrorMessage = "Version not defined")]
        public string Version { set; get; }
        [Required(ErrorMessage = "Date not defined")]
        public DateTime Date { set; get; }
        [Required(ErrorMessage = "Response time not defined")]
        public TimeSpan ResponseTime { set; get; }
        [Required(ErrorMessage = "Tester ID not defined")]
        public long TesterID { set; get; }
    }
}
