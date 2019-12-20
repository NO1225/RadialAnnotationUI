using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BaseWpfCore
{
    public class RadialAnnotationContainerViewModel : BaseViewModel
    {

        #region Public Properties

        /// <summary>
        /// The annotation around the handle
        /// </summary>
        public ObservableCollection<AnnotationViewModel> Annotations { get; set; }

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

        public BadgeColor BadgeColor { get; set; }

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

        public ICommand RefreshCommand { get; set; }

        #endregion

        #region Default Constructor

        public RadialAnnotationContainerViewModel()
        {

            RefreshCommand = new RelayCommand(Refresh);

            Refresh();
        }

        #endregion

        #region Helping Methods

        public void Refresh()
        {
            // Initiating the annotation
            Annotations = new ObservableCollection<AnnotationViewModel>();

            this.groupAngleSpan = ((this.FullAngleTo - this.FullAngleFrom) / this.NumberOfGroups) - this.GroupClearance * 2;

            this.childAngleSpan = (this.groupAngleSpan / this.NumberOfChildrenInGroup) - this.ChildClearance * 2;

            this.childWidth = Math.Sqrt(2 * Math.Pow(this.OuterRadius, 2) - 2 * Math.Pow(this.OuterRadius, 2) * Math.Cos(DegreeToRadian(this.childAngleSpan)));

            var dChild = Math.Sqrt(2 * Math.Pow(this.InnerRadius, 2) - 2 * Math.Pow(this.InnerRadius, 2) * Math.Cos(DegreeToRadian(this.childAngleSpan)));

            this.childHeight = (this.OuterRadius - this.InnerRadius) + dChild / (2 * Math.Tan(DegreeToRadian(180 - this.childAngleSpan / 2) / 2));

            this.childCenterX = this.childWidth / 2;

            this.childCenterY = this.OuterRadius;

            this.childLeft = this.ContainerWidth / 2 - this.childWidth / 2;

            this.childTop = (this.ContainerHeight - this.OuterRadius * 2) / 2;

            var dOuterChild = Math.Sqrt(2 * Math.Pow(this.OuterRadius, 2) - 2 * Math.Pow(this.OuterRadius, 2) * Math.Cos(DegreeToRadian(this.childAngleSpan)));

            var cChild = dOuterChild / (2 * Math.Tan(DegreeToRadian(180 - this.childAngleSpan / 2) / 2));

            this.childPoints = new ObservableCollection<Point>()
            {
                new Point(0, cChild),
                new Point(
                    (this.childWidth-dChild)/2,
                    this.childHeight),
                new Point(
                    this.childWidth-(this.childWidth-dChild)/2,
                    this.childHeight),
                new Point(
                    this.childWidth,
                    cChild),
                new Point(0,cChild),
            };

            this.childSizes = new ObservableCollection<Size>()
            {
                new Size(this.InnerRadius,this.InnerRadius),
                new Size(this.OuterRadius,this.OuterRadius)
            };

            // Giving values to the annotation, a minute for every 6 degrees 
            for (double i = this.FullAngleFrom; i < this.FullAngleTo; i += this.groupAngleSpan + 2 * this.GroupClearance)
            {
                for (double j = i; j - (i + this.groupAngleSpan) < -.001; j += this.childAngleSpan + 2 * this.ChildClearance)
                {
                    Annotations.Add(
                        new AnnotationViewModel(
                            angle: j + this.childAngleSpan / 2 + this.ChildClearance,
                            //text: (j / 6).ToString(),
                            text: null,
                            this.childWidth,
                            this.childHeight,
                            this.childCenterX,
                            this.childCenterY,
                            this.childLeft,
                            this.childTop,
                            this.childPoints,
                            this.childSizes,
                            this.BadgeColor
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
