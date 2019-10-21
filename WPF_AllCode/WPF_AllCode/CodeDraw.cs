///////////////////////////////////////////////////////////////////////
// CodeDraw.cs - Demonstrate that WPF applications can be defined    //
//               entirely in code.                                   //
//                                                                   //
// Jim Fawcett, CSE681 - Software Modeling and Analysis, Summer 2009 //
///////////////////////////////////////////////////////////////////////

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF_AllCode
{
  class CodeDraw : Window
  {
    private Canvas canvas = new Canvas();
    private Rectangle rect = new Rectangle();
    private TextBlock textBlock1 = new TextBlock();
    private TextBlock textBlock2 = new TextBlock();
    private Line line = new Line();
    private Polyline poly = new Polyline();
    private bool sizeChanged = false;
    private bool mouseWasDown = false;

    [STAThread]
    static void Main(string[] args)
    {
      Application App = new Application();
      App.Run(new CodeDraw());
    }
    public CodeDraw()
    {
      Title = "CodeDraw";

      // use absolute positioning on a Canvas element

      Canvas canvas = new Canvas();
      
      // add event handlers for Canvas

      canvas.SizeChanged += new SizeChangedEventHandler(canvas_SizeChanged);
      canvas.MouseDown += new MouseButtonEventHandler(canvas_MouseDown);
      canvas.MouseUp += new MouseButtonEventHandler(canvas_MouseUp);
      canvas.MouseEnter += new MouseEventHandler(canvas_MouseEnter);
      canvas.MouseLeave += new MouseEventHandler(canvas_MouseLeave);

      // add canvas to the Window Content

      Content = canvas;

      // define a rectangle
      rect.Height = 300;
      rect.Width = 500;
      rect.Fill = Brushes.PowderBlue;

      // define main TextBlock
      textBlock1.Text = "Hello CSE681";
      textBlock1.FontSize = 22;
      textBlock1.FontWeight = FontWeights.Bold;

      // define a change notice display TextBlock
      textBlock2.Text = "";
      textBlock2.FontSize = 22;
      textBlock2.FontWeight = FontWeights.Bold;
      textBlock2.Visibility = Visibility.Hidden;

      // draw line under textBlock1
      line.X1 = 145;
      line.Y1 = 185;
      line.X2 = 300;
      line.Y2 = 185;
      line.Stroke = Brushes.Black;
      line.StrokeThickness = 4;

      // draw lines along left and bottom of rect
      poly.Stroke = Brushes.DarkBlue;
      poly.StrokeThickness = 2;
      poly.Points.Add(new Point(0, 0));
      poly.Points.Add(new Point(10, 10));
      poly.Points.Add(new Point(10, 290));
      poly.Points.Add(new Point(490, 290));
      poly.Points.Add(new Point(500, 300));

      // add the children, created above, to canvas
      canvas.Children.Add(rect);
      Canvas.SetLeft(rect, 100);
      Canvas.SetTop(rect, 100);

      canvas.Children.Add(textBlock1);
      Canvas.SetLeft(textBlock1, 150);
      Canvas.SetTop(textBlock1, 150);

      canvas.Children.Add(textBlock2);
      Canvas.SetLeft(textBlock2, 150);
      Canvas.SetTop(textBlock2, 250);

      canvas.Children.Add(line);

      canvas.Children.Add(poly);
      Canvas.SetTop(poly, 100);
      Canvas.SetLeft(poly, 100);
    }

    void canvas_MouseLeave(object sender, MouseEventArgs e)
    {
      textBlock2.Visibility = Visibility.Hidden;
    }

    void canvas_MouseEnter(object sender, MouseEventArgs e)
    {
      textBlock2.Visibility = Visibility.Visible;
    }

    void canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
      textBlock2.Text = "Mouse up";
    }

    void canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
      mouseWasDown = true;
      textBlock2.Text = "Mouse down";
    }

    void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (mouseWasDown)
        textBlock2.Text = "Canvas size changed";
      else
        textBlock2.Text = "";
    }
  }
}
