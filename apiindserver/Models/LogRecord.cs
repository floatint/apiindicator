using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiindserver.Models
{
    /// <summary>
    ///     Log record model
    /// </summary>
    public class LogRecord
    {
        public long ID { set; get; }
        public virtual Product Product { set; get; }
        public virtual Project Project { set; get; }
        public string URL { set; get; }
        public virtual Version Version { set; get; }
        public DateTime DateTime { set; get; }
        public virtual User Tester { set; get; }

    }
}
