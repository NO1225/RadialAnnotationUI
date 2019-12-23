using System;
using System.Collections.Generic;
using System.Text;

namespace BaseWpfCore
{
    /// <summary>
    /// Represents all the entries for a user
    /// </summary>
    public class UserRecordingsDataModel : BaseViewModel
    {
        public List<GlucoseLevelRecordingDataModel> GlucoseLevelRecordings { get; set; }
        = new List<GlucoseLevelRecordingDataModel>();

        public List<CarbIntakeRecordingDataModel> CarbIntakeRecordings { get; set; }
        = new List<CarbIntakeRecordingDataModel>();

        public List<ExersizeRecordingDataModel> ExersizeRecordings { get; set; }
        = new List<ExersizeRecordingDataModel>();

        public List<LongTermInsulinRecordingDataModel> LongTermInsulinRecordings { get; set; }
        = new List<LongTermInsulinRecordingDataModel>();

        public List<ShortTermInsulinRecordingDataModel> ShortTermInsulinRecordings { get; set; }
        = new List<ShortTermInsulinRecordingDataModel>();

        public List<TimeSegmentViewModel> TimeSegments { get; set; }
        = new List<TimeSegmentViewModel>();

        public UserRecordingsDataModel()
        {
            LoadDummyData();
        }

        public void LoadDummyData()
        {
            GlucoseLevelRecordings = new List<GlucoseLevelRecordingDataModel>
            {
                ///
                /// readings for dec 20 2019
                ///
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 20, 1, 15, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 20, 3, 30, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 20, 7, 05, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 20, 10, 31, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 20, 13, 15, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 20, 16, 18, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 20, 19, 05, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 20, 22, 31, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                ///
                /// readings for dec 21 2019
                ///
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 21, 2, 15, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 21, 4, 30, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 21, 8, 05, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 21, 11, 31, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 21, 14, 15, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 21, 17, 18, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 21, 20, 05, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 21, 21, 31, 0),
                    Duration = new TimeSpan(0,0,1)
                },///
                /// readings for dec 22 2019
                ///
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 22, 1, 15, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 22, 3, 30, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 22, 7, 05, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 22, 10, 31, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 22, 13, 15, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 22, 16, 18, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 22, 19, 05, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 22, 22, 31, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                ///
                /// readings for dec 23 2019
                ///
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 23, 2, 15, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 23, 4, 30, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 23, 8, 05, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 23, 11, 31, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 23, 14, 15, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 23, 17, 18, 0),
                    Duration = new TimeSpan(0,0,1)
                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 50,
                    StartTime = new DateTime(2019, 12, 23, 20, 05, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new GlucoseLevelRecordingDataModel
                {
                    GlucoseLevel = 67,
                    StartTime = new DateTime(2019, 12, 23, 21, 31, 0),
                    Duration = new TimeSpan(0,0,1)
                }

            };

            CarbIntakeRecordings = new List<CarbIntakeRecordingDataModel>
            {
                new CarbIntakeRecordingDataModel
                {
                    CarbIntakeAmount = 3,
                    StartTime = new DateTime(2019, 12, 22, 1, 45, 0),
                    Duration = new TimeSpan(0,0,1)

                },
                new CarbIntakeRecordingDataModel
                {
                    CarbIntakeAmount = 2,
                    StartTime = new DateTime(2019, 12, 22, 9, 45, 0),
                    Duration = new TimeSpan(0,0,1)

                }

            };

        }


    }
}
