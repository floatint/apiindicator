using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models
{
    public class Criteria
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public double? MinDiffPercent { set; get; }
        public double? MaxDiffPercent { set; get; }
        public int Color { set; get; }
        public long? ProjectId { set; get; }
        public virtual Project Project { set; get; }
    }
}
