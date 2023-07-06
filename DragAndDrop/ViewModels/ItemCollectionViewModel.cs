using DragAndDrop.Behaviors;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;

namespace DragAndDrop.ViewModels
{
    internal class ItemCollectionViewModel : ViewModelBase, IDropable
    {
        public ObservableCollection<ItemViewModel> Items { get; set; } =
            new ObservableCollection<ItemViewModel>();

        public bool Add(object item)
        {
            if (item is ItemViewModel itemViewModel)
            {

                if (Items.Contains(itemViewModel))
                    return false;

                Items.Add(itemViewModel);
                return true;
            }

            return false;
        }

        public bool Remove(object item)
        {
            if (item is ItemViewModel itemViewModel)
            {
                if (!Items.Contains(itemViewModel))
                    return false;

                return Items.Remove(itemViewModel);
            }

            return false;
        }
    }
}
