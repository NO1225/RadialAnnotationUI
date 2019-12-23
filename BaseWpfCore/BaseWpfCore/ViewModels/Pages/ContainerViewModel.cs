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

        #region Private properties

        DateTime mCurrentDateToShow;

        AMPMEnum mMorningOrNight;

        #endregion

        #region Public Properties

        public string DateTimePrettyText { get; private set; }

        public DateTime CurrentDateToShow
        {
            get
            {
                return mCurrentDateToShow;
            }
            set
            {
                if (value != null)
                {
                    mCurrentDateToShow = value;
                    DateTimePrettyText = mCurrentDateToShow.ToString("MMM dd yyyy") + " " + MorningOrNight.ToString();
                    Refresh();
                }
                else mCurrentDateToShow = DateTime.Now;
            }
        }

        public AMPMEnum MorningOrNight
        {
            get
            {
                return mMorningOrNight;
            }
            set
            {
                mMorningOrNight = value;
                if (mMorningOrNight == AMPMEnum.AM)
                {
                    InfographicStartTime = new DateTime(2019, 12, 12, 0, 0, 0);
                }
                else InfographicStartTime = new DateTime(2019, 12, 12, 12, 0, 0);
                DateTimePrettyText = mCurrentDateToShow.ToString("MMM dd yyyy") + " " + MorningOrNight.ToString();
                   
                Refresh();
            }
        }

        public UserRecordingsDataModel UserRecordings { get; set; }

        public DateTime InfographicStartTime { get; set; }

        // The background graphics for the infographic
        public BackgroundRadialGraphicViewModel BackGround { get; set; }

        // The foreground graphics for the infographic
        // Todo: This isn't being used yet, it is converted
        // back to MainBadges currently at the end of the refresh() method
        public ForegroundRadialGraphicViewModel ForeGround { get; set; }

        // The radar image for the background
        public BaseRadialGraphicViewModel RadarGraphic { get; set; }

        // The outside ring colored polygons that show glucose Recordings,
        // Caloric intake and the background is colored to indicate if the 
        // glucose Recording is within what range: low, good, high, dangerously
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

        /// <summary>
        /// A command that toggles the AM and PM button
        /// on the main infographic
        /// </summary>
        public ICommand ToggleAmAndPmCommand { get; set; }

        /// <summary>
        /// command to change the infographic back 12 hours
        /// </summary>
        public ICommand GoBack12HoursCommand { get; set; }

        /// <summary>
        /// command to change the infographic forward 12 hours
        /// </summary>
        public ICommand GoForward12HoursCommand { get; set; }

        #endregion

        #region Default Constructor

        public ContainerViewModel()
        {
            MorningOrNight = AMPMEnum.PM;

            UserRecordings = new UserRecordingsDataModel();

            // The command to generate the infographic
            RefreshCommand = new RelayCommand(Refresh);

            ToggleAmAndPmCommand = new RelayCommand(ToggleAmAndPm);

            GoBack12HoursCommand = new RelayCommand(GoBack12Hours);

            GoForward12HoursCommand = new RelayCommand(GoForward12Hours);

            CurrentDateToShow = DateTime.Now;

            

            // Calls the refresh method to generate the infographic
            Refresh();
        }

        #endregion

        #region Helping Methods

        public void GoBack12Hours()
        {
            if (MorningOrNight == AMPMEnum.AM)
            {
                CurrentDateToShow = CurrentDateToShow.Subtract(new TimeSpan(24, 0, 0));
            }
            ToggleAmAndPm();
            Refresh();
        }

        public void GoForward12Hours()
        {
            if (MorningOrNight == AMPMEnum.PM)
            {
                CurrentDateToShow = CurrentDateToShow.AddDays(1);
            }
            ToggleAmAndPm();
            Refresh();
        }

        public void ToggleAmAndPm()
        {
            // ToDo: I know there is a better way to do this

            if (MorningOrNight == AMPMEnum.AM) { MorningOrNight = AMPMEnum.PM; }
            else { MorningOrNight = AMPMEnum.AM; }
        }

        /// <summary>
        /// The method to generate all the view models that are used in
        /// the WPF UI controls and pages
        /// </summary>
        private void Refresh()
        {
            UserRecordings = new UserRecordingsDataModel();


            // Create a new background property
            BackGround = new BackgroundRadialGraphicViewModel();

            // Add outside ring to background
            AddWhiteOutsideRingToBackground();

            // add radar graphic to background
            AddRadarGraphicToBackground();

            // add foreground stuff to MainBadges
            AddForgroundGraphicStuff();
        }

        ///
        /// Method to: Add the white ring to be used under the badges
        /// to the background
        /// Todo: This should probably be black
        /// 
        private void AddWhiteOutsideRingToBackground()
        {
            // create a ring given the inner and out radii and assign a color
            var whiteBackgroundRing = new RingFullFilledViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 2,
                NumberOfChildrenInGroup = 1,
                InnerRadius = 121,
                OuterRadius = 169,
                GraphicsColor = BadgeColor.White,
            };

            // Populate the ring by adding each ring segment to each other 
            // with varying x and y values
            whiteBackgroundRing.PopulateRadialGraphicSegmentsProperty();

            // Add the Ring to the Background
            BackGround.AddGraphics(whiteBackgroundRing);
        }

        ///
        /// Method to: Add rador graphic to background
        /// 
        private void AddRadarGraphicToBackground()
        {
            // create new radar graphic based on the base circular 
            // graphic view model
            RadarGraphic = new BaseRadialGraphicViewModel();

            // Create crosshairs graphic out of RadialLines view model
            var crosshairsGraphic = new RadialLinesViewModel()
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

            // generate graphic items for crosshairs
            crosshairsGraphic.PopulateRadialGraphicSegmentsProperty();

            // Add crosshairs graphic to radar graphics
            RadarGraphic.AddGraphics(crosshairsGraphic);

            // generate graphic for innermost circle of the radar
            var innerRadarCircle = new CircleFullLineViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,

                InnerRadius = 30,
                OuterRadius = 33,

                GraphicsColor = BadgeColor.Green,
            };

            // populate the pieces to build the graphic
            innerRadarCircle.PopulateRadialGraphicSegmentsProperty();

            // add the innerRadarCircle to the RadarGraphic
            RadarGraphic.AddGraphics(innerRadarCircle);

            // generate graphic for middle circle of the radar
            var middleRadarCircle = new CircleFullLineViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                InnerRadius = 60,
                OuterRadius = 63,
                GraphicsColor = BadgeColor.Green,
            };

            // populate the pieces to build the graphic
            middleRadarCircle.PopulateRadialGraphicSegmentsProperty();

            // add the middleRadarCircle to the RadarGraphic
            RadarGraphic.AddGraphics(middleRadarCircle);

            // generate graphic for outermost circle of the radar
            var outsideRadarCircle = new CircleFullLineViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                InnerRadius = 90,
                OuterRadius = 93,
                GraphicsColor = BadgeColor.Green,
            };

            // populate the pieces to build the graphic
            outsideRadarCircle.PopulateRadialGraphicSegmentsProperty();

            // add the outesideRadarCircle to the RadarGraphic
            RadarGraphic.AddGraphics(outsideRadarCircle);

            // add the complete radar graphic to the background
            BackGround.AddGraphics(RadarGraphic);
        }

        ///
        /// Add Foreground stuff to MainBadges
        /// 
        public void AddForgroundGraphicStuff()
        {
            /// 
            /// Create Foreground Graphics
            /// ToDo: this isn't used. I convert it back to a MainBadges
            /// 
            ForeGround = new ForegroundRadialGraphicViewModel();

            /// Add the containers for the glucose and carb intake Recordings
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

            // populate the pieces to build the graphic
            MainBadges.PopulateRadialGraphicSegmentsProperty();

            PopulateBadgesWithGlucoseRecordings(MainBadges);

            // Generate random glucose levels, carb intake levels
            // and colors for the container fill
            var rand = new Random();

            MainBadges.RadialGraphicSegments.Where(a => a.Angle > 30).ToList().ForEach(a =>
            {
                a.BadgeColor = (BadgeColor)rand.Next(2, 7);
                a.GlucoseLevel = ((int)a.BadgeColor).ToString("F1");
                a.CarbAmount = ((int)a.BadgeColor).ToString();
            }
            );

            PopulateBadgesWithGlucoseRecordings(MainBadges);

            /////
            ///// Add the hour time stamps to the infographic
            ///// 
            //var HourContainers = new HourContainerViewModel()
            //{
            //    ContainerHeight = this.ContainerHeight,
            //    ContainerWidth = this.ContainerWidth,
            //    NumberOfGroups = 1,
            //    NumberOfChildrenInGroup = 12,
            //    ChildClearance = 10,
            //    GroupClearance = 0,
            //    InnerRadius = 200,
            //    OuterRadius = 220,
            //    FullAngleFrom = 20,
            //    FullAngleTo = 360,
            //    GraphicsColor = (BadgeColor)BadgeColor.Blue,
            //};

            // populate the pieces to build the graphic
            //HourContainers.PopulateRadialGraphicSegmentsProperty();

            //MainBadges.AddGraphics(HourContainers);

            ///
            /// Start the Short Acting Lines added to the infographic
            /// 
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
                GraphicsColor = BadgeColor.Red,
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

            var mainNeedle = new PizzaSliceFilledViewModel()
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

            var centerCircle = new CircleFullFilledViewModel()
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


        public void PopulateBadgesWithGlucoseRecordings(BaseRadialGraphicViewModel mainBadges)
        {
            
            int starttime = InfographicStartTime.Hour * 60;

            

            foreach (BaseRadialGraphicSegmentViewModel timeSegment in mainBadges.RadialGraphicSegments)
            {
                var glucoseMatch = false;
                foreach (GlucoseLevelRecordingDataModel glucoseRecording in UserRecordings.GlucoseLevelRecordings)
                {
                    // check to see if the date matches 
                    if (glucoseRecording.StartTime.Date == CurrentDateToShow.Date)
                    {
                        int glucoseTime = glucoseRecording.StartTime.Hour * 60 + glucoseRecording.StartTime.Minute;
                        if (glucoseTime >= starttime && glucoseTime < starttime + 12)
                        {
                            decimal gl = (decimal)glucoseRecording.GlucoseLevel / 10;
                            timeSegment.GlucoseLevel = string.Format("{0:F1}", gl);
                            glucoseMatch = true;
                        }
                    }
                    
                }
                if (!glucoseMatch)
                {
                    timeSegment.BadgeColor = BadgeColor.White;
                    timeSegment.GlucoseLevel = "";
                }

                var carbMatch = false;
                foreach (CarbIntakeRecordingDataModel carbRecording in UserRecordings.CarbIntakeRecordings)
                {
                    // check to see if the date matches 
                    if (carbRecording.StartTime.Date == CurrentDateToShow.Date)
                    {
                        int carbTime = carbRecording.StartTime.Hour * 60 + carbRecording.StartTime.Minute;
                        if (carbTime >= starttime && carbTime < starttime + 12)
                        {
                            timeSegment.CarbAmount = carbRecording.CarbIntakeAmount.ToString();
                            carbMatch = true;
                        }
                    }

                }
                if (!carbMatch)
                {
                    timeSegment.CarbAmount = "";
                }

                starttime = starttime + 12;
            }
        }
        #endregion
    }
}
