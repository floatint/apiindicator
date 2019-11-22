using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models.DTO
{
    public class CriteriaView
    {
        public string Name { set; get; }
        public double DifferencePercent { set; get; }
        public int Color { set; get; }
        public virtual ProjectView Project { set; get; }
    }
}
