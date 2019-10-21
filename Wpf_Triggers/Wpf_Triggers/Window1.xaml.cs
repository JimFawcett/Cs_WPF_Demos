/*
<!-- ...................................................... -->
<!-- Window1.xaml.cs - Wpf_Triggers                         -->
<!--                   Demonstrates styles and triggers     -->
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

namespace Wpf_Triggers
{
  public partial class Window1 : Window
  {
    public Window1()
    {
      InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      textBox1.Text = "Been clicked";
    }

    private void Button_MouseEnter(object sender, MouseEventArgs e)
    {
      textBox1.Text = "Mouse near button -- Help!";
    }
  }
}
