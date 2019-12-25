using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class TextOnlyViewModel : BaseRadialGraphicSegmentViewModel
    {
        public string Text { get; set; }

        public new BadgeColor Color { get; set; } = BadgeColor.White;

        public double TextAngle
        {
            get
            {
                if (Angle > 90 && Angle < 270)
                {
                    return 180;
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}
