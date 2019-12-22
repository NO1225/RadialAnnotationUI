

using BaseWpfCore.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BaseWpfCore
{
    public class ExersizeEntryDataModel : BaseViewModel
    {
        #region Public properties

        /// <summary>
        /// The quality of the exersize.  5 is an intense workout
        /// 1 is barely walking.  The property is used to determine
        /// how far the graphic extends into the inside of the infographic
        /// </summary>
        public ExersizeQualityEnum Quality { get; set; }

        // start time of the exersize
        public DateTime StartTime { get; set; }

        // the length of time the exersize lasted for
        public TimeSpan Duration { get; set; }

        public List<ArcLineViewModel> ExersizeIntensityArcs { get; set; }

        #endregion

        
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ExersizeEntryDataModel()
        {

        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="quality"> the quality of the exersize 1-5 with 5 being max</param>
        /// <param name="startTime">the date time that the exersize was started</param>
        /// <param name="duration"> the length of time the exersize lasted for</param>
        public ExersizeEntryDataModel(ExersizeQualityEnum quality, DateTime startTime, TimeSpan duration)
        {
            Quality = quality;
            StartTime = startTime;
            Duration = duration;
        }

        
        #endregion  

        public void BuildExersizeGraphic(ExersizeQualityEnum exersizeQuality)
        {

            switch (exersizeQuality)
            {
                case ExersizeQualityEnum.MaximumIntensity:

                    break;

                case ExersizeQualityEnum.HighIntensity:

                    break;

                case ExersizeQualityEnum.MediumIntensity:

                    break;

                case ExersizeQualityEnum.LowIntensity:

                    break;

                case ExersizeQualityEnum.MinimumIntensity:

                    break;

            }


        }
    }
}
