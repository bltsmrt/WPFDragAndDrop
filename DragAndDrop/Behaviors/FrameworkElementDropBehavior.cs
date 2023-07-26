using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Xaml.Behaviors;


namespace DragAndDrop.Behaviors
{
    internal class FrameworkElementDropBehavior : Behavior<FrameworkElement>
    {
        private IDropable dataContextAsDropable;

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.PreviewDragEnter += new DragEventHandler(AssociatedObject_DragEnter);
            this.AssociatedObject.PreviewDragOver += new DragEventHandler(AssociatedObject_DragOver);
            this.AssociatedObject.PreviewDragLeave += new DragEventHandler(AssociatedObject_DragLeave);
            this.AssociatedObject.Drop += new DragEventHandler(AssociatedObject_Drop);
        }


        void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            e.Handled = true;

            if (dataContextAsDropable == null)
                return;

            IDragable dragable = e.Data.GetData(typeof(IDragable)) as IDragable;
            if (dragable != null)
            {
                HandleSingleObjectDrop(dragable);
                return;
            }

            // Check for IEnumerable data
            else
            {
                IEnumerable list = e.Data.GetData(typeof(IEnumerable)) as IEnumerable;
                if (list == null)
                    return;

                HandleMultiObjectDrop(list);
            }
        }
        void HandleSingleObjectDrop(IDragable dragable)
        {
            if (dragable == null || dataContextAsDropable == dragable.Source)
                return;

            if (dataContextAsDropable.Add(dragable))
                dragable.Source.Remove(dragable);
        }

        void HandleMultiObjectDrop(IEnumerable list)
        {
            // We need to maintain a temporary reference to each item;
            // If we try to execute the Remove() action inside a single for loop,
            // We'll get an exception due to modifying the IEnumerable
            // we're attempting to iterate through.

            List<IDragable> itemsToRemoveFromSource = new List<IDragable>();

            foreach (object item in list)
            {
                if (item is IDragable dragableItem)
                {
                    dataContextAsDropable.Add(dragableItem);
                    itemsToRemoveFromSource.Add(dragableItem);
                }
            }

            foreach (IDragable dragableItem in itemsToRemoveFromSource)
            {
                dragableItem.Source.Remove(dragableItem);
            }
        }









        void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }



        void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.None;

            IDropable source = null;
            IDropable target = this.AssociatedObject.DataContext as IDropable;

            // Verify eligability
            bool canBeDropped = e.Data.GetDataPresent(typeof(IDragable));


            // If Single Object
            if (canBeDropped)
            {
                IDragable data = e.Data.GetData(typeof(IDragable)) as IDragable;
                source = data.Source;
            }

            // Check for multi-object
            else
            {
                if (e.Data.GetDataPresent(typeof(IEnumerable)))
                {
                    IEnumerable listData = e.Data.GetData(typeof(IEnumerable)) as IEnumerable;
                    foreach (object item in listData)
                    {
                        if (item is IDragable dragableItem)
                        {
                            canBeDropped = true;
                            source = dragableItem.Source;
                            break;
                        }
                    }
                }
            }

            if (canBeDropped == false)
                return;

            // Test to make sure we're not dropping back into the original list
            if (source == target)
                return;

            e.Effects = DragDropEffects.Move;
        }







        void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            if (this.AssociatedObject.DataContext == null)
                return;

            dataContextAsDropable = this.AssociatedObject.DataContext as IDropable;
            if (dataContextAsDropable == null)
                return;

            e.Handled = true;
        }
    }
}
