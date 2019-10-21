///////////////////////////////////////////////////////////////////////
// MainWindow.xaml.cs
// ver 1.0                                                           //
// Language:      C#, 2011, .Net Framework 4.0                       //
// Platform:      Lenovo Y410, Win7, SP1                             //
// Application:   CSE 681- Project #4 Help                           //
// Date:          Fall 2012                                            //
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DragDropExample
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


        
        
        
        
        
        
        // Drop functionality
        private void canvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Handled == false)
            {
                Canvas _panel = (Canvas)sender; // get the current canvas
                UIElement _element = (UIElement)e.Data.GetData("Object");//get the information
                string keyValue = (string)e.Data.GetData("AnyKey");     // get any key value
                if (_panel != null && _element != null)
                {
                    Panel _parent = (Panel)VisualTreeHelper.GetParent(_element);
                    if (_parent != null)
                    {
                        if (e.AllowedEffects.HasFlag(DragDropEffects.Move) && _parent.Name == "drawingCanvas")
                        {
                            if (_element is PackageDiagram)
                            {
                                Canvas.SetLeft(_element, e.GetPosition(_panel).X - 50);
                                Canvas.SetTop(_element, e.GetPosition(_panel).Y - 50);
                            }
                            if (_element is UsingConnector)
                            {
                                Canvas.SetLeft(_element, e.GetPosition(_panel).X - 20);
                                Canvas.SetTop(_element, e.GetPosition(_panel).Y - 20);
                            }
                        }
                        else
                        {
                            if (_element is PackageDiagram)
                            {
                                PackageDiagram _package = new PackageDiagram((PackageDiagram)_element);
                                Canvas.SetLeft(_package, e.GetPosition(_panel).X - 50);
                                Canvas.SetTop(_package, e.GetPosition(_panel).Y - 50);
                                _panel.Children.Add(_package);
                            }

                            if (_element is UsingConnector)
                            {
                                UsingConnector _conn = new UsingConnector((UsingConnector)_element);
                                Canvas.SetLeft(_conn, e.GetPosition(_panel).X-20);
                                Canvas.SetTop(_conn, e.GetPosition(_panel).Y-20);
                                _panel.Children.Add(_conn);
                            }
                            e.Effects = DragDropEffects.Move;
                        }
                    }
                }
            }
        }






    }
}