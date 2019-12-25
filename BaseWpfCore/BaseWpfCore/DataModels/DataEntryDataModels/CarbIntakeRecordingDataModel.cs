using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class CarbIntakeRecordingDataModel
    {

        public int CarbIntakeAmount { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public CarbIntakeRecordingDataModel()
        {
            //Duration = TimeSpan.FromHours()
        }
    }
}
