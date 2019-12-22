using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class RingFullFilledViewModel : BaseRadialGraphicViewModel
    {

        public RingFullFilledViewModel()
        {
            ChildClearance = 0;
            GroupClearance = 0;
            FullAngleFrom = 0;
            FullAngleTo = 360;
        }
    }
}
