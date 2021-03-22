# SystemExtensions

Extension classes to .NET Standard 2.0.

## Namespaces
- Shipstone.System.Collections: contains classes that define generic collections, which allows users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections
- Shipstone.System.Numerics: contains numeric and mathematical types that complement the numeric primitives, such as Byte, Double and Int32, that are defined by .NET

## Shipstone.System.Collections
- public static class EnumerableExtensions: provides a set of static (Shared in Visual Basic) methods for querying objects that implement IEnumerable<T>
- public class FrequencyTable<T>: represents a strongly types frequency table of items

### EnumerableExtensions
#### Methods
- public static IEnumerable<T> Median(IEnumerable<T> source): returns the median value(s) in the specified sorted collection

### FrequencyTable<T> : ICollection, ICollection<T>, IReadOnlyCollection<T>
#### Remarks
- Add methods and Item property do not currently prevent frequency overflow - this will be fixed
- No unit tests for null as item parameters (currently only testing value types)
- CopyTo and ToArray tests not yet fully implemented
- GetEnumerator modify tests could be strengthened to test every non-const method

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
- public void Swap(T a, T b): swaps the frequencies of the two specified items in the FrequencyTable<T>
- public T[] ToArray(): copies the items of the FrequencyTable<T> to a new array
- public T[] ToArray(int frequency): copies a range of items of the FrequencyTable<T> with the specified frequency to a new array
- public T[] ToArray(int minFrequency, int maxFrequency): copies a range of items of the FrequencyTable<T> within the specified inclusive frequency range to a new array

#### Explicit Interface Implementations
- bool ICollection.IsSynchronized
- Object ICollection.SyncRoot
- bool ICollection<T>.IsReadOnly
- IEnumerator IEnumerable.GetEnumerator()

## Shipstone.System.Numerics
- public static class MathExtensions: provides static methods for common mathematical and statistical functions
- public class Matrix: represents a matrix of specified size

### MathExtensions
#### Methods
- public static double Mean(IEnumerable<double> collection): calculates the mean average of values in the specified collection
- public static double Mean(IEnumerable<double> collection): calculates the median average of values in the specified sorted collection
- public static double Mode(IEnumerable<double> collection): calculates the modal average of values in the specified collection
- public static int SolveQuadratic(double a, double b, double c, out Complex x1, out Complex x2): solves for x the quadratic equation of the form ax ^ 2 + bx + c = 0
- public static bool TrySolveQuadratic(double a, double b, double c, out double x1, out double x2): solves for x the quadratic equation of the form ax ^ 2 + bx + c = 0
- public static double Variance(IEnumerable<double> collection): calculates the population variance of values in the specified collection
- public static double Variance(IEnumerable<double> collection, double mean): calculates the population variance of values in the specified collection using the pre-calculated mean
- public static double Variance(IEnumerable<double> collection, out double mean): calculates the population variance of values in the specified collection

### Matrix : IEquatable<Matrix>
#### Constructors
- public Matrix(int rows, int columns): initializes a new instance of the Matrix class that is empty and contains the specified number of rows and columns
- public Matrix(Matrix matrix): initializes a new instance of the Matrix class that contains values copied from the specified matrix and has an equal number of rows and columns

#### Properties
- public int Columns: gets the number of columns in the Matrix
- public int Rows: gets the number of rows in the Matrix
- public double this[int row, int column]: gets or sets the value at the specified entry in the Matrix

#### Methods
- public override bool Equals(Object obj): returns a value indicating whether this instance is equal to a specified object
- public bool Equals(Matrix matrix): returns a value indicating whether this instance is equal to a specified Matrix
- public override int GetHashCode(): returns the hash code for this instance
- public Matrix Multiply(Matrix matrix): multiples the current Matrix instance by matrix and returns the result as a new Matrix

#### Operators
- public static bool operator ==(Matrix a, Matrix b): determines whether two matrices are equal
- public static bool operator !=(Matrix a, Matrix b): determines whether two matrices are unequal