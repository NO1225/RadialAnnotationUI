using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    public class TextOnlyViewModel : BaseRadialGraphicSegmentViewModel
    {
        public string Text { get; set; }

        public new BadgeColor Color { get; set; } = BadgeColor.White;

    }
}
