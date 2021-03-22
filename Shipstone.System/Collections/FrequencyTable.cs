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
        private const String _ClassName = "FrequencyTable<T>";

        private int _Count;
        private readonly IList<int> _Frequencies;
        private readonly IList<T> _Items;
        private int _MaxFrequency;
        private int _MinFrequency;

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

        /// <summary>
        /// Gets the maximum of frequency of all items contained in the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <value>The maximum of frequency of all items contained in the <see cref="FrequencyTable{T}" />, or 0 (zero) if the table is empty.</value>
        public int MaxFrequency => this._MaxFrequency;

        /// <summary>
        /// Gets the minimum of frequency of all items contained in the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <value>The minimum of frequency of all items contained in the <see cref="FrequencyTable{T}" />, or 0 (zero) if the table is empty.</value>
        public int MinFrequency => this._MinFrequency;

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

                this._ResetMaxMin();
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
            this._MaxFrequency = table._MaxFrequency;
            this._MinFrequency = table._MinFrequency;
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
            this._ResetMaxMin();
        }

        private bool _Remove(T item, int frequency)
        {
            int index = this._Items.IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            if (this._Frequencies[index] < frequency)
            {
                frequency = this._Frequencies[index];
            }

            if (this._Frequencies[index] == frequency)
            {
                this._Items.RemoveAt(index);
                this._Frequencies.RemoveAt(index);
            }
            
            else
            {
                this._Frequencies[index] -= frequency;
            }

            this._Count -= frequency;
            this._ResetMaxMin();
            return true;
        }

        private void _ResetMaxMin()
        {
            if (this._Count == 0)
            {
                this._MaxFrequency = this._MinFrequency = 0;
                return;
            }

            this._MinFrequency = Int32.MaxValue;
            this._MaxFrequency = 0;

            foreach (int frequency in this._Frequencies)
            {
                if (frequency < this._MinFrequency)
                {
                    this._MinFrequency = frequency;
                }

                if (frequency > this._MaxFrequency)
                {
                    this._MaxFrequency = frequency;
                }
            }
        }
#endregion

