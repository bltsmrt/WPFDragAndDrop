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

            if (dragable == null || dataContextAsDropable == dragable.Source)
                return;

            if (dataContextAsDropable.Add(dragable))
                dragable.Source.Remove(dragable);
            
        }


        void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }



        void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
            e.Effects = DragDropEffects.None;
            IDropable target = this.AssociatedObject.DataContext as IDropable;


            //if item can be dropped
            if (e.Data.GetDataPresent(typeof(IDragable)) == false)
                return;

            var data = e.Data.GetData(typeof(IDragable));
            if (data is IDragable dragable && dragable.Source != target)
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
