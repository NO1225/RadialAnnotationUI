using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseWpfCore
{
    public class ArcLineViewModel : BaseRadialGraphicViewModel
    {
        public ArcLineViewModel()
        {
            NumberOfGroups = 1;

            // TODO:
            // This should be 1 if it's doesn't exceed the 180 degree, otherwise, 2,
            // To be used in short and long acting, it's expected to pass 180 degree
            NumberOfChildrenInGroup = 2;

            ChildClearance = 0;
            GroupClearance = 0;
        }
    }
}
