///////////////////////////////////////////////////////////////////////
// UsingConnector.xaml.cs
// ver 1.0                                                           //
// Language:      C#, 2011, .Net Framework 4.0                       //
// Platform:      Lenovo Y410, Win7, SP1                             //
// Application:   CSE 681- Project #4 Help                           //
// Date:          Fall 2012                                          //
// Author:        Vijay Ramakrishnan,                                //
//                viramakr@syr.edu                                   //
// Reference : http://msdn.microsoft.com/en-us/library/hh144799.aspx //
///////////////////////////////////////////////////////////////////////

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
using System.Windows.Shapes;

namespace DragDropExample
{
    /// <summary>
    /// Interaction logic for Line.xaml
    /// </summary>
    public partial class UsingConnector : UserControl
    {
        public UsingConnector()
        {
            InitializeComponent();
        }

        public UsingConnector(UsingConnector c)
        {
            InitializeComponent();

        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData("Object", this);
                data.SetData("AnyKey", "AnyData_Which_You want_to_Pass");

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }

        }
 
    }
}
