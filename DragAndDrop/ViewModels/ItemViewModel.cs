using DragAndDrop.Utils;
using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace DragAndDrop.ViewModels
{
    internal class ItemViewModel : ViewModelBase
    {
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
