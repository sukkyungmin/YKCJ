using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;

namespace DMSGalaxy.EyeControl
{
  /// <summary>
  /// The Eye Class
  /// Maps properties to the Eye style
  /// </summary>
  /// <remarks>
  /// This control is created by Niel M. Thomas in 2010.
  /// This control is covered by the CPOL license.
  /// </remarks>
  [ToolboxBitmap(typeof(Eye))]
  public class Eye : Control
  {
    #region -- Private Members --

    private Point focusPoint;
    private double focusDistance;
    private double focusAngle;

    #endregion

    #region -- Dependency Properties --

    public static DependencyProperty IrisRimColorProperty = DependencyProperty.Register(
      "IrisRimColor",
      typeof(Brush),
      typeof(Eye),
      new PropertyMetadata(Brushes.DarkBlue));

    /// <summary>
    /// Gets or sets the color of the iris rim color.
    /// </summary>
    /// <value>The color of the iris rim color.</value>
    [Category("Eye"), Browsable(true)]
    [Description("The color of the Rim.")]
    public Brush IrisRimColor
    {
      get { return (Brush)GetValue(IrisRimColorProperty); }
      set { SetValue(IrisRimColorProperty, value); }
    }

    public static DependencyProperty IrisOuterColorProperty = DependencyProperty.Register(
      "IrisOuterColor",
      typeof(Color),
      typeof(Eye),
      new PropertyMetadata(Colors.LightBlue));

    /// <summary>
    /// Gets or sets the outer color of the iris.
    /// </summary>
    /// <value>The outer color of the iris.</value>
    [Category("Eye"), Browsable(true)]
    [Description("The outer color of the iris.")]
    public Color IrisOuterColor
    {
      get { return (Color)GetValue(IrisOuterColorProperty); }
      set { SetValue(IrisOuterColorProperty, value); }
    }


    public static DependencyProperty IrisMiddleColorProperty = DependencyProperty.Register(
      "IrisMiddleColor",
      typeof(Color),
      typeof(Eye),
      new PropertyMetadata(Colors.AliceBlue));

    /// <summary>
    /// Gets or sets the middle color of the iris.
    /// </summary>
    /// <value>The middle color of the iris.</value>
    [Category("Eye"), Browsable(true)]
    [Description("The middle color of the iris.")]
    public Color IrisMiddleColor
    {
      get { return (Color)GetValue(IrisMiddleColorProperty); }
      set { SetValue(IrisMiddleColorProperty, value); }
    }


    public static DependencyProperty IrisInnerProperty = DependencyProperty.Register(
        "IrisInnerColor",
        typeof(Color),
        typeof(Eye),
        new PropertyMetadata(Colors.DarkBlue));

    /// <summary>
    /// Gets or sets the inner color of the iris.
    /// </summary>
    /// <value>The inner color of the iris.</value>
    [Category("Eye"), Browsable(true)]
    [Description("The inner color of the iris.")]
    public Color IrisInnerColor
    {
      get { return (Color)GetValue(IrisInnerProperty); }
      set { SetValue(IrisMiddleColorProperty, value); }
    }

    public static DependencyProperty PupilColorProperty = DependencyProperty.Register(
        "PupilColor",
        typeof(Brush),
        typeof(Eye),
        new PropertyMetadata(Brushes.Black));

    /// <summary>
    /// Gets or sets the color of the pupil.
    /// </summary>
    /// <value>The color of the pupil.</value>
    [Category("Eye"), Browsable(true)]
    [Description("The pupil color")]
    public Brush PupilColor
    {
      get { return (Brush)GetValue(PupilColorProperty); }
      set { SetValue(PupilColorProperty, value); }
    }

    public static DependencyProperty PupilSizeProperty = DependencyProperty.Register(
      "PupilSize",
      typeof(double),
      typeof(Eye),
      new PropertyMetadata((double)1));

    /// <summary>
    /// Gets or sets the size of the pupil.
    /// </summary>
    /// <value>The pupil size (0-5).</value>
    [Category("Eye"), Browsable(true)]
    [Description("The size of the pupil (0-5).")]
    public double PupilSize
    {
      get { return (double)GetValue(PupilSizeProperty); }
      set { SetValue(PupilSizeProperty, value); }
    }

    public static DependencyProperty IrisSizeProperty = DependencyProperty.Register(
      "IrisSize",
      typeof(double),
      typeof(Eye),
      new PropertyMetadata(0.6));

    /// <summary>
    /// Gets or sets the size of the iris.
    /// </summary>
    /// <value>The size of the iris (0.3 - 2).</value>
    [Category("Eye"), Browsable(true)]
    [Description("The size of the iris (0.3 - 2")]
    public double IrisSize
    {
      get { return (double)GetValue(IrisSizeProperty); }
      set { SetValue(IrisSizeProperty, value); }
    }

    /// <summary>
    /// Sets the focus point.
    /// </summary>
    /// <value>The focus point.</value>
    public Point FocusPoint
    {
      set
      {
        if (focusPoint == value) return;
        focusPoint = value;
        Point center = Center();
        focusDistance = Distance(focusPoint, center);
        focusAngle = CalcAngle(focusPoint, center);
        RotationYAxis = 180 - Math.Cos(focusAngle * Math.PI / 180) * focusDistance/20;
        RotationXAxis = 0 - Math.Sin(focusAngle * Math.PI / 180) * focusDistance/20;
      }
    }

    #region -- Rotation --

    public static DependencyProperty RotationYProperty = DependencyProperty.Register(
            "RotationYAxis",
            typeof(double),
            typeof(Eye),
            new PropertyMetadata((double)180));

    public double RotationYAxis
    {
      get { return (double)GetValue(RotationYProperty); }
      set { SetValue(RotationYProperty, value); }
    }

    public static DependencyProperty RotationXProperty = DependencyProperty.Register(
        "RotationXAxis",
        typeof(double),
        typeof(Eye),
        new PropertyMetadata((double)0));

    public double RotationXAxis
    {
      get { return (double)GetValue(RotationXProperty); }
      set { SetValue(RotationXProperty, value); }
    }

    #endregion



    #endregion

    #region -- Constructor --

    /// <summary>
    /// Initializes the <see cref="Eye"/> class.
    /// </summary>
    static Eye()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Eye), new FrameworkPropertyMetadata(typeof(Eye)));
    }

    #endregion

    #region -- Public Methods --

    #region Center

    /// <summary>
    /// Return the Center point.
    /// </summary>
    /// <returns></returns>
    public Point Center()
    {
      try
      {

        Point center = PointToScreen(new Point(0, 0));
        center.Offset(ActualWidth / 2, ActualHeight / 2);
        return center;
      }
      catch (InvalidOperationException)
      {
        return new Point(0,0);
      }
    }

    #endregion

    #endregion

    #region -- Private Methods --

    #region CalcAngle

    private static double CalcAngle(Point p1, Point p2)
    {
      return (Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * 180.0 / Math.PI);
    }

    #endregion

    #region Distance

    private static double Distance(Point p1, Point p2)
    {
      double x = p2.X - p1.X;
      double y = p2.Y - p1.Y;
      return Math.Sqrt(x * x + y * y);
    }

    #endregion

    #endregion

  }
}
