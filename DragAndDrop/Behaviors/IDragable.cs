using System;

namespace DragAndDrop.Behaviors
{
    internal interface IDragable
    {
        /// <summary>
        /// The type of the originating parent
        /// </summary>
        IDropable Source { get; set; }
    }

}
