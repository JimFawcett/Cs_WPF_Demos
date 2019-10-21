/////////////////////////////////////////////////////////////
// MainWindow.xaml.cs - WPF Dispatcher Demo                //
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
using System.Windows.Forms;
using System.IO;

namespace WPF_DispatcherDemo
{
  public partial class MainWindow : Window
  {
    List<string> foundfiles = new List<string>();
    IAsyncResult cbResult;

    public MainWindow()
    {
      InitializeComponent();
    }
    /*-- invoke on UI thread --------------------------------*/

    void showPath(string path)
    {
      textBlock1.Text = path;
    }
    /*-- invoke on UI thread --------------------------------*/

    void addFile(string file)
    {
      //listBox1.Items.Add(file);
      listBox1.Items.Insert(0,file);
    }
    /*-- recursive search for files matching pattern --------*/

    void search(string path, string pattern)
    {
      /* called on asynch delegate's thread */
      if (Dispatcher.CheckAccess())
        showPath(path);
      else
        Dispatcher.Invoke(
          new Action<string>(showPath),
          System.Windows.Threading.DispatcherPriority.Background,
          new string[] { path }
        );
      string[] files = System.IO.Directory.GetFiles(path, pattern);
      foreach (string file in files)
      {
        if (Dispatcher.CheckAccess())
          addFile(file);
        else
          Dispatcher.Invoke(
            new Action<string>(addFile),
            System.Windows.Threading.DispatcherPriority.Background,
            new string[] { file }
          );
      }
      string[] dirs = System.IO.Directory.GetDirectories(path);
      foreach (string dir in dirs)
        search(dir, pattern);
    }
    /*-- Start search on asynchronous delegate's thread -----*/

    private void FindButton_Click(object sender, RoutedEventArgs e)
    {
      listBox1.Items.Clear();
      FolderBrowserDialog dlg = new FolderBrowserDialog();
      string path = AppDomain.CurrentDomain.BaseDirectory;
      dlg.SelectedPath = path;
      DialogResult result = dlg.ShowDialog();
      if (result == System.Windows.Forms.DialogResult.OK)
      {
        path = dlg.SelectedPath;
        string pattern = "*.cs";
        Action<string,string> proc = this.search;
        cbResult = proc.BeginInvoke(path, pattern, null, null);
      }
    }
  }
}
