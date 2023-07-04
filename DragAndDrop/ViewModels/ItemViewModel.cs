using DragAndDrop.Utils;
using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using DragAndDrop.Behaviors;

namespace DragAndDrop.ViewModels
{
    internal class ItemViewModel : ViewModelBase, IDragable
    {
		public IDropable Source { get; set; }

		private string displayName;
		public string DisplayName
		{
			get => displayName;
			set
			{
				displayName = value;
				OnPropertyChanged(nameof(DisplayName));
			}
		}

    }
}
