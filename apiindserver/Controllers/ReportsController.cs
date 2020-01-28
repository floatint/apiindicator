using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiindserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private Models.DataContext DbContext { set; get; }

        public ReportsController(Models.DataContext context)
        {
            DbContext = context;
        }

    }
}