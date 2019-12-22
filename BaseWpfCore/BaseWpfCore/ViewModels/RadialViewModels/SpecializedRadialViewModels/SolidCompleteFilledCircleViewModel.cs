using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class SolidCompleteFilledCircleViewModel : BaseRadialGraphicViewModel
    {
        public SolidCompleteFilledCircleViewModel()
        {
            NumberOfGroups = 1;
            NumberOfChildrenInGroup = 2;
            ChildClearance = 0;
            GroupClearance = 0;
            InnerRadius = 0;
            FullAngleFrom = 0;
            FullAngleTo = 360;
        }
    }
}
