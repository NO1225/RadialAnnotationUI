using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace BaseWpfCore
{
    public class BaseArcRadialSegmentViewModel : BaseRadialGraphicSegmentViewModel
    {
        #region Public Properties

        #region UI

        public ObservableCollection<Point> Points { get; set; }
        public ObservableCollection<Size> Sizes { get; set; }

        #endregion

        /// <summary>
        /// The text that should be shown in this annotation
        /// </summary>
        public string GlucoseLevel { get; set; }

        public string CarbAmount { get; set; }

        public string HourText { get; set; }

        public double GlucoseTextAngle
        {
            get
            {
                if (Angle > 0 && Angle < 180)
                {
                    return 270;
                }
                else
                {
                    return 90;
                }
            }
        }

        public double CarbTextAngle
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

        #endregion

        #region Default Contructor

        public BaseArcRadialSegmentViewModel(
            double angle,
            string text,
            double width,
            double height,
            double centerX,
            double centerY,
            double left,
            double top,
            ObservableCollection<Point> points,
            ObservableCollection<Size> sizes,
            BadgeColor badgeColor)
        {
            Angle = angle;
            GlucoseLevel = text;
            Width = width;
            Height = height;
            CenterX = centerX;
            CenterY = centerY;
            Left = left;
            Top = top;
            Points = points;
            Sizes = sizes;
            Color = badgeColor;
        }

        #endregion

    }
}
