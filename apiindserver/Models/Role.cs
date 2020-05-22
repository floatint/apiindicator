using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models
{
    public class Role
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public ICollection<UserRole> Users { set; get; }
    }
}
