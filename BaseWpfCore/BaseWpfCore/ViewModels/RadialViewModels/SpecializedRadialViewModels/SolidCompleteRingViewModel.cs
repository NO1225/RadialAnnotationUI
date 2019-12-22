using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class SolidCompleteRingViewModel : BaseRadialGraphicViewModel
    {

        public SolidCompleteRingViewModel()
        {
            ChildClearance = 0;
            GroupClearance = 0;
            FullAngleFrom = 0;
            FullAngleTo = 360;
        }
    }
}
