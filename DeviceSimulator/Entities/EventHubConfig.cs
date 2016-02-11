using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceSimulator.Entities
{
    class EventHubConfig
    {
        public string ConnectionString;
        public string EventHubName;
        public string StandDelay;
        public string StatusDelayMin;
        public string StatusDelayMax;
        public string StatusCompleteDelay;
    }
}
