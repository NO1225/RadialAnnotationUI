using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BaseWpfCore
{
    public class ContainerViewModel : BaseViewModel
    {

        #region Public Properties

        // The background graphics for the infographic
        public BackgroundRadialGraphicViewModel BackGround { get; set; }

        // The foreground graphics for the infographic
        // Todo: This isn't being used yet, it is converted
        // back to MainBadges currently at the end of the refresh() method
        public ForegroundRadialGraphicViewModel ForeGround { get; set; }

        // The radar image for the background
        public BaseRadialGraphicViewModel Radar { get; set; }

        // The outside ring colored polygons that show glucose readings,
        // Caloric intake and the background is colored to indicate if the 
        // glucose reading is within what range: low, good, high, dangerously
        // high
        // Todo: currently this includes the center circle and also the 
        // 'arm of the clock' graphic.  It should probably be seperated
        // but I didn't want to screw up the wpf ui
        public BaseRadialGraphicViewModel MainBadges { get; set; }

        // The outside LineArcs that represent short term insullin availability
        public BaseRadialGraphicViewModel ShortActings { get; set; }

        // The inside lineArcs that represent long term insullin availability 
        public BaseRadialGraphicViewModel LongActings { get; set; }

        public double RadarDiameter { get; set; } = 120 * 2;

        public double ContainerWidth { get; set; } = 400;

        public double ContainerHeight { get; set; } = 400;

        public double RadarLeft { get; set; } = 50;

        public double RadarRight { get; set; } = 50;

        #endregion

        #region Public Commands

        // The command to generate the infographic
        public ICommand RefreshCommand { get; set; }

        #endregion

        #region Default Constructor

        public ContainerViewModel()
        {

            // The command to generate the infographic
            RefreshCommand = new RelayCommand(Refresh);

            // Calls the refresh method to generate the infographic
            Refresh();
        }

        #endregion

        #region Helping Methods

        /// <summary>
        /// The method to generate all the view models that are used in
        /// the WPF UI controls and pages
        /// </summary>
        private void Refresh()
        {
            // Create a new background property
            BackGround = new BackgroundRadialGraphicViewModel();

            // Add outside ring to background
            AddWhiteOutsideRingToBackground();

            // add radar graphic to background
            AddRadarGraphicToBackground();

            // add foreground stuff to MainBadges
            AddForgroundStuffToMainBadges();
        }

        ///
        /// Add the white ring to be used under the badges
        /// to the background
        /// Todo: This should probably be black
        /// 
        private void AddWhiteOutsideRingToBackground()
        {
            var whiteBackgroundRing = new SolidCompleteRingViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 2,
                NumberOfChildrenInGroup = 1,
                InnerRadius = 121,
                OuterRadius = 169,
                GraphicsColor = BadgeColor.White,
            };

            whiteBackgroundRing.PopulateRadialGraphicSegmentsProperty();

            BackGround.AddGraphics(whiteBackgroundRing);
        }

        ///
        /// Add rador graphic to background
        /// 
        private void AddRadarGraphicToBackground()
        {
            Radar = new BaseRadialGraphicViewModel();

            var crosshairs = new RadialLinesViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 1,
                NumberOfChildrenInGroup = 4,
                ChildClearance = 44,
                GroupClearance = 0,
                InnerRadius = 30,
                OuterRadius = 120,
                FullAngleFrom = -46,
                FullAngleTo = 360 - 46,
                GraphicsColor = BadgeColor.Green,
            };

            crosshairs.PopulateRadialGraphicSegmentsProperty();

            Radar.AddGraphics(crosshairs);

            //BackGround.AddGraphics(crosshairs);

            var innerRadarCircle = new SolidCompleteLineCircleViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,

                InnerRadius = 30,
                OuterRadius = 33,

                GraphicsColor = BadgeColor.Green,
            };

            innerRadarCircle.PopulateRadialGraphicSegmentsProperty();

            Radar.AddGraphics(innerRadarCircle);

            var middleRadarCircle = new SolidCompleteLineCircleViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                InnerRadius = 60,
                OuterRadius = 63,
                GraphicsColor = BadgeColor.Green,
            };

            middleRadarCircle.PopulateRadialGraphicSegmentsProperty();

            Radar.AddGraphics(middleRadarCircle);

            var outsideRadarCircle = new SolidCompleteLineCircleViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                InnerRadius = 90,
                OuterRadius = 93,
                GraphicsColor = BadgeColor.Green,
            };

            outsideRadarCircle.PopulateRadialGraphicSegmentsProperty();

            Radar.AddGraphics(outsideRadarCircle);

            BackGround.AddGraphics(Radar);
        }

        ///
        /// Add Foreground stuff to MainBadges
        /// 
        public void AddForgroundStuffToMainBadges()
        {
            /// 
            /// Create Foreground Graphics
            /// 

            ForeGround = new ForegroundRadialGraphicViewModel();

            MainBadges = new BaseRadialGraphicViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 12,
                NumberOfChildrenInGroup = 5,
                ChildClearance = .2,
                GroupClearance = 1,
                InnerRadius = 120,
                OuterRadius = 170,
                FullAngleFrom = 0,
                FullAngleTo = 360,
                GraphicsColor = (BadgeColor)BadgeColor.Blue,
            };

            MainBadges.PopulateRadialGraphicSegmentsProperty();

            var rand = new Random();

            MainBadges.RadialGraphicSegments.Where(a => a.Angle > 30).ToList().ForEach(a =>
            {
                a.BadgeColor = (BadgeColor)rand.Next(2, 7);
                a.GlucoseLevel = ((int)a.BadgeColor).ToString("F1");
                a.CarbAmount = ((int)a.BadgeColor).ToString();
            }
            );

            ShortActings = new BaseRadialGraphicViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 1,
                NumberOfChildrenInGroup = 2,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 98,
                OuterRadius = 100,
                FullAngleFrom = 0,
                FullAngleTo = 180,
                GraphicsColor = BadgeColor.White,
            };

            ShortActings.PopulateRadialGraphicSegmentsProperty();

            LongActings = new BaseRadialGraphicViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 1,
                NumberOfChildrenInGroup = 2,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 68,
                OuterRadius = 70,
                FullAngleFrom = 30,
                FullAngleTo = 270,
                GraphicsColor = BadgeColor.White,
            };

            LongActings.PopulateRadialGraphicSegmentsProperty();

            ForeGround.AddGraphics(MainBadges);
            ForeGround.AddGraphics(ShortActings);
            ForeGround.AddGraphics(LongActings);

            var mainNeedle = new SolidFilledArcPieFromCenter()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                OuterRadius = 120,
                FullAngleFrom = -1,
                FullAngleTo = 1,
                GraphicsColor = BadgeColor.White,
            };

            mainNeedle.PopulateRadialGraphicSegmentsProperty();

            ForeGround.RadialGraphicSegments.Add(mainNeedle.RadialGraphicSegments.First());

            var centerCircle = new SolidCompleteFilledCircleViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                OuterRadius = 10,
                GraphicsColor = BadgeColor.White,
            };

            centerCircle.PopulateRadialGraphicSegmentsProperty();

            ForeGround.AddGraphics(centerCircle);

            MainBadges = ForeGround;
        }

        /// <summary>
        /// converts the angle from a double to a radian
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        #endregion
    }
}
