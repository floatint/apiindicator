using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models.DTO
{
    public class User
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public long? RoleId { set; get; }
        public virtual Role Role { set; get;}
    }
}
