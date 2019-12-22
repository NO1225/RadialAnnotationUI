using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class SolidFilledArcPieFromCenter : BaseRadialGraphicViewModel
    {
        public SolidFilledArcPieFromCenter()
        {
            NumberOfGroups = 1;
            NumberOfChildrenInGroup = 1;
            ChildClearance = 0;
                GroupClearance = 0;
            InnerRadius = 0;
        }
    }
}
