/////////////////////////////////////////////////////////////////
// Timer.xaml.cs - custom timer control, uses DispatcherTimer  //
//                                                             //
// Jim Fawcett, CSE775 - Distributed Objects, Summer 2010      //
// Source:                                                     //
// Mark Berry, windowsclient.net/learn/video.aspx?v=76360      //
/////////////////////////////////////////////////////////////////
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
using System.Windows.Threading;

namespace UserControlDemo
{
    public partial class Timer : UserControl
    {
        private int milliSeconds = 0;
        private DispatcherTimer t = new DispatcherTimer();

        public Timer()
        {
            InitializeComponent();
            t.Interval = new TimeSpan(0,0,0,0,100);
            t.Tick += new EventHandler(t_Tick);
        }

        void t_Tick(object sender, EventArgs e)
        {
            milliSeconds = milliSeconds + 100;
            _ElapsedTime.Text = milliSeconds.ToString();
        }

        private void StartTimer(object sender, RoutedEventArgs e)
        {
            t.Start();
        }

        private void StopTimer(object sender, RoutedEventArgs e)
        {
            t.Stop();
        }

        public void ResetTimer()
        {
          milliSeconds = 0;
          _ElapsedTime.Text = 0.ToString();
        }
    }
}
