/////////////////////////////////////////////////////////////
// Window1.xaml.cs - Bar Chart Demo in WPF                 //
//                                                         //
// Jim Fawcett, CSE775 - Distributed Objects, Spring 2009  //
/////////////////////////////////////////////////////////////

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

namespace BarChart
{
  public partial class Window1 : Window
  {
    private double xAxisAlt;

    public Window1()
    {
      InitializeComponent();
    }

    private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      // scale axes to fit new canvas size

      double oldY = xAxis.Y1;
      xAxis.X1 = 50;
      xAxis.X2 = plot.ActualWidth - 50;
      xAxis.Y1 = xAxis.Y2 = (plot.ActualHeight) / 2;
      yAxis.X1 = yAxis.X2 = 50;
      yAxis.Y1 = 20;
      yAxis.Y2 = plot.ActualHeight - 20;

      // move rectangles to maintain position on axes

      for (int i = 0; i < plot.Children.Count; ++i)
      {
        UIElement child = plot.Children[i];
        Type t = child.GetType();
        if (t != typeof(Line))
        {
          // reposition using RenderTransform

          var rt = child.RenderTransform;
          var offsetY = rt.Value.OffsetY;
          rt.SetValue(TranslateTransform.YProperty, offsetY - xAxis.Y1 + oldY);

          ////////////////////////////////////////////////////////////////////
          // reposition by measuring and setting coordinates - This works too!
          //
          // double y = Canvas.GetBottom(child);
          // double hgt = (double)child.GetValue(HeightProperty);
          // if (y < oldY)
          //   Canvas.SetBottom(child, xAxis.Y1 - hgt + 1 - xAxis.StrokeThickness / 2);
          // else
          //   Canvas.SetBottom(child, xAxis.Y1 - 1 + xAxis.StrokeThickness / 2);
        }
      }
   }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      // scale X and Y axes to fit canvas

      xAxis.X1 = 50;
      xAxis.X2 = plot.ActualWidth - 50;
      xAxis.Y1 = xAxis.Y2 = (plot.ActualHeight) / 2;
      yAxis.X1 = yAxis.X2 = 50;
      yAxis.Y1 = 20;
      yAxis.Y2 = plot.ActualHeight - 20;
      xAxisAlt = xAxis.Y1;

      // load rectangles into canvas

      string[] names = { "r1", "r2", "r3", "r4", "r5" };
      double[] hght = { 50, 80, -30, 10, -50 };
      for (int i = 0; i < names.Length; ++i)
      {
        Rectangle rct = new Rectangle();
        rct.RenderTransform = new TranslateTransform();
        rct.Name = names[i];
        rct.Width = 20;
        rct.Height = Math.Abs(hght[i]);
        rct.Fill = Brushes.Beige;
        rct.Stroke = Brushes.Black;
        rct.ToolTip = rct.Name;

        // place rectangles in Axes

        if (hght[i] > 0)
          Canvas.SetBottom(rct, xAxis.Y1 -1 + xAxis.StrokeThickness/2);
        else
          Canvas.SetBottom(rct, xAxis.Y1 - rct.Height + 1 - xAxis.StrokeThickness/2);
        Canvas.SetLeft(rct, xAxis.X1 + 25 * i);
        plot.Children.Add(rct);
      }
    }
  }
}
