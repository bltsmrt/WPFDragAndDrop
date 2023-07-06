using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;
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
            dragObject.Source = parentSource;

            if (dragObject == null)
                return;

            // Set the DataObject
            DataObject data = new DataObject();
            data.SetData(typeof(IDragable), dragObject);


            // System.Windows implementation
            DragDrop.DoDragDrop(this.AssociatedObject, data, DragDropEffects.Move);

            isMouseClicked = false;
        }
    }
}
