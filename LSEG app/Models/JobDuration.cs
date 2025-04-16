using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSEG_app.Models
{
    internal class JobDuration
    {
        public string PID { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Automatically calculates job duration
        public TimeSpan Duration => EndTime - StartTime;
    }
}
