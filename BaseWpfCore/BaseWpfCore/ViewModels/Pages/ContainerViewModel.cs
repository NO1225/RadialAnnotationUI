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

        /// <summary>
        /// date represented as MMM dd yyyy AM or 
        ///                     MMM dd yyyy PM
        /// </summary>
        public string DateTimePrettyText { get; private set; }

        /// <summary>
        /// The current date that the infographic is focused on
        /// can be changed by the date picker or through
        /// going forwards or backwards in time
        /// </summary>
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
                    /// see if the new date is from the datepicker
                    /// if so, and it is more than one day, always
                    /// set the AM / PM to AM
                    var t = Math.Abs(mCurrentDateToShow.Ticks - value.Ticks);
                    var d = TimeSpan.FromTicks(t).TotalHours;
                    if (d > 24)
                    {
                        MorningOrNight = AMPMEnum.AM;
                    }
                    mCurrentDateToShow = value;

                    /// set the date pretty text property
                    DateTimePrettyText = mCurrentDateToShow.ToString("MMM dd yyyy")
                        + " " + MorningOrNight.ToString();

                    /// refresh the infographic
                    Refresh();
                }

                /// I don't think this needs to be here anymore
                else mCurrentDateToShow = DateTime.Now;
            }
        }

        /// <summary>
        /// property to represent if we are currently in AM or PM
        /// </summary>
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
                    /// set the infographic time to :  00:00:00 to 11:59:59
                    InfographicStartTime = new DateTime(2019, 12, 12, 0, 0, 0);
                }
                /// set the infographic time to :  12:00:00 to 23:59:59
                else InfographicStartTime = new DateTime(2019, 12, 12, 12, 0, 0);

                /// set the date pretty text property
                DateTimePrettyText = mCurrentDateToShow.ToString("MMM dd yyyy")
                    + " " + MorningOrNight.ToString();

                /// refresh the infographic
                Refresh();
            }
        }

        /// <summary>
        /// A copy of all the Recordings made by the user for:
        /// Glucose Level,
        /// Carb Intake,
        /// Exersize,
        /// Short term insulin,
        /// Long term insulin
        /// </summary>
        public UserRecordingsDataModel UserRecordings { get; set; }

        /// <summary>
        /// current infographic start time... currently they can only 
        /// be 00:00:00 or 12:00:00
        /// </summary>
        public DateTime InfographicStartTime { get; set; }

        /// <summary>
        /// The background graphics for the infographic
        /// </summary>
        public BackgroundRadialGraphicViewModel BackGround { get; set; }

        /// <summary>
        /// The foreground graphics for the infographic
        /// Todo: This isn't being used yet, it is converted
        /// back to MainBadges currently at the end of the refresh() method
        /// </summary>
        public ForegroundRadialGraphicViewModel ForeGround { get; set; }

        /// <summary>
        /// The radar image for the background
        /// </summary>
        public BaseRadialGraphicViewModel RadarGraphic { get; set; }

        /// <summary>
        /// The outside ring colored polygons that show glucose Recordings,
        /// Caloric intake and the background is colored to indicate if the 
        /// glucose Recording is within what range: low, good, high, dangerously
        /// high
        /// Todo: currently this includes the center circle and also the 
        /// 'arm of the clock' graphic.  It should probably be seperated
        /// but I didn't want to screw up the wpf ui
        /// </summary>
        public BaseRadialGraphicViewModel MainBadges { get; set; }

        /// <summary>
        /// The LineArcs that represent minimum intensity Exersize
        /// </summary>
        public BaseRadialGraphicViewModel MinimumIntensityExersizeArcs { get; set; }

        /// <summary>
        /// The outside LineArcs that represent low intensity Exersize
        /// </summary>
        public BaseRadialGraphicViewModel LowIntensityExersizeArcs { get; set; }

        /// <summary>
        /// The LineArcs that represent medium intensity Exersize
        /// </summary>
        public BaseRadialGraphicViewModel MediumIntensityExersizeArcs { get; set; }

        /// <summary>
        /// The LineArcs that represent High intensity Exersize
        /// </summary>
        public BaseRadialGraphicViewModel HighIntensityExersizeArcs { get; set; }

        /// <summary>
        /// The LineArcs that represent maximum intensity Exersize
        /// </summary>
        public BaseRadialGraphicViewModel MaximumIntensityExersizeArcs { get; set; }

        /// <summary>
        /// The outside LineArcs that represent short term insulin availability
        /// </summary>
        public BaseRadialGraphicViewModel ShortActings { get; set; }

        /// <summary>
        /// The inside lineArcs that represent long term insullin availability 
        /// </summary>
        public BaseRadialGraphicViewModel LongActings { get; set; }

        public double RadarDiameter { get; set; } = 120 * 2;

        public double ContainerWidth { get; set; } = 400;

        public double ContainerHeight { get; set; } = 400;

        public double RadarLeft { get; set; } = 50;

        public double RadarRight { get; set; } = 50;

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to generate the infographic
        /// </summary>
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
        /// command to change the infographic back 24 hours
        /// </summary>
        public ICommand GoBack24HoursCommand { get; set; }

        /// <summary>
        /// command to change the infographic forward 12 hours
        /// </summary>
        public ICommand GoForward12HoursCommand { get; set; }

        /// <summary>
        /// command to change the infographic forward 12 hours
        /// </summary>
        public ICommand GoForward24HoursCommand { get; set; }



        #endregion

        #region Default Constructor

        public ContainerViewModel()
        {
            /// Set the AM and PM setting to AM
            MorningOrNight = AMPMEnum.AM;

            /// Load the User Records ( TODO:currently dummy data)
            UserRecordings = new UserRecordingsDataModel();

            ///
            /// Initialize the Relay Commands
            /// 
            /// The command to generate the infographic
            RefreshCommand = new RelayCommand(Refresh);

            /// Command to toggle between AM and PM
            ToggleAmAndPmCommand = new RelayCommand(ToggleAmAndPm);

            /// Command to jump back 12 hours
            GoBack12HoursCommand = new RelayCommand(GoBack12Hours);

            /// Command to jump back 12 hours
            GoBack24HoursCommand = new RelayCommand(GoBack24Hours);

            /// Command to jump forward 12 hours
            GoForward12HoursCommand = new RelayCommand(GoForward12Hours);

            /// Command to jump forward 24 hours
            GoForward24HoursCommand = new RelayCommand(GoForward24Hours);

            /// Set the current date to today's date
            CurrentDateToShow = DateTime.Now;

            /// Calls the refresh method to generate the infographic
            Refresh();
        }

        #endregion

        #region Helping Methods

        /// <summary>
        /// change the infographic to 12 hours previous
        /// </summary>
        public void GoBack12Hours()
        {
            /// Check if it is morning, if so, then make the current day, yesterday
            if (MorningOrNight == AMPMEnum.AM)
            {
                CurrentDateToShow = CurrentDateToShow.Subtract(new TimeSpan(24, 0, 0));
            }

            /// Command to toggle between AM and PM
            ToggleAmAndPm();

            /// Calls the refresh method to generate the infographic
            Refresh();
        }

        /// <summary>
        /// change the infographic to 24 hours previous
        /// </summary>
        public void GoBack24Hours()
        {
            /// Set the current date to show to one day prior
            CurrentDateToShow = CurrentDateToShow.Subtract(new TimeSpan(24, 0, 0));

            /// Calls the refresh method to generate the infographic
            Refresh();
        }


        /// <summary>
        /// change the infographic to 12 hours forward in time
        /// </summary>
        public void GoForward12Hours()
        {
            /// Check if it is afternoon, if so, then make the current day, tomorrow
            if (MorningOrNight == AMPMEnum.PM)
            {
                CurrentDateToShow = CurrentDateToShow.AddDays(1);
            }

            /// Command to toggle between AM and PM
            ToggleAmAndPm();

            /// Calls the refresh method to generate the infographic
            Refresh();
        }

        /// <summary>
        /// change the infographic to 24 hours forward in time
        /// </summary>
        public void GoForward24Hours()
        {
            /// set the current date to show to one day in the future
            CurrentDateToShow = CurrentDateToShow.AddDays(1);

            /// Calls the refresh method to generate the infographic
            Refresh();
        }

        /// <summary>
        /// Switches the AM and PM property...
        /// </summary>
        public void ToggleAmAndPm()
        {
            /// ToDo: I know there is a better way to do this

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


            /// Create a new background property
            BackGround = new BackgroundRadialGraphicViewModel();

            /// Add outside ring to background
            AddWhiteOutsideRingToBackground();

            /// add radar graphic to background
            AddRadarGraphicToBackground();

            /// add foreground stuff to MainBadges
            AddForgroundGraphicStuff();
        }

        ///
        /// Method to: Add the white ring to be used under the badges
        /// to the background
        /// Todo: This should probably be black
        /// 
        private void AddWhiteOutsideRingToBackground()
        {
            /// create a ring given the inner and out radii and assign a color
            var whiteBackgroundRing = new RingFullFilledViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 2,
                NumberOfChildrenInGroup = 1,
                InnerRadius = 121,
                OuterRadius = 169,
                GraphicsColor = BadgeColor.Yellow,
            };

            /// Populate the ring by adding each ring segment to each other 
            /// with varying x and y values
            whiteBackgroundRing.PopulateRadialGraphicSegmentsProperty();

            /// Add the Ring to the Background
            BackGround.AddGraphics(whiteBackgroundRing);
        }

        ///
        /// Method to: Add radar graphic to background
        /// 
        private void AddRadarGraphicToBackground()
        {
            /// create new radar graphic based on the base circular 
            /// graphic view model
            RadarGraphic = new BaseRadialGraphicViewModel();

            /// Create crosshairs graphic out of RadialLines view model
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

            /// generate graphic items for crosshairs
            crosshairsGraphic.PopulateRadialGraphicSegmentsProperty();

            /// Add crosshairs graphic to radar graphics
            RadarGraphic.AddGraphics(crosshairsGraphic);

            /// generate graphic for innermost circle of the radar
            var innerRadarCircle = new CircleFullLineViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                InnerRadius = 30,
                OuterRadius = 33,
                GraphicsColor = BadgeColor.Green,
            };

            /// populate the pieces to build the graphic
            innerRadarCircle.PopulateRadialGraphicSegmentsProperty();

            /// add the innerRadarCircle to the RadarGraphic
            RadarGraphic.AddGraphics(innerRadarCircle);

            /// generate graphic for middle circle of the radar
            var middleRadarCircle = new CircleFullLineViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                InnerRadius = 60,
                OuterRadius = 63,
                GraphicsColor = BadgeColor.Green,
            };

            /// populate the pieces to build the graphic
            middleRadarCircle.PopulateRadialGraphicSegmentsProperty();

            /// add the middleRadarCircle to the RadarGraphic
            RadarGraphic.AddGraphics(middleRadarCircle);

            /// generate graphic for outermost circle of the radar
            var outsideRadarCircle = new CircleFullLineViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                InnerRadius = 90,
                OuterRadius = 93,
                GraphicsColor = BadgeColor.Green,
            };

            /// populate the pieces to build the graphic
            outsideRadarCircle.PopulateRadialGraphicSegmentsProperty();

            /// add the outesideRadarCircle to the RadarGraphic
            RadarGraphic.AddGraphics(outsideRadarCircle);

            /// add the complete radar graphic to the background
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

            /// populate the pieces to build the graphic
            MainBadges.PopulateRadialGraphicSegmentsProperty();

            /// add the users recordings to the infographic
            /// to this 12 hour time period
            PopulateBadgesWithGlucoseRecordings(MainBadges);

            /// ************************************************************
            /// ***************** Adding the exersize arcs *****************
            /// ************************************************************
            /// 
            /// Add the minimum intensity exersize rings
            MinimumIntensityExersizeArcs = new BaseRadialGraphicViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 12,
                NumberOfChildrenInGroup = 5,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 114,
                OuterRadius = 119,
                FullAngleFrom = 0,
                FullAngleTo = 360,
                GraphicsColor = (BadgeColor)BadgeColor.Black,
            };

            /// populate the pieces to build the graphic
            MinimumIntensityExersizeArcs.PopulateRadialGraphicSegmentsProperty();

            /// add the users exersize to the infographic
            /// to this 12 hour time period
            PopulateBadgesWithExersizeRecordings(MinimumIntensityExersizeArcs, ExersizeQualityEnum.MinimumIntensity);

            /// Add the low intensity exersize rings
            LowIntensityExersizeArcs = new BaseRadialGraphicViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 12,
                NumberOfChildrenInGroup = 5,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 95,
                OuterRadius = 100,
                FullAngleFrom = 0,
                FullAngleTo = 360,
                GraphicsColor = (BadgeColor)BadgeColor.Black,
            };

            /// populate the pieces to build the graphic
            LowIntensityExersizeArcs.PopulateRadialGraphicSegmentsProperty();

            /// add the users exersize to the infographic
            /// to this 12 hour time period
            PopulateBadgesWithExersizeRecordings(LowIntensityExersizeArcs, ExersizeQualityEnum.LowIntensity);

            /// Add the medium intensity exersize rings
            MediumIntensityExersizeArcs = new BaseRadialGraphicViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 12,
                NumberOfChildrenInGroup = 5,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 75,
                OuterRadius = 80,
                FullAngleFrom = 0,
                FullAngleTo = 360,
                GraphicsColor = (BadgeColor)BadgeColor.Black,
            };

            /// populate the pieces to build the graphic
            MediumIntensityExersizeArcs.PopulateRadialGraphicSegmentsProperty();

            /// add the users exersize to the infographic
            /// to this 12 hour time period
            PopulateBadgesWithExersizeRecordings(MediumIntensityExersizeArcs, ExersizeQualityEnum.MediumIntensity);

            /// Add the high intensity exersize rings
            HighIntensityExersizeArcs = new BaseRadialGraphicViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 12,
                NumberOfChildrenInGroup = 5,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 54,
                OuterRadius = 59,
                FullAngleFrom = 0,
                FullAngleTo = 360,
                GraphicsColor = (BadgeColor)BadgeColor.Black,
            };

            /// populate the pieces to build the graphic
            HighIntensityExersizeArcs.PopulateRadialGraphicSegmentsProperty();

            /// add the users exersize to the infographic
            /// to this 12 hour time period
            PopulateBadgesWithExersizeRecordings(HighIntensityExersizeArcs, ExersizeQualityEnum.HighIntensity);

            /// Add the maximum intensity exersize rings
            MaximumIntensityExersizeArcs = new BaseRadialGraphicViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 12,
                NumberOfChildrenInGroup = 5,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 34,
                OuterRadius = 39,
                FullAngleFrom = 0,
                FullAngleTo = 360,
                GraphicsColor = (BadgeColor)BadgeColor.Black,
            };

            /// populate the pieces to build the graphic
            MaximumIntensityExersizeArcs.PopulateRadialGraphicSegmentsProperty();

            /// add the users exersize to the infographic
            /// to this 12 hour time period
            PopulateBadgesWithExersizeRecordings(MaximumIntensityExersizeArcs, ExersizeQualityEnum.MaximumIntensity);

            /// TODO: this needs to go
            /// Generate random glucose levels, carb intake levels
            /// and colors for the container fill
            var rand = new Random();

            MainBadges.RadialGraphicSegments.Where(a => a.Angle > 30).ToList().ForEach(a =>
            {
                a.BadgeColor = (BadgeColor)rand.Next(2, 7);
                a.GlucoseLevel = ((int)a.BadgeColor).ToString("F1");
                a.CarbAmount = ((int)a.BadgeColor).ToString();
            }
            );

            /// populate the pieces to build the graphic
            PopulateBadgesWithGlucoseRecordings(MainBadges);

            /// call the method to add the short term insulin arcs to the 
            /// foreground
            CreateShortTermInsulin();

            ///
            /// Start the Long Acting Lines added to the infographic
            /// 
            LongActings = new BaseRadialGraphicViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 1,
                NumberOfChildrenInGroup = 2,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 45,
                OuterRadius = 48,
                FullAngleFrom = 5,
                FullAngleTo = 355,
                GraphicsColor = BadgeColor.White,
            };

            /// populate the pieces to build the graphic
            LongActings.PopulateRadialGraphicSegmentsProperty();

            /// add main badges, short and long term insulin
            /// graphics to main foreground graphic
            ForeGround.AddGraphics(MainBadges);
            ForeGround.AddGraphics(LongActings);
            ForeGround.AddGraphics(MinimumIntensityExersizeArcs);
            ForeGround.AddGraphics(LowIntensityExersizeArcs);
            ForeGround.AddGraphics(MediumIntensityExersizeArcs);
            ForeGround.AddGraphics(HighIntensityExersizeArcs);
            ForeGround.AddGraphics(MaximumIntensityExersizeArcs);


            ///
            /// Start the clock arm graphic added to the infographic
            /// 
            var mainNeedle = new PizzaSliceFilledViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                OuterRadius = 120,
                FullAngleFrom = -1,
                FullAngleTo = 1,
                GraphicsColor = BadgeColor.White,
            };

            /// populate the pieces to build the graphic
            mainNeedle.PopulateRadialGraphicSegmentsProperty();

            /// Add the clock arm needle graphic to the foreground
            ForeGround.RadialGraphicSegments.Add(mainNeedle.RadialGraphicSegments.First());

            ///
            /// Start the solid white circle in the middle, added to the infographic
            /// 
            var centerCircle = new CircleFullFilledViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                OuterRadius = 10,
                GraphicsColor = BadgeColor.White,
            };

            /// populate the pieces to build the graphic
            centerCircle.PopulateRadialGraphicSegmentsProperty();

            /// Add the solid white circle in the middle to the foreground
            ForeGround.AddGraphics(centerCircle);

            /// TODO: lol, then I just change the foreground back into mainBadges...
            /// needs to be changed
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

        /// <summary>
        /// iterates through the User Recordings and populates the individual time segments 
        /// with glucose readings, background colors (depending on glucose reading), carb
        /// intake readings, 
        /// TODO: Need to add the populating for exersize, short term insulin, and long
        /// term insulin
        /// </summary>
        /// <param name="mainBadges"></param>
        /// 
        /// ************************************************************
        /// **************** Adding the glucose part... ****************
        /// ************************************************************
        public void PopulateBadgesWithGlucoseRecordings(BaseRadialGraphicViewModel mainBadges)
        {
            /// converts the start time hour (either 00 or 12) to minutes
            int starttime = InfographicStartTime.Hour * 60;

            /// sets the first background color to black
            var mostRecentGlucoseBackgroundColor = BadgeColor.Black;

            /// iterate through all the time segments currently being shown on the infographic
            foreach (BaseRadialGraphicSegmentViewModel timeSegment in mainBadges.RadialGraphicSegments)
            {
                /// set the glucose match flag to false
                var glucoseMatch = false;

                /// iterate through all the glucose recordings in the user's UserRecordingsDataModel
                foreach (GlucoseLevelRecordingDataModel glucoseRecording in UserRecordings.GlucoseLevelRecordings)
                {
                    /// check to see if the date of the user recording matches the date of the time segment
                    if (glucoseRecording.StartTime.Date == CurrentDateToShow.Date)
                    {
                        /// convert the time of the recording into minutes
                        int glucoseTime = glucoseRecording.StartTime.Hour * 60 + glucoseRecording.StartTime.Minute;

                        /// if the time of the glucose recording is within the start and 
                        /// end time of the time segment then continue
                        if (glucoseTime >= starttime && glucoseTime < starttime + 12)
                        {
                            /// converts the glucose reading in the record into a decimal
                            /// (it is stored as an integer, so every recording is multiplied
                            /// by 10 for storage... It must be converted back into a one 
                            /// decimal place decimal from integer
                            decimal gl = (decimal)glucoseRecording.GlucoseLevel / 10;

                            /// convert the new decimal glucose level reading into a string
                            /// with one decimal place
                            timeSegment.GlucoseLevel = string.Format("{0:F1}", gl);

                            /// set the glucose match flag so that we can change the background
                            /// color for all future time segments until we find another reading
                            glucoseMatch = true;

                            /// 4 if/else statements to set the background color of the time segment
                            /// depending on the glucose level reading... ToDO: This should be somewhere 
                            /// else, maybe in a settings page for a particular user
                            if (gl < 5) { timeSegment.BadgeColor = BadgeColor.White; }
                            else if (gl < 8) { timeSegment.BadgeColor = BadgeColor.Blue; }
                            else if (gl < 11) { timeSegment.BadgeColor = BadgeColor.Pink; }
                            else { timeSegment.BadgeColor = BadgeColor.Red; }

                            /// change the background color for all future time segments to this color.
                            /// it will stay this way until we find another recording.
                            mostRecentGlucoseBackgroundColor = timeSegment.BadgeColor;
                        }
                    }
                }
                /// If we didn't find a match...
                if (!glucoseMatch)
                {
                    /// set the background color of the time segment to the most recent one
                    timeSegment.BadgeColor = mostRecentGlucoseBackgroundColor;

                    /// set the text of the time segment to nothing, there wasn't a reading.
                    timeSegment.GlucoseLevel = "";
                }
                /// ************************************************************
                /// **************** Adding the carb intake part... ************
                /// ************************************************************
                /// 

                /// Set the carb intake match flag to false
                var carbMatch = false;

                /// iterate through all the carb intake recordings in the user's UserRecordingsDataModel
                foreach (CarbIntakeRecordingDataModel carbRecording in UserRecordings.CarbIntakeRecordings)
                {
                    /// Check to see if the recording happened on the day of this time segment
                    if (carbRecording.StartTime.Date == CurrentDateToShow.Date)
                    {
                        /// convert the time of the recording to minutes
                        int carbTime = carbRecording.StartTime.Hour * 60 + carbRecording.StartTime.Minute;

                        /// if the recording falls in this 12 minute segment we are looking at...
                        if (carbTime >= starttime && carbTime < starttime + 12)
                        {
                            /// Add the text to the time segment carb text
                            timeSegment.CarbAmount = carbRecording.CarbIntakeAmount.ToString();

                            /// set the 'found a carb intake that matches' flag to true
                            carbMatch = true;
                        }
                    }
                }

                /// if there was no recording of carb intake in this time segment, set the 
                /// text to blank
                if (!carbMatch)
                {
                    /// if there isn't a carb reading for this time segment make the text blank
                    timeSegment.CarbAmount = "";
                }

                /// set the start time to the start time of the next infographic so we can check the next day
                starttime = starttime + 12;
            }
        }

        /// <summary>
        /// iterates through the User Recordings and populates the individual time segments 
        /// with glucose readings, background colors (depending on glucose reading), carb
        /// intake readings, 
        /// TODO: Need to add the populating for exersize, short term insulin, and long
        /// term insulin
        /// </summary>
        /// <param name="mainBadges"></param>
        /// 
        /// ************************************************************
        /// **************** set color for exersize arcs ****************
        /// ************************************************************
        public void PopulateBadgesWithExersizeRecordings(BaseRadialGraphicViewModel exersizeArcs, ExersizeQualityEnum quality)
        {

            /// converts the start time hour (either 00 or 12) to minutes
            int starttime = InfographicStartTime.Hour * 60;

            /// iterate through all the time segments currently being shown on the infographic
            foreach (BaseRadialGraphicSegmentViewModel timeSegment in exersizeArcs.RadialGraphicSegments)
            {
                /// set the glucose match flag to false
                var exersizeMatch = false;

                /// iterate through all the glucose recordings in the user's UserRecordingsDataModel
                foreach (ExersizeRecordingDataModel exersizeRecording in UserRecordings.ExersizeRecordings)
                {
                    /// check to see if the arc is necessary, depending on the level of exersize intensity
                    if (exersizeRecording.Quality >= quality)
                    {
                        /// check to see if the date of the user recording matches the date of the time segment
                        if (exersizeRecording.StartTime.Date == CurrentDateToShow.Date)
                        {
                            /// convert the time of the recording into minutes
                            int exersizeStartTime = exersizeRecording.StartTime.Hour * 60 + exersizeRecording.StartTime.Minute;

                            /// convert the start time + duration into minutes
                            int exersizeEndTime = exersizeStartTime + (int)exersizeRecording.Duration.TotalMinutes;

                            /// boolean flag
                            var changeExersizeColor = false;

                            /// if the time of the glucose recording is within the start and 
                            /// end time of the time segment then continue
                            if (starttime >= exersizeStartTime && starttime < exersizeEndTime)
                            {
                                changeExersizeColor = true;
                            }
                            /// I think that this and the If statement above can probably be combined
                            if (changeExersizeColor)
                            {
                                /// change the color from black to lime green to indicate 
                                /// that there is an exersize property in this time segment
                                timeSegment.BadgeColor = BadgeColor.LimeGreen;
                            }
                        }
                    }
                }
                /// move the start time to the next time segment
                starttime = starttime + 12;
            }
        }

        /// <summary>
        /// method to create a small arc in each time segment that will make up a long
        /// arc that displays the insulin in the system.  The arc starts off as a solid
        /// line and then turns into a slowly diminishing dotted line depending on
        /// how effective the insulin is in the system over time.
        /// </summary>
        public void CreateShortTermInsulin()
        {
            /// converts the start time hour (either 00 or 12) to minutes
            int starttime = InfographicStartTime.Hour * 60;

            /// iterate through all the insulin recordings in the user's recordings
            foreach (ShortTermInsulinRecordingDataModel insulinRecording in UserRecordings.ShortTermInsulinRecordings)
            {
                /// check to see if the date of the user recording matches the date of the time segment
                if (insulinRecording.StartTime.Date == CurrentDateToShow.Date)
                {
                    /// check to see if we are in the correct am/pm time period
                    if ((insulinRecording.StartTime.Hour < 12 && MorningOrNight == AMPMEnum.AM
                        || insulinRecording.StartTime.Hour >= 12 && MorningOrNight == AMPMEnum.PM))
                    {
                        /// iterate through all the minutes segments in the 12 hour period
                        /// incrementing in 12 minute periods
                        for (var minutes = 0; minutes < 720; minutes += 12)
                        {

                            /// convert the time of the recording into minutes
                            var insulinStartTime = insulinRecording.StartTime.Hour * 60 + insulinRecording.StartTime.Minute;

                            /// convert the insulin recording duration into minutes
                            var duration = (int)insulinRecording.Duration.TotalMinutes;

                            /// figure out the time the insulin will wear off 100%
                            /// in minutes
                            var insulinEndTime = insulinStartTime + duration;

                            /// the small piece of arc that makes up the big ar
                            ArcLineViewModel arcLineRadialGraphicSegment;

                            /// private variable
                            PercentageOfInsulinEffectiveness effectiveness;

                            /// figure out if it is in the first 40% of the effectiveness of
                            /// the insulin
                            if (minutes < insulinStartTime + duration * .4)
                            {
                                effectiveness = PercentageOfInsulinEffectiveness.MaximumEffectiveness;
                            }
                            /// if not first 40%, check to see if it is in the next 30%
                            else if (minutes < insulinStartTime + duration * .7)
                            {
                                effectiveness = PercentageOfInsulinEffectiveness.PartialEffectiveness;
                            }
                            /// finally assign a level of minimum effectiveness
                            else
                            {
                                effectiveness = PercentageOfInsulinEffectiveness.MinimumEffectiveness;
                            }

                            /// check to see if the current time segment is inside
                            /// the insulin reading start and end time
                            if (minutes >= insulinStartTime && minutes <= insulinEndTime)
                            {
                                /// create a new arc segment to fill this time segment
                                arcLineRadialGraphicSegment =
                                    new ArcLineViewModel()
                                    {
                                        ContainerHeight = this.ContainerHeight,
                                        ContainerWidth = this.ContainerWidth,
                                        NumberOfGroups = 1,
                                        NumberOfChildrenInGroup = 2,
                                        ChildClearance = 0,
                                        GroupClearance = 0,
                                        InnerRadius = 105,
                                        OuterRadius = 108,
                                        FullAngleFrom = minutes / 12 * 6 + (int)effectiveness,
                                        FullAngleTo = minutes / 12 * 6 + 6 - (int)effectiveness,
                                        GraphicsColor = BadgeColor.White
                                    };
                                /// generate the arcRadialGraphicSegment for this time segment
                                arcLineRadialGraphicSegment.PopulateRadialGraphicSegmentsProperty();

                                /// if it isn't null, which it shouldn't be
                                if (arcLineRadialGraphicSegment != null)
                                {
                                    /// add the arc to the foreground
                                    ForeGround.AddGraphics(arcLineRadialGraphicSegment);
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    #endregion
}

