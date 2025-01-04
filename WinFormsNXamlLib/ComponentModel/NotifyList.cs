using System.Collections;
using System.Collections.Specialized;

namespace WinFormsNXaml.ComponentModel
{
    /// <summary>Represents a <see cref="IList{T}"/>, that notifies about collection changes through <see cref="INotifyCollectionChanged"/> interface</summary>
    /// <typeparam name="T"></typeparam>
    public class NotifyList<T> : IList<T>, INotifyCollectionChanged
    {
        /// <summary>Base collection to change</summary>
        protected IList<T> values;
        /// <summary>Initializes a new instance of the <see cref="NotifyList{T}"/> class through <see cref="List{T}"/>.</summary>
        public NotifyList() => values = [];
        /// <summary>Initializes a new instance of the <see cref="NotifyList{T}"/> class through <see cref="IList{T}"/>.</summary>
        /// <param name="values">Collection to changing</param>
        public NotifyList(IList<T> values) => this.values = values;
        /// <inheritdoc/>
        public virtual T this[int index]
        {
            get => values[index];
            set
            {
                T old = values[index];
                values[index] = value;
                OnCollectionChanged(this, new(NotifyCollectionChangedAction.Replace, value, old, index));
            }
        }
        /// <inheritdoc/>
        public virtual int Count => values.Count;
        /// <inheritdoc/>
        public virtual bool IsReadOnly => values.IsReadOnly;
        /// <inheritdoc/>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        /// <summary>Invokes <see cref="CollectionChanged"/></summary>>
        protected virtual void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => CollectionChanged?.Invoke(sender, e);
        /// <inheritdoc/>
        public virtual void Add(T item)
        {
            values.Add(item);
            OnCollectionChanged(this, new(NotifyCollectionChangedAction.Add, item, Count - 1));
        }
        /// <summary>Adds the elements of the specified collection to the end of the <see cref="NotifyList{T}"/></summary>
        public virtual void AddRange(IEnumerable<T> items)
        {
            if (values is List<T> list)
                list.AddRange(items);
            else
                foreach (T item in items)
                    values.Add(item);
            OnCollectionChanged(this, new(NotifyCollectionChangedAction.Add, items, Count - items.Count()));
        }
        /// <inheritdoc/>
        public virtual void Clear()
        {
            values.Clear();
            OnCollectionChanged(this, new(NotifyCollectionChangedAction.Reset));
        }
        /// <inheritdoc/>
        public virtual bool Contains(T item) => values.Contains(item);
        /// <inheritdoc/>
        public virtual void CopyTo(T[] array, int arrayIndex) => values.CopyTo(array, arrayIndex);
        /// <inheritdoc/>
        public virtual IEnumerator<T> GetEnumerator() => values.GetEnumerator();
        /// <inheritdoc/>
        public virtual int IndexOf(T item) => values.IndexOf(item);
        /// <inheritdoc/>
        public virtual void Insert(int index, T item)
        {
            T old = this[index];
            values.Insert(index, item);
            OnCollectionChanged(this, new(NotifyCollectionChangedAction.Add, item, old, index));
        }
        /// <inheritdoc/>
        public virtual bool Remove(T item)
        {
            int index = IndexOf(item);
            if (values.Remove(item))
            {
                OnCollectionChanged(this, new(NotifyCollectionChangedAction.Remove, item, index));
                return true;
            }
            return false;
        }
        /// <inheritdoc/>
        public virtual void RemoveAt(int index)
        {
            T old = this[index];
            values.RemoveAt(index);
            OnCollectionChanged(this, new(NotifyCollectionChangedAction.Remove, old, index));
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}