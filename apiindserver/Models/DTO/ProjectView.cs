using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models.DTO
{
    public class ProjectView
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Version { set; get; }
    }
}
