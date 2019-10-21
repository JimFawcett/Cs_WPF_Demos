/*
<!-- ...................................................... -->
<!-- Window1.xaml.cs - Wpf_AttachedProperties               -->
<!--                   Demonstrates WPF Property System     -->
<!--                                                        -->
<!-- Jim Fawcett, CSE775 - Distributed Objects, Spring 2009 -->
<!-- ...................................................... -->
*/
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

namespace Wpf_AttachedProperties
{
  public partial class Window1 : Window
  {
    public Window1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Button clicked", "An Event");
    }

    private void button5_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Fill clicked", "An Event");
    }

    private void button3_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Right clicked", "An Event");
    }

    private void button2_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Left clicked", "An Event");
    }

    private void button4_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Top clicked", "An Event");
    }
  }
}
