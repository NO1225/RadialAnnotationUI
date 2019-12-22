using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class SolidCompleteLineCircleViewModel : BaseRadialGraphicViewModel
    {
        public SolidCompleteLineCircleViewModel()
        {
            NumberOfGroups = 1;
            NumberOfChildrenInGroup = 2;
            ChildClearance = 0;
            GroupClearance = 0;
            FullAngleFrom = 0;
            FullAngleTo = 360;
        }
    }
}
