using System.Collections.Generic;
using System.Linq;

namespace BaseWpfCore
{
    public class RadialPieChartViewModel : BaseRadialGraphicViewModel
    {
        public class RadialPieChartItemViewModel
        {
            public string Text { get; set; }

            public decimal Value { get; set; }
        }

        public List<RadialPieChartItemViewModel> Items { get; set; }

        public bool RandomColors { get; set; } = true;

        public RadialPieChartViewModel()
        {
            this.ChildClearance = 0;
            this.NumberOfChildrenInGroup = 0;
            this.NumberOfGroups = 0;
            //ContainerHeight = this.ContainerHeight;
            //ContainerWidth = this.ContainerWidth;
            //NumberOfGroups = 1;
            //NumberOfChildrenInGroup = 2;
            //ChildClearance = -1;
            //GroupClearance = 0;
            //InnerRadius = 120;
            //OuterRadius = 170;
            //FullAngleFrom = 90;
            //FullAngleTo = 360;
            //GraphicsColor = (BadgeColor)BadgeColor.Pink;
        }

        public override void PopulateRadialGraphicSegmentsProperty()
        {
            var dum = new BaseRadialGraphicViewModel()
            {
                ChildClearance = 0,
                NumberOfChildrenInGroup = 0,
                NumberOfGroups = 0,
                OuterRadius = (this.InnerRadius + this.OuterRadius) * .75,

            };
            dum.PopulateRadialGraphicSegmentsProperty();

            if (Items?.Count > 0)
            {
                var total = Items.Sum(i => i.Value);
                double currentAngle = 0;
                var currentColor = BadgeColor.Yellow;
                foreach (var item in Items)
                {
                    var newAngle = currentAngle + 360 * (double)(item.Value / total);
                    var graph = new BaseRadialGraphicViewModel()
                    {
                        ContainerHeight = this.ContainerHeight,
                        ContainerWidth = this.ContainerWidth,
                        NumberOfGroups = 1,
                        NumberOfChildrenInGroup = 2,
                        ChildClearance = -1,
                        GroupClearance = 0,
                        InnerRadius = this.InnerRadius,
                        OuterRadius = this.OuterRadius,
                        FullAngleFrom = currentAngle,
                        FullAngleTo = newAngle,
                        GraphicsColor = currentColor++,
                    };

                    graph.PopulateRadialGraphicSegmentsProperty();

                    this.AddGraphics(graph);

                    var text = new TextOnlyViewModel()
                    {
                        Angle = (currentAngle + newAngle) / 2,
                        CenterX = dum.childCenterX,
                        CenterY = dum.childCenterY,
                        Color = BadgeColor.White,
                        Height = 20,
                        Width = 20,
                        Left = dum.childLeft,
                        Top = dum.childTop,
                        Text = item.Text,
                    };

                    RadialGraphicSegments.Add(text);

                    currentAngle = newAngle;
                }
            }

            //base.PopulateRadialGraphicSegmentsProperty();
        }
    }

}
