using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models
{
    public class UserRole
    {
        public long UserId { set; get; }
        public User User { set; get; }
        public long RoleId { set; get; }
        public Role Role { set; get; }
    }
}