#region Public methods
        /// <summary>
        /// Adds an item to the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <param name="item">The item to be added to the <see cref="FrequencyTable{T}" />. The value can be <c>null</c> for reference types.</param>
        public void Add(T item) => this._Add(item, 1);

        /// <summary>
        /// Increases the frequency of the specified item in the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <param name="item">The item to increase the frequency of in the <see cref="FrequencyTable{T}" />. The value can be <c>null</c> for reference types.</param>
        /// <param name="frequency">The frequency to increase the current frequency of <c><paramref name="item" /></c> by.</param>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="frequency" /></c> is less than 0.</exception>
        public void Add(T item, int frequency)
        {
            if (frequency < 0)
            {
                throw new ArgumentOutOfRangeException(nameof (frequency));
            }

            if (frequency > 0)
            {
                this._Add(item, frequency);
            }
        }

        /// <summary>
        /// Adds the items of the specified collection to the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <param name="collection">The collection whose items should be added to the <see cref="FrequencyTable{T}" />. The collection itself cannot be <c>null</c>, but it can contain items that are <c>null</c>, if <c><typeparamref name="T" /></c> is a reference type.</param>
        /// <exception cref="ArgumentNullException"><c><paramref name="collection" /></c> is <c>null</c>.</exception>
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T item in collection ?? throw new ArgumentNullException(nameof (collection)))
            {
                this._Add(item, 1);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="FrequencyTable{T}" />.
        /// </summary>
        public void Clear()
        {
            this._Count = this._MaxFrequency = this._MinFrequency = 0;
            this._Frequencies.Clear();
            this._Items.Clear();
        }

        /// <summary>
        /// Determines whether an item is in the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="FrequencyTable{T}" />. The value can be <c>null</c> for reference types.</param>
        /// <returns><c>true</c> if <c><paramref name="item" /></c> is found in the <see cref="FrequencyTable{T}" />; otherwise, <c>false</c>.</returns>
        public bool Contains(T item) => this._Items.Contains(item);

        /// <summary>
        /// Determines whether all items contained in the specified collection are in the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <param name="collection">The collection to locate all items from in the <see cref="FrequencyTable{T}" />. The collection itself cannot be <c>null</c>, but it can contain items that are <c>null</c>, if <c><typeparamref name="T" /></c> is a reference type.</param>
        /// <returns><c>true</c> if all items in <c><paramref name="collection" /></c> are found in the <see cref="FrequencyTable{T}" />; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="collection" /></c> is <c>null</c>.</exception>
        public bool ContainsAll(IEnumerable<T> collection)
        {
            foreach (T item in collection ?? throw new ArgumentNullException(nameof (collection)))
            {
                if (!this._Items.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

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

        /// <summary>
        /// Creates a shallow copy of all items with the maximum frequency in the source <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <returns>A shallow copy of all items with the maximum frequency in the source <see cref="FrequencyTable{T}" />.</returns>
        /// <exception cref="InvalidOperationException">The <see cref="FrequencyTable{T}" /> is empty.</exception>
        public IEnumerable<T> GetMax()
        {
            if (this._Count == 0)
            {
                throw new InvalidOperationException($"The {FrequencyTable<T>._ClassName} is empty.");
            }

            ICollection<T> max = new List<T>();
            
            for (int i = 0; i < this._Items.Count; i ++)
            {
                if (this._Frequencies[i] == this._MaxFrequency)
                {
                    max.Add(this._Items[i]);
                }
            }

            return max;
        }

        /// <summary>
        /// Creates a shallow copy of all items with the minimum frequency in the source <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <returns>A shallow copy of all items with the minimum frequency in the source <see cref="FrequencyTable{T}" />.</returns>
        /// <exception cref="InvalidOperationException">The <see cref="FrequencyTable{T}" /> is empty.</exception>
        public IEnumerable<T> GetMin()
        {
            if (this._Count == 0)
            {
                throw new InvalidOperationException($"The {FrequencyTable<T>._ClassName} is empty.");
            }

            ICollection<T> min = new List<T>();
            
            for (int i = 0; i < this._Items.Count; i ++)
            {
                if (this._Frequencies[i] == this._MinFrequency)
                {
                    min.Add(this._Items[i]);
                }
            }

            return min;
        }

        public IEnumerable<T> GetRange(int frequency) => throw new NotImplementedException();
        public IEnumerable<T> GetRange(int minFrequency, int maxFrequency) => throw new NotImplementedException();

        /// <summary>
        /// Removes an item from the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <param name="item">The item to remove from the <see cref="FrequencyTable{T}" />. The value can be <c>null</c> for reference types.</param>
        /// <returns><c>true</c> if <c><paramref name="item" /></c> is successfully removed; otherwise, <c>false</c>. This method also returns <c>false</c> if <c><paramref name="item" /></c> was not found in the <see cref="FrequencyTable{T}" />.</returns>
        public bool Remove(T item) => this._Remove(item, 1);

        /// <summary>
        /// Decreases the frequency of the specified item in the <see cref="FrequencyTable{T}" />.
        /// </summary>
        /// <param name="item">The item to decrease the frequency of in the <see cref="FrequencyTable{T}" />. The value can be <c>null</c> for reference types.</param>
        /// <param name="frequency">The decrease to increase the current frequency of <c><paramref name="item" /></c> by.</param>
        /// <returns><c>true</c> if <c><paramref name="item" /></c> is successfully decreased by at most <c><paramref name="frequency" /></c>; otherwise, <c>false</c>. This method also returns <c>false</c> if <c><paramref name="item" /></c> was not found in the <see cref="FrequencyTable{T}" />.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="frequency" /></c> is less than 0.</exception>
        public bool Remove(T item, int frequency) => frequency < 0 ? throw new ArgumentOutOfRangeException(nameof (frequency)) : frequency > 0 && this._Remove(item, frequency);
        
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