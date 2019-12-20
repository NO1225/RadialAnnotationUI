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

        public RadialAnnotationContainerViewModel BackGround { get; set; }

        public RadialAnnotationContainerViewModel Radar { get; set; }

        public RadialAnnotationContainerViewModel MainBadges { get; set; }

        public RadialAnnotationContainerViewModel ShortActings { get; set; }

        public RadialAnnotationContainerViewModel LongActings { get; set; }

        public double RadarDiameter { get; set; } = 120 * 2;
        
        public double ContainerWidth { get; set; } = 400;

        public double ContainerHeight { get; set; } = 400;

        public double RadarLeft { get; set; } = 50;

        public double RadarRight { get; set; } = 50;

        #endregion

        #region Public Commands

        public ICommand RefreshCommand { get; set; }

        #endregion

        #region Default Constructor

        public ContainerViewModel()
        {

            RefreshCommand = new RelayCommand(Refresh);

            Refresh();
        }

        #endregion

        #region Helping Methods

        private void Refresh()
        {
            BackGround = new RadialAnnotationContainerViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 2,
                NumberOfChildrenInGroup = 1,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 121,
                OuterRadius = 169,
                FullAngleFrom = 0,
                FullAngleTo = 360,
                BadgeColor = BadgeColor.White,
            };

            BackGround.Refresh();

            var radar = new RadialAnnotationContainerViewModel()
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
                FullAngleTo = 360-46,
                BadgeColor = BadgeColor.Green,
            };

            radar.Refresh();

            foreach(AnnotationViewModel annotation in radar.Annotations)
            {
                BackGround.Annotations.Add(annotation);
            }
            radar = new RadialAnnotationContainerViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 1,
                NumberOfChildrenInGroup = 2,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 30,
                OuterRadius = 33,
                FullAngleFrom = 0,
                FullAngleTo = 360,
                BadgeColor = BadgeColor.Green,
            };

            radar.Refresh();

            foreach (AnnotationViewModel annotation in radar.Annotations)
            {
                BackGround.Annotations.Add(annotation);
            }
            
            radar = new RadialAnnotationContainerViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 1,
                NumberOfChildrenInGroup = 2,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 60,
                OuterRadius = 63,
                FullAngleFrom = 0,
                FullAngleTo = 360,
                BadgeColor = BadgeColor.Green,
            };

            radar.Refresh();

            foreach (AnnotationViewModel annotation in radar.Annotations)
            {
                BackGround.Annotations.Add(annotation);
            }
            
            radar = new RadialAnnotationContainerViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 1,
                NumberOfChildrenInGroup = 2,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 90,
                OuterRadius = 93,
                FullAngleFrom = 0,
                FullAngleTo = 360,
                BadgeColor = BadgeColor.Green,
            };

            radar.Refresh();

            foreach (AnnotationViewModel annotation in radar.Annotations)
            {
                BackGround.Annotations.Add(annotation);
            }

            MainBadges = new RadialAnnotationContainerViewModel()
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
                BadgeColor = BadgeColor.Blue,
            };

            MainBadges.Refresh();

            var rand = new Random();

            MainBadges.Annotations.Where(a => a.Angle > 30).ToList().ForEach(a => 
            {
                a.BadgeColor = (BadgeColor)rand.Next(2, 7);
                a.GlucoseLevel = ((int)a.BadgeColor).ToString("F1");
                a.CarbAmount = ((int)a.BadgeColor).ToString();
                }
            );

            ShortActings = new RadialAnnotationContainerViewModel()
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
                BadgeColor = BadgeColor.White,
            };

            ShortActings.Refresh();

            LongActings = new RadialAnnotationContainerViewModel()
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
                BadgeColor = BadgeColor.White,
            };

            LongActings.Refresh();

            foreach(AnnotationViewModel annotationViewModel in ShortActings.Annotations)
            {
                MainBadges.Annotations.Add(annotationViewModel);
            }

            foreach(AnnotationViewModel annotationViewModel in LongActings.Annotations)
            {
                MainBadges.Annotations.Add(annotationViewModel);
            }

            var mainNeedle = new RadialAnnotationContainerViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 1,
                NumberOfChildrenInGroup = 1,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 0,
                OuterRadius = 120,
                FullAngleFrom = -1,
                FullAngleTo = 1,
                BadgeColor = BadgeColor.White,
            };

            mainNeedle.Refresh();

            MainBadges.Annotations.Add(mainNeedle.Annotations.First());

            mainNeedle = new RadialAnnotationContainerViewModel()
            {
                ContainerHeight = this.ContainerHeight,
                ContainerWidth = this.ContainerWidth,
                NumberOfGroups = 1,
                NumberOfChildrenInGroup = 2,
                ChildClearance = 0,
                GroupClearance = 0,
                InnerRadius = 0,
                OuterRadius = 10,
                FullAngleFrom = 0,
                FullAngleTo = 360,
                BadgeColor = BadgeColor.White,
            };

            mainNeedle.Refresh();

            foreach (AnnotationViewModel annotationViewModel in mainNeedle.Annotations)
            {
                MainBadges.Annotations.Add(annotationViewModel);
            }
            //MainBadges.Refresh();
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        } 

        #endregion
    }
}
