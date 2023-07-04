using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DragAndDrop.ViewModels
{

    internal interface ICollectionViewModel
    {
        ICollection Items { get; }
        void Add(object item);
        bool Remove(object item);
    }

    internal interface ICollectionViewModel<T> : ICollectionViewModel where T : ViewModelBase
    {
        new ObservableCollection<T> Items { get; }
        void Add(T item);
        bool Remove(T item);

    }


    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
