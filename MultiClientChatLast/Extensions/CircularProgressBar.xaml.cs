﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;


namespace MultiClientChatLast.Extensions
{
    /// <summary>
    /// Interaction logic for CircularProgressBar.xaml
    /// </summary>
    public partial class CircularProgressBar : UserControl
    {
        public CircularProgressBar()
        {
            InitializeComponent();

            //Use a default Animation Framerate of 20, which uses less CPU time
            //than the standard 50 which you get out of the box
            Timeline.DesiredFrameRateProperty.OverrideMetadata(
                typeof(Timeline),
                     new FrameworkPropertyMetadata { DefaultValue = 20 });
        }
    }
}
