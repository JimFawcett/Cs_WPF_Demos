/////////////////////////////////////////////////////////////
// MainWindow.xaml - Demonstrate Message Hooking           //
// Ver 1.1                                                 //
// Jim Fawcett, CSE775 - Distributed Objects, Spring 2011  //
/////////////////////////////////////////////////////////////
/*
 * Ver 1.1 - 25 Sep 14
 * - fixed timing bug that showed up in Visual Studio 2013 with Win 8.1
 *   See comments below.
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
using System.Windows.Interop;

namespace MessageHook
{
  public partial class MainWindow : Window
  {
    int RenderCount;
    HwndSource src = null;
    class Win32MsgForDisplay
    {
      public string msg { get; set; }
      public string value { get; set; }
      public string renderCount { get; set; }
    }
    class Win32Msg
    {
      public string msg { get; set; }
      public int value { get; set; }
    }
    List<Win32Msg> msgs = new List<Win32Msg>
    {
      /* Windows messages defined in winuser.h */
      new Win32Msg { msg="WM_CREATE", value=0X0001 },
      new Win32Msg { msg="WM_DESTROY", value=0x0002 },
      new Win32Msg { msg="WM_MOVE", value=0x0003 },
      new Win32Msg { msg="WM_MOUSEMOVE", value=0x0200 },
      new Win32Msg { msg="WM_LBUTTONDOWN", value=0x0201 },
      new Win32Msg { msg="WM_LBUTTONUP", value=0x0202 },
      new Win32Msg { msg="WM_RBUTTONDOWN", value=0x0204 },
      new Win32Msg { msg="WM_RBUTTONUP", value=0x0205 },
      new Win32Msg { msg="WM_KEYDOWN", value=0x0100 },
      new Win32Msg { msg="WM_KEYUP", value=0x0101 },
      new Win32Msg { msg="WM_PAINT", value=0x000F }
    };
    string MessageNameLookup(int msgVal)
    {
      foreach(Win32Msg msg in msgs)
        if (msg.value == msgVal)
          return msg.msg;
      return "";
    }
    /*-- Initialize Window and Subscribe for events ---------*/

    public MainWindow()
    {
      InitializeComponent();

      /* Set Window Message Hook */
      this.SourceInitialized += new EventHandler(OnSourceInitialized);

      /* Subscribe for Rendering event */
      RenderCount = 0;
      CompositionTarget.Rendering += new EventHandler(TrackRenderCount);
    }
    /*-- Set Windows Message Hook ---------------------------*/

    void OnSourceInitialized(object sender, EventArgs ev)
    {
      src = PresentationSource.FromVisual(this) as HwndSource;

      // Have to wait to add hook until Window has completed loading
      //src.AddHook(new HwndSourceHook(HandleMessages));
    }
    /*-- Hook to Handle Win32 Messages ----------------------*/
 
    IntPtr HandleMessages(IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
      string name = MessageNameLookup(message);
      if (name != "")
      {
        // don't show mouse messages unless user requests it

        if (name != "WM_MOUSEMOVE" | (bool)checkBox1.IsChecked)
        {
          Win32MsgForDisplay msg = new Win32MsgForDisplay();
          msg.msg = name;
          msg.value = String.Format("{0:X}",message);
          msg.renderCount = String.Format("{0}",RenderCount);
          listBox1.Items.Insert(1, msg);
        }
      }
      return System.IntPtr.Zero;
    }
    /*-- Remove all ListBox items except the first ----------*/

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      while (listBox1.Items.Count > 1)
        listBox1.Items.RemoveAt(1);
    }
    /*-- Count number of Rendering Operations ---------------*/

    void TrackRenderCount(object sender, EventArgs ev)
    {
      ++RenderCount;
    }
    /*-- Initialize ListBox display -------------------------*/

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      Win32MsgForDisplay msg = new Win32MsgForDisplay();
      msg.msg = "Message";
      msg.value = String.Format("{0}", "Msg Value");
      msg.renderCount = String.Format("{0}", "Render Count");
      listBox1.Items.Insert(0, msg);

      // Had to wait to add hook until after the window was completed
      src.AddHook(new HwndSourceHook(HandleMessages));
    }
  }
}
