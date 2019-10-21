/////////////////////////////////////////////////////////////////
// Window1.xaml.cs - Demonstrates use of DataTemplate to       //
//                   customize appearence of ListBox items     //
//                                                             //
// Jim Fawcett, CSE775 - Distributed Objects, Summer 2010      //
// Source:                                                     //
// Todd Miranda, windowsclient.net/learn/video.aspx?v=29384    //
/////////////////////////////////////////////////////////////////
/*
 * The original ideas for this demo were presented by Todd Miranda
 * in The www.Windowsclient.net video titled "Customize the Appearance
 * of ListBox ListItems in WPF".
 * 
 * I've added some functionality, modified some functionality,
 * and made cosmetic changes.
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

namespace HDI_WPF_ListItemTemplate_cs
{
  public partial class Window1 : Window
  {
    public Window1()
    {
      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      lstItems.Items.Add(new Course { CourseNumber = "CSE681", CourseName = "SMA", Semester = "Fall", Instructor = "Fawcett" });
      lstItems.Items.Add(new Course { CourseNumber = "CSE686", CourseName = "IP", Semester = "Spring", Instructor = "Fawcett" });
      lstItems.Items.Add(new Course { CourseNumber = "CSE687", CourseName = "OOD", Semester = "Spring", Instructor = "Fawcett" });
      lstItems.Items.Add(new Course { CourseNumber = "CSE775", CourseName = "DO", Semester = "Spring", Instructor = "Fawcett" });
      lstItems.Items.Add(new Course { CourseNumber = "CSE776", CourseName = "DP", Semester = "Fall", Instructor = "Fawcett" });
      lstItems.Items.Add(new Course { CourseNumber = "CSE784", CourseName = "SS", Semester = "Fall", Instructor = "Pratt-Szeliga" });

      lstItems.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
    }

    private void lstItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      string show = "You selected item #" + lstItems.SelectedIndex.ToString();
      MessageBox.Show(show, "Been clicked");
    }
  }

  public class Course
  {
    public string CourseNumber { get; set; }
    public string CourseName { get; set; }
    public string Semester { get; set; }
    public string Instructor { get; set; }
  }
}
