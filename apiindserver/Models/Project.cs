using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models
{
    public class Project
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public virtual ICollection<Product> Products { set; get; }
        public virtual ICollection<User> Testers { set; get; }
        public virtual Version Version { set; get; }

        public Project()
        {
            Products = new List<Product>();
            Testers = new List<User>();
        }
    }
}
