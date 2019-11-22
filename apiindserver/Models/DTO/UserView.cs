using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models.DTO
{
    public class UserView
    {
        public string Login { set; get; }
        public Role Role { set; get; }
    }
}
