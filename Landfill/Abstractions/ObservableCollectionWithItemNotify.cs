using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Landfill.Abstractions
{
    public class ObservableCollectionWithItemNotify<T> : ObservableCollection<T> where T : ObservableObject
    {
        public ObservableCollectionWithItemNotify()
        {
            CollectionChanged += items_CollectionChanged;
        }

        public ObservableCollectionWithItemNotify(IEnumerable<T> collection) : base(collection)
        {
            CollectionChanged += items_CollectionChanged;
            foreach (INotifyPropertyChanged item in collection)
                item.PropertyChanged += item_PropertyChanged;

        }

        private void items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e != null)
            {
                if (e.OldItems != null)
                    foreach (INotifyPropertyChanged item in e.OldItems)
                        item.PropertyChanged -= item_PropertyChanged;

                if (e.NewItems != null)
                    foreach (INotifyPropertyChanged item in e.NewItems)
                        item.PropertyChanged += item_PropertyChanged;
            }
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected") return;
            //var replace = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender, Items.IndexOf((T)sender));
            //OnCollectionChanged(replace);
            var reset = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(reset);
        }
    }
}
