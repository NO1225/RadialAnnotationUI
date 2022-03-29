using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class CircleFullFilledViewModel : BaseRadialGraphicViewModel
    {
        public CircleFullFilledViewModel()
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
