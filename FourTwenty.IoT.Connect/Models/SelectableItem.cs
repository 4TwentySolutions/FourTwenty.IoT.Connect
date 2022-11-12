using System;
using System.Collections.Generic;
using System.Text;

namespace FourTwenty.IoT.Connect.Models
{
    public class SelectableItem<T>
    {
        public T Item { get; set; }
        public bool IsSelected { get; set; }

        public SelectableItem() { }

        public SelectableItem(T item)
        {
            Item = item;
        }

    }
}
