/////////////////////////////////////////////////////////////
// MainWindow.xaml.cs - Demonstrates Canvas Panel          //
//                                                         //
// Jim Fawcett, CSE775 - Distributed Objects, Spring 2011  //
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

namespace CanvasPanel
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      StatusMsg.Text = "Button1 Clicked";
    }

    private void button1_MouseLeave(object sender, MouseEventArgs e)
    {
      StatusMsg.Text = "";
    }

    private void button2_Click(object sender, RoutedEventArgs e)
    {
      StatusMsg.Text = "Button2 Clicked";
    }

    private void button2_MouseLeave(object sender, MouseEventArgs e)
    {
      StatusMsg.Text = "";
    }

    private void button3_Click(object sender, RoutedEventArgs e)
    {
      StatusMsg.Text = "Button3 Clicked";
    }

    private void button3_MouseLeave(object sender, MouseEventArgs e)
    {
      StatusMsg.Text = "";
    }

    private void button4_Click(object sender, RoutedEventArgs e)
    {
      StatusMsg.Text = "Button4 Clicked";
    }

    private void button4_MouseLeave(object sender, MouseEventArgs e)
    {
      StatusMsg.Text = "";
    }

  }
}
