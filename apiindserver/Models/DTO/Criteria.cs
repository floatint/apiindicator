using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models.DTO
{
    public class Criteria
    {
        public string Name { set; get; }
        public double? MinDiffPercent { set; get; }
        public double? MaxDiffPercent { set; get; }
        public int? Color { set; get; }
        public long? ProjectId { set; get; }
    }
}
