///////////////////////////////////////////////////////////////
// Window1.xaml.cs - Default Project Illustrates WPF basics  //
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

namespace Wpf_DefaultProject
{
  public partial class Window1 : Window
  {
    public Window1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      listBox1.Items.Insert(0,textBox1.Text);
      textBox1.Select(0,textBox1.Text.Length);
      textBox1.Focus();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      this.Title = "DefaultProject Application";
      textBox1.Text = Properties.Settings.Default.StartText;
      this.Height = Properties.Settings.Default.WindowHt;
      this.Width = Properties.Settings.Default.WindowWd;
      button1.Width = 100;
      button1.Height = 30;
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      Properties.Settings.Default.StartText = textBox1.Text;
      Properties.Settings.Default.WindowHt = Height;
      Properties.Settings.Default.WindowWd = Width;
      Properties.Settings.Default.Save();
    }
  }
}
