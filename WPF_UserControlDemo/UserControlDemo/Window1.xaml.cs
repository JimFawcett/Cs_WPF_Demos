///////////////////////////////////////////////////////////////
// Window1.xaml.cs - Window hosting custom control           //
//                                                           //
// Jim Fawcett, CSE775 - Distributed Objects, Summer 2010    //
// Source:                                                   //
// Mark Berry, windowsclient.net/learn/video.aspx?v=76360    //
///////////////////////////////////////////////////////////////
/*
 * The original ideas for this demo were presented by Mark Berry
 * in The www.Windowsclient.net video titled "How to Create a
 * User Control in WPF".
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

namespace UserControlDemo
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
          TimerControl.ResetTimer();
        }
    }
}
