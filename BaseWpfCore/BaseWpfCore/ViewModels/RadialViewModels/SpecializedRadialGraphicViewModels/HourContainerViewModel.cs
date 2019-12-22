using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{ 
    public class HourContainerViewModel : BaseRadialGraphicViewModel
    {
        public List<string> Hours { get; set; } = new List<string>();

        public HourContainerViewModel()
        {
            for (int i = 1; i <= 12; i++)
            {
                Hours.Add(i.ToString());
            }
        }
    }
}
