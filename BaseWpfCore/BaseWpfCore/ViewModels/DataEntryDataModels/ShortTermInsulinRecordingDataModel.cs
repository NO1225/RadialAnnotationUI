using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore.ViewModels.DataEntryDataModels
{
    public class ShortTermInsulinEntryDataModel
    {
        public int Quantity { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
