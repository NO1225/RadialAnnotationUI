using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{ 
    public class HourContainerViewModel : BaseRadialGraphicViewModel
    {
        public AMPMEnum AMPM { get; set; }

        public HourContainerViewModel()
        {
            this.ChildClearance = 0;
            this.NumberOfChildrenInGroup = 0;
            this.NumberOfGroups = 0;
        }

        public override void PopulateRadialGraphicSegmentsProperty()
        {
            base.PopulateRadialGraphicSegmentsProperty();

            var hours = new List<int>() {
                1,
                2,
                4,
                5,
                7,
                8,
                10,
                11,
            };

            if (AMPM == AMPMEnum.PM)
                hours.ForEach(h => h += 12);

            foreach (var hour in hours)
            {
                RadialGraphicSegments.Add(new TextOnlyViewModel()
                {
                    Angle = (hour%12 * 30)-4,
                    CenterX = this.childCenterX,
                    CenterY = this.childCenterY,
                    Color = GraphicsColor,
                    Height = 20,
                    Width = 20,
                    Left = this.childLeft,
                    Top = this.childTop,
                    Text = hour.ToString(),
                });
            }
        }
    }
}
