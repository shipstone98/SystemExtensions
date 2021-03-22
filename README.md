# SystemExtensions

Extension classes to .NET Standard 2.0.

## Namespaces
- Shipstone.System.Collections: contains classes that define generic collections, which allows users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections

## Shipstone.System.Collections
- FrequencyTable<T>: represents a strongly types frequency table of items

### FrequencyTable<T> : ICollection, ICollection<T>, IReadOnlyCollection<T>
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
- public IEnumerable<T> GetMax(): creates a shallow copy of all items with the maximum frequency in the source FrequencyTable<T>
- public IEnumerable<T> GetMin(): creates a shallow copy of all items with the minimum frequency in the source

#### Explicit Interface Implementations
- bool ICollection.IsSynchronized
- Object ICollection.SyncRoot
- bool ICollection<T>.IsReadOnly
- IEnumerator IEnumerable.GetEnumerator()
