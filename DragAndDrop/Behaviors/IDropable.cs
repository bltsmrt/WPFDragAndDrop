using System;
using System.Collections;
using System.Collections.Generic;

namespace DragAndDrop.Behaviors
{
    /// <summary>
    /// Describes the behavior of an object that accepts drops from the user.
    /// </summary>
    internal interface IDropable 
    {
        bool Add(object data);
        bool Remove(object data);   
    }
}
