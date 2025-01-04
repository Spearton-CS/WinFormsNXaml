using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace WinFormsNXaml.ComponentModel
{
    /// /// <summary>Represents a <see cref="IDictionary{TKey, TValue}"/>, that notifies about collection changes through <see cref="INotifyCollectionChanged"/> interface</summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class NotifyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyCollectionChanged where TKey : notnull
    {
        /// <summary>Base collection to change</summary>
        protected IDictionary<TKey, TValue> values;
        /// <summary>Initializes a new instance of the <see cref="NotifyDictionary{TKey, TValue}"/> class through <see cref="Dictionary{TKey, TValue}"/>.</summary>
        public NotifyDictionary() => values = new Dictionary<TKey, TValue>();
        /// <summary>Initializes a new instance of the <see cref="NotifyDictionary{TKey, TValue}"/> class through <see cref="IDictionary{TKey, TValue}"/>.</summary>
        /// <param name="values">Collection to changing</param>
        public NotifyDictionary(IDictionary<TKey, TValue> values) => this.values = values;
        /// <inheritdoc/>
        public virtual TValue this[TKey key]
        {
            get => values[key];
            set => values[key] = value;
        }
        /// <inheritdoc/>
        public virtual ICollection<TKey> Keys => values.Keys;
        /// <inheritdoc/>
        public virtual ICollection<TValue> Values => values.Values;
        /// <inheritdoc/>
        public virtual int Count => values.Count;
        /// <inheritdoc/>
        public virtual bool IsReadOnly => values.IsReadOnly;
        /// <inheritdoc/>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        /// <summary>Invokes <see cref="CollectionChanged"/></summary>>
        protected virtual void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => CollectionChanged?.Invoke(sender, e);
        /// <inheritdoc/>
        public virtual void Add(TKey key, TValue value)
        {
            values.Add(key, value);
            OnCollectionChanged(this, new(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value), Count - 1));
        }
        /// <inheritdoc/>
        public virtual void Add(KeyValuePair<TKey, TValue> item)
        {
            values.Add(item);
            OnCollectionChanged(this, new(NotifyCollectionChangedAction.Add, item, Count - 1));
        }
        /// <inheritdoc/>
        public virtual void Clear()
        {
            values.Clear();
            OnCollectionChanged(this, new(NotifyCollectionChangedAction.Reset));
        }
        /// <inheritdoc/>
        public virtual bool Contains(KeyValuePair<TKey, TValue> item) => values.Contains(item);
        /// <inheritdoc/>
        public virtual bool ContainsKey(TKey key) => values.ContainsKey(key);
        /// <inheritdoc/>
        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => values.CopyTo(array, arrayIndex);
        /// <inheritdoc/>
        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => values.GetEnumerator();
        /// <inheritdoc/>
        public virtual bool Remove(TKey key)
        {
            if (values.Remove(key))
            {
                OnCollectionChanged(this, new(NotifyCollectionChangedAction.Remove, key));
                return true;
            }
            return false;
        }
        /// <inheritdoc/>
        public virtual bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (values.Remove(item))
            {
                OnCollectionChanged(this, new(NotifyCollectionChangedAction.Remove, item));
                return true;
            }
            return false;
        }
        /// <inheritdoc/>
        public virtual bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => values.TryGetValue(key, out value);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}