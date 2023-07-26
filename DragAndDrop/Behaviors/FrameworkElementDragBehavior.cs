using System;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Windows.Controls.Primitives;
using System.Collections;
using Microsoft.Xaml.Behaviors;
using QueryContinueDragEventArgs = System.Windows.QueryContinueDragEventArgs;

using DragAndDrop.Utils;

namespace DragAndDrop.Behaviors
{
    public class FrameworkElementDragBehavior : Behavior<FrameworkElement>
    {
        private bool isMouseClicked = false;
        private Selector parent;
        private IDropable parentSource;

  
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.MouseLeftButtonDown +=
                 new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown);
            this.AssociatedObject.MouseLeftButtonUp +=
                 new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonUp);
            this.AssociatedObject.MouseLeave +=
                 new MouseEventHandler(AssociatedObject_MouseLeave);


            // We iterate through the visual tree to find the Selector element that houses this element
            parent = AssociatedObject.FindVisualAncestor<Selector>();

            // If the Source DataContext isn't IDropable, we don't expect to be able to drag from it.
            parentSource = parent.DataContext as IDropable;
        }


        void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isMouseClicked = true;


            //Multi select will require that this object handle mouse - down events completely.
            if (parent.SelectedItem == this.AssociatedObject.DataContext ||
                (parent is ListBox lb &&
                  lb.SelectedItems.Contains(this.AssociatedObject.DataContext)))
            {
                e.Handled = true;
            }
        }

        void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseClicked = false;

        }

        void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isMouseClicked == false || parent == null || parentSource == null)
                return;

            // Set the item's DataContext as the data to be transferred
            IDragable dragObject = this.AssociatedObject.DataContext as IDragable;
            if (dragObject == null)
                return;

            if (parent is ListBox listBox && 
                listBox.SelectedItems.Count > 1)
            {
                HandleMultiObjectDrag(dragObject, listBox);
            }
            else
            {
                HandleSingleObjectDrag(dragObject);
            }

            isMouseClicked = false;
        }


        void HandleSingleObjectDrag(IDragable dragObject)
        {
            // Set the source object on the item being dragged.
            dragObject.Source = parentSource;

            // Set the DataObject
            DataObject data = new DataObject();
            data.SetData(typeof(IDragable), dragObject);

            // System.Windows implementation
            DragDrop.DoDragDrop(this.AssociatedObject, data, DragDropEffects.Move);
        }


        // NOTE:
        // This code does assume that the items contained in the ListBox
        // Are all of the same type.
        void HandleMultiObjectDrag(IDragable dragObject, ListBox listBox)
        {
            // Grab a reference to ALL of the selected items.
            var dataObjects = listBox.SelectedItems;

            // Set the parent source on all objects in the set.
            foreach (var obj in dataObjects)
                if (obj is IDragable dragable)
                    dragable.Source = this.parentSource;

            // Set the DataObject
            DataObject data = new DataObject();
            data.SetData(typeof(IEnumerable), dataObjects);

            // System.Windows implementation
            DragDrop.DoDragDrop(this.AssociatedObject, data, DragDropEffects.Move);
        }


    }
}
