using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class CarbIntakeEntryDataModel
    {

        public int CarbIntakeLevel { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public CarbIntakeEntryDataModel()
        {
            //Duration = TimeSpan.FromHours()
        }
    }
}
