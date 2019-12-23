using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class LongTermInsulinRecordingDataModel
    {
        public int Quantity { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
