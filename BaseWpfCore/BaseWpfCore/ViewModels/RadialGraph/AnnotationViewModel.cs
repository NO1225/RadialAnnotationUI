using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace BaseWpfCore
{
    public class AnnotationViewModel
    {
        #region Public Properties

        #region UI

        public double Width { get; set; }
        public double Height { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public ObservableCollection<Point> Points { get; set; }
        public ObservableCollection<Size> Sizes { get; set; }

        #endregion

        /// <summary>
        /// The text that should be shown in this annotation
        /// </summary>
        public string GlucoseLevel { get; set; }

        public string CarbAmount { get; set; }

        /// <summary>
        /// The angle at which this annotation should be ratated
        /// </summary>
        public double Angle { get; private set; }

        public double GlucoseTextAngle { get
            {
                if(Angle>0&&Angle<180)
                {
                    return 270;
                }
                else
                {
                    return 90;
                }
            } 
        }

        public double CarbTextAngle { get
            {
                if(Angle>90&&Angle<270)
                {
                    return 180;
                }
                else
                {
                    return 0;
                }
            } 
        }

        public string ActingAmount { get; set; }

        public double AcringTextAngle { get; set; }

        public BadgeColor BadgeColor { get; set; } 

        #endregion

        #region Default Contructor

        public AnnotationViewModel(
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
            BadgeColor = badgeColor;
        }

        #endregion

    }
}
