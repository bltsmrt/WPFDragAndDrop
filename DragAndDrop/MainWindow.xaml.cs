
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using DragAndDrop.Utils;
using DragAndDrop.ViewModels;


namespace DragAndDrop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListBox sourceBox;

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

            sourceBox = ((FrameworkElement)sender).FindVisualAncestor<ListBox>();
            if (sourceBox == null)
                return;

            // Package the data.
            DataObject data = new DataObject();
            data.SetData(fe.DataContext.GetType(), fe.DataContext);

            // Initiate the drag-and-drop operation.
            DragDrop.DoDragDrop(sourceBox, data, DragDropEffects.Move);
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
            ItemCollectionViewModel targetCollection = frameworkElement.DataContext as ItemCollectionViewModel;
            if (targetCollection == null) 
                return;

            // Get the data from the DragDrop event
            var item = e.Data.GetData(typeof(ItemViewModel)) as ItemViewModel;
            if (item == null)
                return;

            // Grab a reference to the source collection
            // Compare the source collection to the target collection.
            // If the same, we don't continue.
            ItemCollectionViewModel sourceCollection = sourceBox.DataContext as ItemCollectionViewModel;
            if (sourceCollection == null || sourceCollection == targetCollection)
                return;

            if (sourceCollection.Remove(item))
                targetCollection.Add(item);
        }
    }
}
