using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BaseWpfCore
{
    public class BaseRadialGraphicViewModel : BaseViewModel
    {

        #region Public Properties

        /// <summary>
        /// The annotation around the handle
        /// </summary>
        public ObservableCollection<BaseRadialGraphicSegmentViewModel> RadialGraphicSegments { get; set; }

        public double ContainerWidth { get; set; } = 400;

        public double ContainerHeight { get; set; } = 400;

        public double OuterRadius { get; set; } = 170;

        public double InnerRadius { get; set; } = 120;

        public double FullAngleFrom { get; set; } = 0;

        public double FullAngleTo { get; set; } = 360;

        public int NumberOfGroups { get; set; } = 12;

        public double GroupClearance { get; set; } = 1;

        public double ChildClearance { get; set; } = 0;

        public int NumberOfChildrenInGroup { get; set; } = 5;

        public BadgeColor GraphicsColor { get; set; }

        #endregion

        #region Private Members

        private double groupAngleSpan;

        private double childAngleSpan;

        private double childWidth;

        private double childHeight;

        private double childCenterX;

        private double childCenterY;

        private double childLeft;

        private double childTop;

        private ObservableCollection<Point> childPoints;

        private ObservableCollection<Size> childSizes;

        #endregion

        #region Public Commands

        public ICommand PopulateRadialGraphicSegmentsPropertyCommand { get; set; }

        #endregion

        #region Default Constructor

        public BaseRadialGraphicViewModel()
        {

            PopulateRadialGraphicSegmentsPropertyCommand = 
                new RelayCommand(PopulateRadialGraphicSegmentsProperty);

            PopulateRadialGraphicSegmentsProperty();
        }

        #endregion

        public void AddGraphics(BaseRadialGraphicViewModel graphics)
        {
            foreach (BaseRadialGraphicSegmentViewModel graphic in graphics.RadialGraphicSegments)
            {
                RadialGraphicSegments.Add(graphic);
            }
        }

        #region Helping Methods

        public void PopulateRadialGraphicSegmentsProperty()
        {
            // Initiating the annotation
            RadialGraphicSegments = new ObservableCollection<BaseRadialGraphicSegmentViewModel>();

            /// The number of degrees each group will extend through after subtracting the
            /// group clearance before and after the group
            groupAngleSpan = ((FullAngleTo - FullAngleFrom) / NumberOfGroups) - GroupClearance * 2;

            /// The number of degrees each group child will exted through after subtracting 
            /// the child clearance before and after the child
            childAngleSpan = (groupAngleSpan / NumberOfChildrenInGroup) - ChildClearance * 2;

            var AdjacentLinePercentOfHypotenuse = Math.Cos(DegreeToRadian(childAngleSpan));

            childWidth = Math.Sqrt(2 * Math.Pow(OuterRadius, 2) - 2 * Math.Pow(OuterRadius, 2) * AdjacentLinePercentOfHypotenuse);

            var dChild = Math.Sqrt(2 * Math.Pow(InnerRadius, 2) - 2 * Math.Pow(InnerRadius, 2) * AdjacentLinePercentOfHypotenuse);

            childHeight = (OuterRadius - InnerRadius) + dChild / (2 * Math.Tan(DegreeToRadian(180 - childAngleSpan / 2) / 2));

            childCenterX = childWidth / 2;

            childCenterY = OuterRadius;

            childLeft = ContainerWidth / 2 - childCenterX;

            childTop = (ContainerHeight - OuterRadius * 2) / 2;

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

            childSizes = new ObservableCollection<Size>()
            {
                new Size(InnerRadius,InnerRadius),
                new Size(OuterRadius,OuterRadius)
            };

            // Giving values to the annotation, a minute for every 6 degrees 
            for (double i = FullAngleFrom; i < FullAngleTo; i += groupAngleSpan + 2 * GroupClearance)
            {
                for (double j = i; j - (i + groupAngleSpan) < -.001; j += childAngleSpan + 2 * ChildClearance)
                {
                    // Todo: change graphic color below for badge color
                    RadialGraphicSegments.Add(
                        new BaseRadialGraphicSegmentViewModel(
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

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        #endregion
    }
}
