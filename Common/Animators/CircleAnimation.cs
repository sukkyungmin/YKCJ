﻿using System;
using System.Windows.Media;

using System.Windows.Media.Animation;
using System.Windows;

namespace Common.Animators
{
    public class CircleAnimation
    {
        public event Action AnimationCompleted;
        public void MakeCircleAnimation(FrameworkElement animatedElement, double width, double height, TimeSpan timeSpan)
        {
            EllipseGeometry ellipseGeometry = new EllipseGeometry();
            ellipseGeometry.RadiusX = 0;
            ellipseGeometry.RadiusY = 0;
            double centrex = width / 2;
            double centrey = height / 2;
            ellipseGeometry.Center = new Point(centrex, centrey);
            animatedElement.Clip = ellipseGeometry; //The most important line           
            double halfWidth = width / 2;
            double halfheight = height / 2;
            DoubleAnimation a = new DoubleAnimation();
            a.Completed += new EventHandler(a_Completed);
            a.From = 0;
            a.To = Math.Sqrt(halfWidth * halfWidth + halfheight * halfheight);
            a.Duration = new Duration(timeSpan);
            ellipseGeometry.BeginAnimation(EllipseGeometry.RadiusXProperty, a);
            ellipseGeometry.BeginAnimation(EllipseGeometry.RadiusYProperty, a);

        }

        public void a_Completed(object sender, EventArgs e)
        {
            if (AnimationCompleted != null)
            {
                AnimationCompleted();
            }
        }
    }
}
