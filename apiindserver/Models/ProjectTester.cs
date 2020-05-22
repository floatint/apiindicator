using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models
{
    public class ProjectTester
    {
        public long TesterId { set; get; }
        public User Tester { set; get; }
        public long ProjectId { set; get; }
        public Project Project { set; get; }
    }
}
