using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BaseWpfCore
{
    public class BaseGraphicViewModel : BaseViewModel
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

        protected double groupAngleSpan;

        protected double childAngleSpan;

        protected double childWidth;

        protected double childHeight;

        protected double childCenterX;

        protected double childCenterY;

        protected double childLeft;

        protected double childTop;

        #endregion
    }
}
