using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace BaseWpfCore
{
    public class ArcGradialDottedLineWithTextViewModel : BaseRadialGraphicViewModel
    {
        public ArcGradialDottedLineWithTextViewModel()
        {
            NumberOfGroups = 3;

            // TODO:
            // This should be 1 if it's doesn't exceed the 180 degree, otherwise, 2,
            // To be used in short and long acting, it's expected to pass 180 degree
            NumberOfChildrenInGroup = 3;

            ChildClearance = 0;
            GroupClearance = 0;
        }

        public string Amount { get; set; }

        private double textSpan = 20;

        public override void PopulateRadialGraphicSegmentsProperty()
        {
            PopulateTheArcLine();

            var text = new TextOnlyViewModel()
            {
                Angle = this.FullAngleFrom - textSpan / 2,
                CenterX = this.childCenterX,
                CenterY = this.childCenterY,
                Color = this.GraphicsColor,
                Height = 20,
                Width = 20,
                Left = this.childLeft,
                Top = this.childTop,
                Text = Amount,
            };

            this.RadialGraphicSegments.Add(text);  
            
            var icon = new IconOnlyViewModel()
            {
                Angle = this.FullAngleTo + textSpan / 2,
                CenterX = this.childCenterX,
                CenterY = this.childCenterY,
                Color = this.GraphicsColor,
                Height = 20,
                Width = 20,
                Left = this.childLeft,
                Top = this.childTop,
                Text = "\uf0f3",
            };

            this.RadialGraphicSegments.Add(icon);
        }

        private void PopulateTheArcLine()
        {
            // Initiating the annotation
            RadialGraphicSegments = new ObservableCollection<BaseRadialGraphicSegmentViewModel>();

            /// The number of degrees each group will extend through after subtracting the
            /// group clearance before and after the group
            groupAngleSpan = ((FullAngleTo - FullAngleFrom) / NumberOfGroups) - GroupClearance * 2;

            childCenterY = OuterRadius;

            childTop = (ContainerHeight - OuterRadius * 2) / 2;

            childSizes = new ObservableCollection<Size>()
            {
                new Size(InnerRadius,InnerRadius),
                new Size(OuterRadius,OuterRadius)
            };

            CalculateDimension();

            // Giving values to the annotation, a minute for every 6 degrees 
            for (double i = FullAngleFrom; i < FullAngleFrom+ (FullAngleTo-FullAngleFrom)/3; i += groupAngleSpan + 2 * GroupClearance)
            {
                for (double j = i; j - (i + groupAngleSpan) < -.001; j += childAngleSpan + 2 * ChildClearance)
                {
                    // Todo: change graphic color below for badge color
                    RadialGraphicSegments.Add(
                        new BaseArcRadialSegmentViewModel(
                            angle: j + childAngleSpan / 2 + ChildClearance,
                            //text: (j / 6).ToString(),
                            text: null,
                            childWidth,
                            childHeight,
                            childCenterX,
                            childCenterY,
                            childLeft,
                            childTop,
                            childPoints,
                            childSizes,
                            GraphicsColor
                            )
                        );
                }
            }

            ChildClearance = 1;

            CalculateDimension();

            // Giving values to the annotation, a minute for every 6 degrees 
            for (double i = FullAngleFrom + (FullAngleTo - FullAngleFrom) / 3; i <  FullAngleFrom + 2 * (FullAngleTo - FullAngleFrom) / 3; i += groupAngleSpan + 2 * GroupClearance)
            {
                for (double j = i; j - (i + groupAngleSpan) < -.001; j += childAngleSpan + 2 * ChildClearance)
                {
                    // Todo: change graphic color below for badge color
                    RadialGraphicSegments.Add(
                        new BaseArcRadialSegmentViewModel(
                            angle: j + childAngleSpan / 2 + ChildClearance,
                            //text: (j / 6).ToString(),
                            text: null,
                            childWidth,
                            childHeight,
                            childCenterX,
                            childCenterY,
                            childLeft,
                            childTop,
                            childPoints,
                            childSizes,
                            GraphicsColor
                            )
                        );
                }
            }
            
            ChildClearance = 2;

            CalculateDimension();

            // Giving values to the annotation, a minute for every 6 degrees 
            for (double i = FullAngleFrom + 2 * (FullAngleTo - FullAngleFrom) / 3; i < FullAngleTo; i += groupAngleSpan + 2 * GroupClearance)
            {
                for (double j = i; j - (i + groupAngleSpan) < -.001; j += childAngleSpan + 2 * ChildClearance)
                {
                    // Todo: change graphic color below for badge color
                    RadialGraphicSegments.Add(
                        new BaseArcRadialSegmentViewModel(
                            angle: j + childAngleSpan / 2 + ChildClearance,
                            //text: (j / 6).ToString(),
                            text: null,
                            childWidth,
                            childHeight,
                            childCenterX,
                            childCenterY,
                            childLeft,
                            childTop,
                            childPoints,
                            childSizes,
                            GraphicsColor
                            )
                        );
                }
            }
        }

        private void CalculateDimension()
        {
            /// The number of degrees each group child will exted through after subtracting 
            /// the child clearance before and after the child
            childAngleSpan = (groupAngleSpan / NumberOfChildrenInGroup) - ChildClearance * 2;

            var AdjacentLinePercentOfHypotenuse = Math.Cos(DegreeToRadian(childAngleSpan));

            childWidth = Math.Sqrt(2 * Math.Pow(OuterRadius, 2) - 2 * Math.Pow(OuterRadius, 2) * AdjacentLinePercentOfHypotenuse);

            var dChild = Math.Sqrt(2 * Math.Pow(InnerRadius, 2) - 2 * Math.Pow(InnerRadius, 2) * AdjacentLinePercentOfHypotenuse);

            childHeight = (OuterRadius - InnerRadius) + dChild / (2 * Math.Tan(DegreeToRadian(180 - childAngleSpan / 2) / 2));

            childCenterX = childWidth / 2;

            childLeft = ContainerWidth / 2 - childCenterX;

            var dOuterChild = Math.Sqrt(2 * Math.Pow(OuterRadius, 2) - 2 * Math.Pow(OuterRadius, 2) * Math.Cos(DegreeToRadian(childAngleSpan)));

            var cChild = dOuterChild / (2 * Math.Tan(DegreeToRadian(180 - childAngleSpan / 2) / 2));

            childPoints = new ObservableCollection<Point>()
            {
                new Point(0, cChild),
                new Point(
                    (childWidth-dChild)/2,
                    childHeight),
                new Point(
                    childWidth-(childWidth-dChild)/2,
                    childHeight),
                new Point(
                    childWidth,
                    cChild),
                new Point(0,cChild),
            };
        }
    }
}
