using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class GlucoseLevelRecordingDataModel
    {
        public int GlucoseLevel { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
