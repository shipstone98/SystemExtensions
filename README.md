# SystemExtensions

Extension classes to .NET Standard 2.0.

## Namespaces
- Shipstone.System.Collections: contains classes that define generic collections, which allows users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections

## Shipstone.System.Collections
- FrequencyTable<T>: represents a strongly types frequency table of items

### FrequencyTable<T> : ICollection, ICollection<T>, IReadOnlyCollection<T>
#### Remarks
- Add methods and Item property do not currently prevent frequency overflow - this will be fixed
- No unit tests for null as item parameters (currently only testing value types)
- CopyTo and ToArray tests not yet fully implemented

#### Constructors
- public FrequencyTable(): initializes a new instance of the FrequencyTable<T> class that is empty
- public FrequencyTable(IEnumerable<T> collection): initializes a new instance of the FrequencyTable<T> class that contains items copied from the specified collection
- public FrequencyTable(FrequencyTable<T> table): initializes a new instance of the FrequencyTable<T> class that contains items copied from the specified frequency table

#### Properties
- public int Count { get; }: gets the number of items contained in the FrequencyTable<T>
- public IEnumerable<int> Frequencies { get; }: gets a collection containing the frequencies of all items contained in the FrequencyTable<T>
- public IEnumerable<T> Items { get; }: gets a collection containing all items contained in the FrequencyTable<T>
- public int MaxFrequency { get; }: gets the maximum of frequency of all items contained in the FrequencyTable<T>
- public int MinFrequency { get; }: gets the minimum of frequency of all items contained in the FrequencyTable<T>
- public T this[int frequency] { get; set; }: gets or sets the frequency of the specified item

#### Methods
- public void Add(T item): adds an item to the FrequencyTable<T>
- public void Add(T item, int frequency): increases the frequency of the specified item in the FrequencyTable<T>
- public void AddRange(IEnumerable<T> collection): adds the items of the specified collection to the FrequencyTable<T>
- public void Clear(): removes all items from the FrequencyTable<T>
- public bool Contains(T item): determines whether an item is in the FrequencyTable<T>
- public bool ContainsAll(IEnumerable<T> collection): determines whether all items contained in the specified collection are in the FrequencyTable<T>
- public void CopyTo(T[] array): copies the entire FrequencyTable<T> to a compatible one-dimensional array, starting at the beginning of the target array
- public void CopyTo(T[] array, int arrayIndex): copies the entire FrequencyTable<T> to a compatible one-dimensional array, starting at the specified index of the target array
- public void CopyTo(T[] array, int arrayIndex, int frequency): copies the a range of items from the FrequencyTable<T> with the specified frequency to a compatible one-dimensional array, starting at the specified index of the target array
- public void CopyTo(T[] array, int arrayIndex, int minFrequency, int maxFrequency): copies the a range of items from the FrequencyTable<T> within the specified inclusive frequency range to a compatible one-dimensional array, starting at the specified index of the target array
- public IEnumerator<T> GetEnumerator(): returns an enumerator that iterates through the FrequencyTable<T>
- public IEnumerable<T> GetMax(): creates a shallow copy of all items with the maximum frequency in the source FrequencyTable<T>
- public IEnumerable<T> GetMin(): creates a shallow copy of all items with the minimum frequency in the source
- public bool Remove(T item): removes an item from the FrequencyTable<T>
- public bool Remove(T item, int frequency): decreases the frequency of the specified item in the FrequencyTable<T>
- public T[] ToArray(): copies the items of the FrequencyTable<T> to a new array
- public T[] ToArray(int frequency): copies a range of items of the FrequencyTable<T> with the specified frequency to a new array
- public T[] ToArray(int minFrequency, int maxFrequency): copies a range of items of the FrequencyTable<T> within the specified inclusive frequency range to a new array

#### Explicit Interface Implementations
- bool ICollection.IsSynchronized
- Object ICollection.SyncRoot
- bool ICollection<T>.IsReadOnly
- IEnumerator IEnumerable.GetEnumerator()
