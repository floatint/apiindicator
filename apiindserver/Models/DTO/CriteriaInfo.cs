using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models.DTO
{
    public class CriteriaInfo
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public double DifferencePercent { set; get; }
        public int Color { set; get; }
        public ProjectView Project { set; get; }
    }
}
