/////////////////////////////////////////////////////////////
// Window1.xaml.cs - RoutedEvent example                   //
//                                                         //
// Jim Fawcett, CSE775 - Distributed Objects, Spring 2008  //
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

namespace RoutedEvent
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class Window1 : Window
  {
    public Window1()
    {
      InitializeComponent();
      this.Title = "Routed Events Demo";
    }

    private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
    {
      this.Title = "Image load failed";
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("I've been clicked", "Handled by Button Click Handler");
    }

    private void button1_MouseEnter(object sender, MouseEventArgs e)
    {
      this.Title = "Button Element";
    }

    private void button1_MouseLeave(object sender, MouseEventArgs e)
    {
      this.Title = "Routed Events Demo";
    }

    private void Image_MouseEnter(object sender, MouseEventArgs e)
    {
      this.Title = "Image Element";
    }

    private void label1_MouseEnter(object sender, MouseEventArgs e)
    {
      this.Title = "Label Element";
    }

    private void label1_MouseLeave(object sender, MouseEventArgs e)
    {
      this.Title = "Button Element";
    }

    private void Image_MouseLeave(object sender, MouseEventArgs e)
    {
      this.Title = "Button Element";
    }
  }
}
