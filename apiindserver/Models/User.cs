using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models
{
    public class User
    {
        public long Id { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public virtual Role Role { set; get; }
    }
}
