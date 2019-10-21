///////////////////////////////////////////////////////////////
// App.xaml.cs - Default Project Illustrates WPF basics      //
//                                                           //
// Jim Fawcett, CSE775 - Distributed Objects, Spring 2009    //
///////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Wpf_DefaultProject
{
  public partial class App : Application
  {
    App()
    {
      this.Startup += new StartupEventHandler(App_Startup);
      this.Exit += new ExitEventHandler(App_Exit);
    }

    void App_Exit(object sender, ExitEventArgs e)
    {
      // MessageBox.Show("App exit");
    }

    void App_Startup(object sender, StartupEventArgs e)
    {
      // MessageBox.Show("App startup");
    }
  }
}
