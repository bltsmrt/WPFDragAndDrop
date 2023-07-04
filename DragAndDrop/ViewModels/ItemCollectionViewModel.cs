using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;

namespace DragAndDrop.ViewModels
{
    internal class ItemCollectionViewModel
    {
        public ObservableCollection<ItemViewModel> Items { get; set; } =
            new ObservableCollection<ItemViewModel>();


        public void Add(ItemViewModel itemViewModel)
        {
            if (Items.Contains(itemViewModel)) return;
            Items.Add(itemViewModel);
        }

        public bool Remove(ItemViewModel itemViewModel)
        {
            if (!Items.Contains(itemViewModel))
                return false;

            return Items.Remove(itemViewModel);
        }
    }
}
