using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models.DTO
{
    public class ProjectInfo
    {
        public ICollection<UserInfo> Testers { set; get; }
        public ICollection<ProductInfo> Products { set; get; }
    }
}
