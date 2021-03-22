using System;
using System.Collections;
using System.Collections.Generic;

namespace Shipstone.System.Collections
{
    public partial class FrequencyTable<T> : ICollection, ICollection<T>, IReadOnlyCollection<T>
    {
        public int Count => throw new NotImplementedException();
        public IEnumerable<int> Frequencies => throw new NotImplementedException();
        bool ICollection<T>.IsReadOnly => false;
        bool ICollection.IsSynchronized => false;
        public IEnumerable<T> Items => throw new NotImplementedException();
        public int MaxFrequency => throw new NotImplementedException();
        public int MinFrequency => throw new NotImplementedException();
        Object ICollection.SyncRoot => this;

        public int this[T item]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public FrequencyTable() => throw new NotImplementedException();
        public FrequencyTable(IEnumerable<T> collection) => throw new NotImplementedException();
        public FrequencyTable(FrequencyTable<T> table) => throw new NotImplementedException();

        public void Add(T item) => throw new NotImplementedException();
        public void Add(T item, int frequency) => throw new NotImplementedException();
        public void AddRange(IEnumerable<T> collection) => throw new NotImplementedException();
        public void Clear() => throw new NotImplementedException();
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
    }
}