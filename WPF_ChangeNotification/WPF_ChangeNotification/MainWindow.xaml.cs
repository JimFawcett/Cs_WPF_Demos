/////////////////////////////////////////////////////////////
// MainWindow.xaml.cs - WPF Change Notification            //
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
using System.ComponentModel;

namespace WPF_ChangeNotification
{
  public partial class MainWindow : Window
  {
    public course myCourse { get; set; }
    static int nameCount = 1;

    public MainWindow()
    {
      InitializeComponent();
      myCourse = new course("CSE681 - Software Modeling and Analysis");
      Grid1.DataContext = myCourse;
    }
    /*-- click handler changes course.Name property ---------*/

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      const int courseCount = 6;
      string[] names =
      {
        "CSE681 - Software Modeling and Analysis",
        "CSE686 - Internet Programming",
        "CSE687 - Object Oriented Design",
        "CSE775 - Distributed Objects",
        "CSE776 - Design Patterns",
        "CSE784 - Software Studio"
      };
      myCourse.Name = names[(nameCount++)%courseCount];
    }
  }
  /*-- class that implements property change notification ---*/

  public class course : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    /* Xaml will recognize this */
    public const string NamePropertyName = "Name";

    /* The Name Property */
    private string _name = string.Empty;

    public string Name
    {
      get { return _name; }
      set 
      { 
        _name = value;
        RaisePropertyChanged(NamePropertyName);
      }
    }
    /*-- Xaml needs a default constructor -------------------*/

    public course(string name)
    {
      Name = name;
    }
    public course()
    {
      Name = "CSE681";
    }
    /*-- Here's where the change notification happens -------*/

    public void RaisePropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)  // binding causes a subscription so not null
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}
