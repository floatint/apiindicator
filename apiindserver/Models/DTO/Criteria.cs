using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models.DTO
{
    public class Criteria
    {
        public string Name { set; get; }
        public double? DifferencePercent { set; get; }
        public int? Color { set; get; }
        public long? ProjectId { set; get; }
    }
}
