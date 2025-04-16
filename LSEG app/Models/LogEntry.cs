using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSEG_app.Models
{
    internal class LogEntry
    {
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        public string PID { get; set; }
    }
}
