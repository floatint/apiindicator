using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiindserver.Models.DTO
{
    public class Project
    {
        public string Name { set; get; }
        public string Version { set; get; }
    }
}
