using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class PizzaSliceFilledViewModel : BaseRadialGraphicViewModel
    {
        public PizzaSliceFilledViewModel()
        {
            NumberOfGroups = 1;
            NumberOfChildrenInGroup = 1;
            ChildClearance = 0;
            GroupClearance = 0;
            InnerRadius = 0;
        }
    }
}
