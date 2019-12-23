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
