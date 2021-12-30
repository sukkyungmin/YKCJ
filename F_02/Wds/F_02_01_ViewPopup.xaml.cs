﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using YKCJViewModel.F_02;

namespace YKCJ_EngineerReport.F_02.Wds
{
    /// <summary>
    /// F_02_01_ViewPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class F_02_01_ViewPopup : Window
    {
        private F_02_01_ViewModel Viewmodel = null;

        private double orginalWidth, originalHeight;
        private ScaleTransform scale = new ScaleTransform();
        public F_02_01_ViewPopup(F_02_01_ViewModel model)
        {
            InitializeComponent();

            Viewmodel = model;
            DataContext = Viewmodel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Width = 1600;
            //this.Height = 800;

            orginalWidth = this.Width;
            originalHeight = this.Height;

            if (this.WindowState == WindowState.Maximized)
            {
                ChangeSize(this.ActualWidth, this.ActualHeight);
            }

            this.SizeChanged += new SizeChangedEventHandler(Window1_SizeChanged);
        }

        void Window1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSize(e.NewSize.Width, e.NewSize.Height);
        }

        private void ChangeSize(double width, double height)
        {
            scale.ScaleX = width / orginalWidth;
            scale.ScaleY = height / originalHeight;

            FrameworkElement rootElement = this.Content as FrameworkElement;

            rootElement.LayoutTransform = scale;
        }

        private void WindowMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState != WindowState.Maximized) ? WindowState.Maximized : WindowState.Normal;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
