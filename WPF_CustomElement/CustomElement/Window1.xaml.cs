///////////////////////////////////////////////////////////////
// Window1.xaml.cs - Creating Custom UIElement               //
//                                                           //
// Jim Fawcett, CSE775 - Distributed Objects, Spring 2009    //
///////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomElement
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class Window1 : Window
  {
    public Window1()
    {
      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      Point org = new Point(25, 200);
      Point[] pts = 
        { 
          new Point(0, 0), new Point(50, 25), new Point(100, 75), 
          new Point(150, 120), new Point(200, 150), new Point(250, 120),
          new Point(300, 90), new Point(350, 70), new Point(400, 60) 
        };
      CustomPolyLine pl = new CustomPolyLine(org, pts);
      Content = pl;
    }
  }
  public class CustomPolyLine : FrameworkElement
  {
    private Polyline pl = new Polyline();
    //private Line xAxis = new Line();
    //private Line yAxis = new Line();
    private Point orgn = new Point();
    private Point first = new Point();
    private Point second = new Point();
    double hgt;
    double wdt;

    public CustomPolyLine(Point origin, Point[] pts)
    {
      pl.RenderTransform = new TranslateTransform();
      pl.Stroke = Brushes.Black;
      pl.StrokeThickness = 2;
      orgn = origin;
      var rt = pl.RenderTransform;
      var offsetX = rt.Value.OffsetX;
      var offsetY = rt.Value.OffsetY;
      rt.SetValue(TranslateTransform.XProperty, orgn.X + offsetX);
      rt.SetValue(TranslateTransform.YProperty, orgn.Y + offsetY);
      foreach (Point pt in pts)
      {
        pl.Points.Add(pt);
      }
    }

    protected override void OnRender(DrawingContext dc)
    {
      base.OnRender(dc);
      Pen pen = new Pen(pl.Stroke, pl.StrokeThickness);
      first.X = orgn.X; first.Y = 20;
      second.X = orgn.X; second.Y = 2*orgn.Y + 20;
      dc.DrawLine(pen, first, second);
      first = orgn;
      second.X = pl.Points[pl.Points.Count - 1].X + orgn.X;
      second.Y = orgn.Y;
      dc.DrawLine(pen, first, second);

      for(int i=1; i<pl.Points.Count; ++i)
      {
        first.X = orgn.X + pl.Points[i - 1].X;
        first.Y = orgn.Y - pl.Points[i - 1].Y;
        second.X = orgn.X + pl.Points[i].X;
        second.Y = orgn.Y - pl.Points[i].Y;
        dc.DrawLine(pen, first, second);
      }
    }
  }
}
