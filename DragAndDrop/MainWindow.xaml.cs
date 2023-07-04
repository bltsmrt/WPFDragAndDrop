
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DragAndDrop.Behaviors;
using DragAndDrop.Utils;
using DragAndDrop.ViewModels;


namespace DragAndDrop
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


        // Called when the mouse moves over one of the 
        // TextBlocks displaying our items
        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            // If the mousebutton isn't pressed, return immediately;
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            // Cast the sender (TextBox) to a FrameworkElement
            // So we can grab the DataContext
            FrameworkElement fe = sender as FrameworkElement;
            if (fe == null) 
                return;

            // If dragable is null, this can't be dragged
            IDragable dragable = fe.DataContext as IDragable;
            if (dragable == null)
                return;


            IDropable source = fe.FindVisualAncestor<ListBox>()?.DataContext as IDropable;
            if (source == null)
                return;

            // Set the source on the dragable object so we don't have to 
            // save a reference in an arbitrary location
            dragable.Source = source;

            // Package the data.
            DataObject data = new DataObject();
            data.SetData(typeof(IDragable), dragable);

            // Initiate the drag-and-drop operation.
            DragDrop.DoDragDrop(fe, data, DragDropEffects.Move);
        }

        
        // Called when a drop action happens on a ListBox
        private void ListBox_Drop(object sender, DragEventArgs e) 
        {
            // Cast the sender (ListBox) to a FrameworkElement
            // So we can grab the DataContext
            FrameworkElement frameworkElement = sender as FrameworkElement;
            if (frameworkElement == null)
                return;

            // Grab a reference to the targetCollection
            IDropable targetDropable = frameworkElement.DataContext as IDropable;
            if (targetDropable == null) 
                return;

            // Get the data from the DragDrop event
            IDragable dragable = e.Data.GetData(typeof(IDragable)) as IDragable;
            if (dragable == null)
                return;

            if (dragable.Source == targetDropable)
                return;

            if (dragable.Source.Remove(dragable))
                targetDropable.Add(dragable);
        }
    }
}
