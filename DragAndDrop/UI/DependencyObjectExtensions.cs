using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace DragAndDrop.Utils
{
    public static class DependencyObjectExtensions
    {
        public static T FindLogicalAncestor<T>(this DependencyObject obj)
            where T : DependencyObject
        {
            if (obj == null)
                return null;

            var dependObj = obj;
            do
            {
                dependObj = GetLogicalParent(dependObj);
                if (dependObj is T)
                    return dependObj as T;
            }
            while (dependObj != null);

            return null;
        }

        public static DependencyObject GetLogicalParent(this DependencyObject obj)
        {
            if (obj == null)
                return null;

            return LogicalTreeHelper.GetParent(obj);
        }


        public static T FindVisualAncestor<T>(this DependencyObject obj)
            where T : DependencyObject
        {
            if (obj == null)
                return null;

            var dependObj = obj;
            do
            {
                dependObj = GetVisualParent(dependObj);
                if (dependObj is T)
                    return dependObj as T;
            }
            while (dependObj != null);

            return null;
        }


        public static DependencyObject GetVisualParent(this DependencyObject obj)
        {
            if (obj == null)
                return null;

            if (obj is ContentElement)
            {
                var parent = ContentOperations.GetParent(obj as ContentElement);
                if (parent != null)
                    return parent;
                if (obj is FrameworkContentElement)
                    return (obj as FrameworkContentElement).Parent;
                return null;
            }

            return VisualTreeHelper.GetParent(obj);
        }
    }
}
