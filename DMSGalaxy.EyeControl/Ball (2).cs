using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace EyeControl
{
  /// <summary>
  /// The Ball Class
  /// </summary>
  /// <remarks>
  /// Credits to ]Metty[ for the majority of this control.
  /// To see the original goto: 
  /// http://www.codeproject.com/KB/cs/Bouncing_Ball.aspx
  /// This control is re-created for WPF by Niel M. Thomas in 2010.
  /// This control is covered by the CPOL license.
  /// </remarks>
  [ToolboxBitmap(typeof(Ball))]
  public class Ball : Control
  {
    #region -- Members --
    private double moveX;
    private double moveY;
    private double x;
    private double y;
    private const double gravity = 0.1;
    private readonly Random rand;
    private Rect area;
    readonly DispatcherTimer timer = new DispatcherTimer();
    private int counter;
    #endregion

    #region -- Events --
    /// <summary>
    /// Occurs when [ball moved].
    /// </summary>
    public event EventHandler BallMoved;
    #endregion

    #region -- Properties --

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="Ball"/> is moving.
    /// </summary>
    /// <remarks>
    /// Is not visible in the designer, we do not want the ball to bounce around in the designer...
    /// </remarks>
    /// <value><c>true</c> if moving; otherwise, <c>false</c>.</value>
    [Category("Ball"), Description("Set or get if the Ball is moving."), Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool Moving { get; set; }

    [Category("Ball"), Description("Set or get the update interval in Milliseconds"), Browsable(true)]
    public TimeSpan UpdateInterval { 
      get { return timer.Interval; }
      set { timer.Interval = value; }
    }
    
    #endregion

    #region -- Constructor --
    /// <summary>
    /// Initializes a new instance of the <see cref="Ball"/> class.
    /// </summary>
    public Ball()
    {
      rand = new Random(Environment.TickCount);
      counter = 0;
      Moving = false;
      Width = 24;
      Height = 24;
      CalcArea();
      timer = new DispatcherTimer(DispatcherPriority.Normal) { Interval = TimeSpan.FromMilliseconds(10), IsEnabled = true };
      timer.Tick += delegate
        {
          timer.IsEnabled = false;
          if (!Moving) return;
          Tick();
          Margin = new Thickness(x, y, 0, 0);
          InvokeBallMoved(null);
          if (counter%200 == 0)
            CalcArea();
          timer.IsEnabled = true;
        };
    }

    /// <summary>
    /// Initializes the <see cref="Ball"/> class.
    /// </summary>
    static Ball()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Ball), new FrameworkPropertyMetadata(typeof(Ball)));
    }
    #endregion

    #region -- Public Methods --

    #region Center
    /// <summary>
    /// Return the center of the ball
    /// </summary>
    /// <returns></returns>
    public Point Center()
    {
      try
      {
        Point center = PointToScreen(new Point(0, 0));
        center.Offset(Width / 2, Height / 2);
        return center;
      }
      catch (InvalidOperationException)
      {
        return new Point(0, 0);
      }

    }
    #endregion

    #region Bounce
    /// <summary>
    /// Bounces this instance.
    /// </summary>
    public void Bounce()
    {
      moveX = (rand.NextDouble() + rand.NextDouble()) - 1;
      moveY = -(rand.NextDouble());
      moveX *= 50;
      moveY *= 50;
      x += moveX;
      y += moveY;
    }
    #endregion

    #region Tick
    /// <summary>
    /// Ticks this instance.
    /// </summary>
    public void Tick()
    {
      moveY += gravity;

      x += moveX;
      y += moveY;
      var parentWindow = Window.GetWindow(this);
      if (parentWindow != null)
         area = new Rect(new Point(0, 0), new Size(parentWindow.ActualWidth, parentWindow.ActualHeight));
      else
         area = new Rect();          
      
      //Check Collision
      if (x < 0)
      {
        x = 0;
        moveX = -moveX;
        moveX *= 0.75;
        moveY *= 0.95;
      }
      if (x > (area.Width - 15 - Width))
      {
        x = area.Width - 15 - Width;
        moveX = -moveX;
        moveX *= 0.75;
        moveY *= 0.95;
      }

      if (y < 0)
      {
        y = 0;
        moveY = -moveY;
        moveY *= 0.75;
        moveX *= 0.95;
      }
      if (y > (area.Height - 30 - Height))
      {
        y = area.Height - 30 - Height;
        moveY = -moveY;
        moveY *= 0.8;
        moveX *= 0.95;
      }

      if (Math.Abs(moveX) < 0.1 && Math.Abs(moveY) < 0.5 &&
        DateTime.Now.Second % 3 == 0 && y > area.Height - 1 - Height - 40)
      {
        Bounce();
      }
    }
    #endregion

    #region -- InvokeBallMoved --
    public void InvokeBallMoved(EventArgs e)
    {
      EventHandler handler = BallMoved;
      if (handler != null) handler(this, e);
    }
    #endregion

    #endregion

    #region -- Private Methods --

    private void CalcArea()
    {
      var parentWindow = Window.GetWindow(this);
      if (parentWindow != null)
        area = new Rect(new Point(0, 0), new Size(parentWindow.ActualWidth, parentWindow.ActualHeight));
      else
        area = new Rect();          

    }
    #endregion
  }
}
