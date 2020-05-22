﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models
{
    public class Product
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public long ProjectId { set; get; }
        public virtual Project Project { set; get; }
    }
}
