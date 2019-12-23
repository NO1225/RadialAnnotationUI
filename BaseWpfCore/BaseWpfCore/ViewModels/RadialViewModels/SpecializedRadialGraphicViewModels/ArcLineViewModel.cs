using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class ArcLineViewModel : BaseRadialGraphicViewModel
    {
        public ArcLineViewModel()
        {
            NumberOfGroups = 1;
            // TODO:should NumberOfChildrenInGroup be 1 or is that 
            // only used if it extends past 180 degrees
            NumberOfChildrenInGroup = 2;
            ChildClearance = 0;
            GroupClearance = 0;
                    }
    }
}
