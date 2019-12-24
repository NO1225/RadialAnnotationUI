using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class LongTermInsulinRecordingDataModel
    {
        public int Amount { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
