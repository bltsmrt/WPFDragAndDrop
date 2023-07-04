using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDrop.ViewModels
{

    internal class ViewModel : ViewModelBase
    {
        public ItemCollectionViewModel ColumnA { get; set; }
        public ItemCollectionViewModel ColumnB { get; set; }

        public ViewModel()
        {
            this.ColumnA = new ItemCollectionViewModel();
            this.ColumnB = new ItemCollectionViewModel();

            this.ColumnA.Add(new ItemViewModel { DisplayName = "Item 1" });
            this.ColumnA.Add(new ItemViewModel { DisplayName = "Item 2" });
            this.ColumnA.Add(new ItemViewModel { DisplayName = "Item 3" });
            this.ColumnA.Add(new ItemViewModel { DisplayName = "Item 4" });
        }

    }
}
