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
        public long? ProductID { set; get; }
        public virtual Product Product { set; get; }
        public long? ProjectID { set; get; }
        public virtual Project Project { set; get; }
        public string URL { set; get; }
        public string Version { set; get; }
        public DateTime DateTime { set; get; }
        public TimeSpan ResponseTime { set; get; }
        public long? TesterId { set; get; }
        public virtual User Tester { set; get; }
    }
}
