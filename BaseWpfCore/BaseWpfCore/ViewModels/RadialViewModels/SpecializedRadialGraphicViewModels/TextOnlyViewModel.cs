using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class TextOnlyViewModel : BaseRadialGraphicSegmentViewModel
    {
        public string Text { get; set; }

        public new BadgeColor Color { get; set; } = BadgeColor.White;

        public bool Horizontal { get; set; }

        public double TextAngle
        {
            get
            {
                if (Angle > 0 && Angle < 180)
                {
                    return Horizontal? 180:270;
                }
                else
                {
                    return Horizontal ? 0 : 90;
                }
            }
        }
        public TextOnlyViewModel()
        {

        }
    }
}
