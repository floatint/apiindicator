﻿using System;
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
        public virtual ICollection<ProjectTester> Testers { set; get; }
        public virtual ICollection<Criteria> Criterias { set; get; }
        public string Version { set; get; }
    }
}
