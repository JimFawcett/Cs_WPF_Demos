///////////////////////////////////////////////////////////////////
// MainWindow.xaml.cs  -   Demonstrates use of ControlTemplate   //
//                         and Styles to customize appearence    //
//                         of controls.                          //
//                                                               //
// Jim Fawcett, CSE775 - Distributed Objects, Summer 2010        //
///////////////////////////////////////////////////////////////////

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

namespace ControlTemplateDemo
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void Demo_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Been Clicked!");
    }

    private void StyledButton_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Styled Clicked too!");
    }
  }
}
