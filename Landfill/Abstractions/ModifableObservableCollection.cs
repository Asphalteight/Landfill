using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Landfill.Abstractions
{
    public class ModifableObservableCollection<T> : ObservableCollection<T> where T : ModifableObject
    {
        private IList<T> _originalList = [];
        private bool _endOfOriginalList;
        public bool IsModified { get; set; }

        public ModifableObservableCollection()
        {
            CollectionChanged += OnCollectionChanged;
        }

        public ModifableObservableCollection(IEnumerable<T> collection) : base(collection)
        {
            CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e != null)
            {
                if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems.OfType<T>().First().IsNew == false)
                {
                    _originalList.Add(e.NewItems.OfType<T>().First());
                }
                else if (_originalList.Count > 0)
                {
                    _endOfOriginalList = true;
                }
                if (_endOfOriginalList)
                {
                    IsModified = !_originalList.SequenceEqual(Items);
                }
            }
        }
    }
}
