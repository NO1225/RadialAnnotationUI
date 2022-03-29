using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    /// <summary>
    ///  The outline/circumference of a circle... not filled or solid
    /// </summary>
    public class CircleFullLineViewModel : BaseRadialGraphicViewModel
    {
        /// <summary>
        /// An unfilled circle with a line thickness determined 
        /// by the difference between the inside and outside radii.
        /// argueably... a skinny 'RingFullFilledViewModel'.
                /// </summary>
        public CircleFullLineViewModel()
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
