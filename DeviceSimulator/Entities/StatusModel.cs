using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceSimulator.Entities
{
    class StatusModel
    {
        public int StandID { get; set; }
        public int Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Description { get; set; }
    }
}
