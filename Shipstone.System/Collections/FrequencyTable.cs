using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    /// <summary>
    /// Represents a strongly types frequency table of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    public partial class FrequencyTable<T> : ICollection, ICollection<T>, IReadOnlyCollection<T>
    {
        private int _Count;
        private readonly IList<int> _Frequencies;
        private readonly IList<T> _Items;

#region Properties
        /// <summary>
        /// Gets the number of items contained in the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <value>The number of items contained in the <see cref="FrequencyTable{T}" />.</value>
        public int Count => this._Count;

        /// <summary>
        /// Gets a collection containing the frequencies of all items contained in the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <value>A collection containing the frequencies of all items contained in the <see cref="FrequencyTable{T}" />.</value>
        public IEnumerable<int> Frequencies => this._Frequencies;

        bool ICollection<T>.IsReadOnly => false;
        bool ICollection.IsSynchronized => false;

        /// <summary>
        /// Gets a collection containing all items contained in the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <value>A collection containing all items contained in the <see cref="FrequencyTable{T}" />.</value>
        public IEnumerable<T> Items => this._Items;

        public int MaxFrequency => throw new NotImplementedException();
        public int MinFrequency => throw new NotImplementedException();
        Object ICollection.SyncRoot => this;

        /// <summary>
        /// Gets or sets the frequency of the specified item.
        /// </summary>
        /// <param name="item">The item of the frequency to get or set.</param>
        /// <value>The frequency of the specified item.</value>
        /// <exception cref="ArgumentOutOfRangeException">On a set operation, value is less than 0.</exception>
        public int this[T item]
        {
            get
            {
                int index = this._Items.IndexOf(item);
                return index == -1 ? 0 : this._Frequencies[index];
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                int index = this._Items.IndexOf(item);

                if (index == -1)
                {
                    if (value == 0)
                    {
                        return;
                    }

                    this._Items.Add(item);
                    this._Frequencies.Add(value);
                    this._Count += value;
                }
                
                else
                {
                    int frequency = this._Frequencies[index];

                    if (value == 0)
                    {
                        this._Items.RemoveAt(index);
                        this._Frequencies.RemoveAt(index);
                        this._Count -= frequency;
                    }

                    else
                    {
                        this._Frequencies[index] = value;
                        this._Count += value - frequency;
                    }
                }
            }
        }
#endregion

#region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FrequencyTable{T}" /> class that is empty.
        /// </summary>
        public FrequencyTable()
        {
            this._Frequencies = new List<int>();
            this._Items = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrequencyTable{T}" /> class that contains items copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose items are copied to the new frequency table.</param>
        /// <exception cref="ArgumentNullException"><c><paramref name="collection" /></c> is <c>null</c>.</exception>
        public FrequencyTable(IEnumerable<T> collection) : this()
        {
            foreach (T item in collection ?? throw new ArgumentNullException(nameof (collection)))
            {
                this._Add(item, 1);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrequencyTable{T}" /> class that contains items copied from the specified frequency table.
        /// </summary>
        /// <param name="table">The source table whose items are copied to the new frequency table.</param>
        /// <exception cref="ArgumentNullException"><c><paramref name="table" /></c> is <c>null</c>.</exception>
        public FrequencyTable(FrequencyTable<T> table)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof (table));
            }

            this._Count = table._Count;
            this._Frequencies = new List<int>(table._Frequencies);
            this._Items = new List<T>(table._Items);
        }
#endregion

#region Private methods
        private void _Add(T item, int frequency)
        {
            int index = this._Items.IndexOf(item);

            if (index == -1)
            {
                this._Items.Add(item);
                this._Frequencies.Add(frequency);
            }

            else
            {
                this._Frequencies[index] += frequency;
            }

            this._Count += frequency;
        }
#endregion

#region Public methods
        public void Add(T item) => throw new NotImplementedException();
        public void Add(T item, int frequency) => throw new NotImplementedException();
        public void AddRange(IEnumerable<T> collection) => throw new NotImplementedException();

        /// <summary>
        /// Removes all items from the <see cref="FrequencyTable{T}" />.
        /// </summary>
        public void Clear()
        {
            this._Count = 0;
            this._Frequencies.Clear();
            this._Items.Clear();
        }

        public bool Contains(T item) => throw new NotImplementedException();
        public bool ContainsRange(IEnumerable<T> collection) => throw new NotImplementedException();
        public void CopyTo(T[] array) => throw new NotImplementedException();
        public void CopyTo(T[] array, int arrayIndex) => throw new NotImplementedException();
        public void CopyTo(T[] array, int arrayIndex, int frequency) =>  throw new NotImplementedException();
        public void CopyTo(T[] array, int arrayIndex, int minFrequency, int maxFrequency) => throw new NotImplementedException();
        public void CopyTo(Array array) => throw new NotImplementedException();
        public void CopyTo(Array array, int arrayIndex) => throw new NotImplementedException();
        public void CopyTo(Array array, int arrayIndex, int frequency) =>  throw new NotImplementedException();
        public void CopyTo(Array array, int arrayIndex, int minFrequency, int maxFrequency) => throw new NotImplementedException();
        public bool Exists(Predicate<T> match) => throw new NotImplementedException();
        public bool Exists(Predicate<T> match, int frequency) => throw new NotImplementedException();
        public bool Exists(Predicate<T> match, int minFrequency, int maxFrequency) => throw new NotImplementedException();
        public T Find(Predicate<T> match) => throw new NotImplementedException();
        public T Find(Predicate<T> match, int frequency) => throw new NotImplementedException();
        public T Find(Predicate<T> match, int minFrequency, int maxFrequency) => throw new NotImplementedException();
        public IEnumerable<T> FindAll(Predicate<T> match) => throw new NotImplementedException();
        public IEnumerable<T> FindAll(Predicate<T> match, int frequency) => throw new NotImplementedException();
        public IEnumerable<T> FindAll(Predicate<T> match, int minFrequency, int maxFrequency) => throw new NotImplementedException();
        public void ForEach(Action<T> action) => throw new NotImplementedException();
        public void ForEach(Action<T> action, int frequency) => throw new NotImplementedException();
        public void ForEach(Action<T> action, int minFrequency, int maxFrequency) => throw new NotImplementedException();
        public IEnumerator<T> GetEnumerator() => throw new NotImplementedException();
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
        public IEnumerable<T> GetMax() => throw new NotImplementedException();
        public IEnumerable<T> GetMin() => throw new NotImplementedException();
        public IEnumerable<T> GetRange(int frequency) => throw new NotImplementedException();
        public IEnumerable<T> GetRange(int minFrequency, int maxFrequency) => throw new NotImplementedException();
        public bool Remove(T item) => throw new NotImplementedException();
        public bool Remove(T item, int frequency) => throw new NotImplementedException();
        public int RemoveAll(T item) => throw new NotImplementedException();
        public int RemoveAll(Predicate<T> match) => throw new NotImplementedException();
        public int RemoveRange(IEnumerable<T> collection) => throw new NotImplementedException();
        public int RemoveRange(int frequency) => throw new NotImplementedException();
        public int RemoveRange(int minFrequency, int maxFrequency) => throw new NotImplementedException();
        public void Swap(T a, T b) => throw new NotImplementedException();
        public void ToArray()  => throw new NotImplementedException();
        public void ToArray(int frequency) => throw new NotImplementedException();
        public void ToArray(int minFrequency, int maxFrequency) => throw new NotImplementedException();
#endregion
    }
}