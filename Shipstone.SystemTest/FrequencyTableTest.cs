using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Collections;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class FrequencyTableTest
    {
        private const int _SampleLength = 40;

        private static readonly Random _Random;

        private FrequencyTable<int> _Table;

        static FrequencyTableTest() => FrequencyTableTest._Random = new();

        private static void _GetMaxMin(Dictionary<int, int> dictionary, out int min, out int max)
        {
            if (dictionary.Count == 0)
            {
                max = min = 0;
                return;
            }

            min = Int32.MaxValue;
            max = 0;

            foreach (int val in dictionary.Values)
            {
                if (val < min)
                {
                    min = val;
                }

                if (val > max)
                {
                    max = val;
                }
            }
        }

        private static int[] _Sample() => FrequencyTableTest._Sample(0, Int32.MaxValue);
        private static int[] _Sample(out Dictionary<int, int> dictionary) => FrequencyTableTest._Sample(0, Int32.MaxValue, out dictionary);
        private static int[] _Sample(int min, int max) => FrequencyTableTest._Sample(min, max, out Dictionary<int, int> dictionary);

        private static int[] _Sample(int min, int max, out Dictionary<int, int> dictionary)
        {
            int[] sample = new int[FrequencyTableTest._SampleLength];
            dictionary = new();

            for (int i = 0; i < sample.Length; i ++)
            {
                int n = FrequencyTableTest._Random.Next(min, max);
                sample[i] = n;
                
                if (dictionary.ContainsKey(n))
                {
                    ++ dictionary[n];
                }

                else
                {
                    dictionary.Add(n, 1);
                }
            }

            return sample;
        }

        private static void _AssertProperties<T>(FrequencyTable<T> table, int count, int itemsCount, int min, int max)
        {
            Assert.AreEqual(count, table.Count);
            Assert.AreEqual(itemsCount, table.Frequencies.Count());
            Assert.AreEqual(itemsCount, table.Items.Count());
            Assert.AreEqual(max, table.MaxFrequency);
            Assert.AreEqual(min, table.MinFrequency);
        }

        private void _AssertProperties(int count, int itemsCount, int min, int max) => FrequencyTableTest._AssertProperties(this._Table, count, itemsCount, min, max);

        [TestInitialize]
        public void Initialize() => this._Table = new();

#region Item tests
        public void TestItem_InvalidRange()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => this._Table[10] = Int32.MinValue);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => this._Table[10] = -1);
        }

        [TestMethod]
        public void TestItem_NonZeroToNonZero_MultipleItems()
        {
            const int CONTROL = 10, CONSTANT = CONTROL * 2;
            this._Table[CONTROL] = CONTROL;
            this._Table[CONSTANT] = CONSTANT;
            this._Table[CONTROL] = CONSTANT;
            this._AssertProperties(CONSTANT * 2, 2, CONSTANT, CONSTANT);
            Assert.IsTrue(this._Table.Items.Contains(CONTROL));
            Assert.IsTrue(this._Table.Items.Contains(CONSTANT));
            Assert.AreEqual(CONSTANT, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
        }

        [TestMethod]
        public void TestItem_NonZeroToZero_MultipleItems()
        {
            const int CONTROL = 10, CONSTANT = CONTROL * 2;
            this._Table[CONTROL] = CONTROL;
            this._Table[CONSTANT] = CONSTANT;
            this._Table[CONTROL] = 0;
            this._AssertProperties(CONSTANT, 1, CONSTANT, CONSTANT);
            Assert.IsFalse(this._Table.Items.Contains(CONTROL));
            Assert.IsTrue(this._Table.Items.Contains(CONSTANT));
            Assert.AreEqual(0, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
        }

        [TestMethod]
        public void TestItem_ZeroToNonZero_MultipleItems()
        {
            const int CONTROL = 10, CONSTANT = CONTROL * 2;
            this._Table[CONTROL] = CONTROL;
            this._Table[CONSTANT] = CONSTANT;
            Assert.IsTrue(this._Table.Items.Contains(CONTROL));
            Assert.IsTrue(this._Table.Items.Contains(CONSTANT));
            Assert.AreEqual(CONTROL, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
            
            if (CONTROL < CONSTANT)
            {
                this._AssertProperties(CONSTANT + CONTROL, 2, CONTROL, CONSTANT);
            }

            else
            {
                this._AssertProperties(CONSTANT + CONTROL, 2, CONSTANT, CONTROL);
            }
        }

        [TestMethod]
        public void TestItem_ZeroToZero_MultipleItems()
        {
            const int CONTROL = 10, CONSTANT = CONTROL * 2;
            this._Table[CONTROL] = 0;
            this._Table[CONSTANT] = CONSTANT;
            this._AssertProperties(CONSTANT, 1, CONSTANT, CONSTANT);
            Assert.IsFalse(this._Table.Items.Contains(CONTROL));
            Assert.IsTrue(this._Table.Items.Contains(CONSTANT));
            Assert.AreEqual(0, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
        }

        [TestMethod]
        public void TestItem_NonZeroToNonZero_SingleItem()
        {
            const int CONTROL = 10, FREQ_1 = CONTROL, FREQ_2 = CONTROL * 2;
            this._Table[CONTROL] = FREQ_1;
            this._Table[CONTROL] = FREQ_2;
            this._AssertProperties(FREQ_2, 1, FREQ_2, FREQ_2);
            Assert.IsTrue(this._Table.Items.Contains(CONTROL));
            Assert.AreEqual(FREQ_2, this._Table[CONTROL]);
        }

        [TestMethod]
        public void TestItem_NonZeroToZero_SingleItem()
        {
            const int ITEM_1 = 10;
            this._Table[ITEM_1] = ITEM_1;
            this._Table[ITEM_1] = 0;
            this._AssertProperties(0, 0, 0, 0);
            Assert.IsFalse(this._Table.Items.Contains(ITEM_1));
            Assert.AreEqual(0, this._Table[ITEM_1]);
        }

        [TestMethod]
        public void TestItem_ZeroToNonZero_SingleItem()
        {
            const int CONTROL = 10;
            this._Table[CONTROL] = CONTROL;
            this._AssertProperties(CONTROL, 1, CONTROL, CONTROL);
            Assert.IsTrue(this._Table.Items.Contains(CONTROL));
            Assert.AreEqual(CONTROL, this._Table[CONTROL]);
        }

        [TestMethod]
        public void TestItem_ZeroToZero_SingleItem()
        {
            const int ITEM = 10;
            this._Table[ITEM] = 0;
            this._AssertProperties(0, 0, 0, 0);
            Assert.IsFalse(this._Table.Items.Contains(ITEM));
            Assert.AreEqual(0, this._Table[ITEM]);
        }
#endregion

#region Constructor tests
        [TestMethod]
        public void TestConstructor()
        {
            Assert.IsNotNull(this._Table.Frequencies);
            Assert.IsNotNull(this._Table.Items);
            this._AssertProperties(0, 0, 0, 0);
        }

        [TestMethod]
        public void TestConstructor_IEnumerable_Empty()
        {
            this._Table = new(Array.Empty<int>());
            this.TestConstructor();
        }

        [TestMethod]
        public void TestConstructor_IEnumerable_NotEmpty()
        {
            int[] sample = FrequencyTableTest._Sample(1, 10, out Dictionary<int, int> dictionary);
            this._Table = new(sample);
            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);

            for (int i = 0; i < sample.Length; i ++)
            {
                int n = sample[i];
                Assert.IsTrue(this._Table.Items.Contains(n));
                Assert.AreEqual(dictionary[n], this._Table[n]);
            }
        }

        [TestMethod]
        public void TestConstructor_IEnumerable_Null() => Assert.ThrowsException<ArgumentNullException>(() => new FrequencyTable<int>(null as int[]));

        [TestMethod]
        public void TestConstructor_FrequencyTable_Empty()
        {
            this._Table = new(this._Table);
            this.TestConstructor();
        }

        [TestMethod]
        public void TestConstructor_FrequencyTable_NotEmpty()
        {
            int[] sample = FrequencyTableTest._Sample(1, 10, out Dictionary<int, int> dictionary);
            this._Table = new(sample);
            FrequencyTable<int> table = new(this._Table);
            FrequencyTableTest._AssertProperties(table, this._Table.Count, this._Table.Items.Count(), this._Table.MinFrequency, this._Table.MaxFrequency);
            
            foreach (int item in table.Items)
            {
                Assert.IsTrue(this._Table.Items.Contains(item));
                Assert.AreEqual(this._Table[item], table[item]);
            }
            
            foreach (int frequency in table.Frequencies)
            {
                Assert.IsTrue(this._Table.Frequencies.Contains(frequency));
            }
        }

        [TestMethod]
        public void TestConstructor_FrequencyTable_Null() => Assert.ThrowsException<ArgumentNullException>(() => new FrequencyTable<int>(null));
#endregion

#region Add functionality
#region Add tests
        [TestMethod]
        public void TestAdd_Item_Empty()
        {
            const int CONTROL = 10;
            this._Table.Add(CONTROL);
            this._AssertProperties(1, 1, 1, 1);
        }

        [TestMethod]
        public void TestAdd_Item_NotEmpty_Contains()
        {
            const int CONTROL = 10;
            this._Table[CONTROL] = CONTROL;
            this._Table.Add(CONTROL);
            this._AssertProperties(CONTROL + 1, 1, CONTROL + 1, CONTROL + 1);
        }

        [TestMethod]
        public void TestAdd_Item_NotEmpty_NotContains()
        {
            const int CONTROL = 10, CONSTANT = CONTROL * 2;
            this._Table[CONSTANT] = CONSTANT;
            this._Table.Add(CONTROL);
            this._AssertProperties(CONSTANT + 1, 2, 1, CONSTANT);
        }

        [TestMethod]
        public void TestAdd_Item_Int32_Empty_NonZero()
        {
            const int CONTROL = 10;
            this._Table.Add(CONTROL, CONTROL);
            this._AssertProperties(CONTROL, 1, CONTROL, CONTROL);
        }

        [TestMethod]
        public void TestAdd_Item_Int32_Empty_Zero()
        {
            const int CONTROL = 10;
            this._Table.Add(CONTROL, 0);
            this._AssertProperties(0, 0, 0, 0);
        }

        [TestMethod]
        public void TestAdd_Item_Int32_NotEmpty_Contains_NonZero()
        {
            const int CONTROL = 10;
            this._Table[CONTROL] = CONTROL;
            this._Table.Add(CONTROL, CONTROL);
            this._AssertProperties(CONTROL * 2, 1, CONTROL * 2, CONTROL * 2);
        }

        [TestMethod]
        public void TestAdd_Item_Int32_NotEmpty_Contains_Zero()
        {
            const int CONTROL = 10;
            this._Table[CONTROL] = CONTROL;
            this._Table.Add(CONTROL, 0);
            this._AssertProperties(CONTROL, 1, CONTROL, CONTROL);
        }

        [TestMethod]
        public void TestAdd_Item_Int32_NotEmpty_NotContains_NonZero()
        {
            const int CONTROL = 10, CONSTANT = CONTROL * 2;
            this._Table[CONSTANT] = CONSTANT;
            this._Table.Add(CONTROL, CONTROL);
            this._AssertProperties(CONSTANT + CONTROL, 2, CONTROL, CONSTANT);
        }

        [TestMethod]
        public void TestAdd_Item_Int32_NotEmpty_NotContains_Zero()
        {
            const int CONTROL = 10, CONSTANT = CONTROL * 2;
            this._Table[CONSTANT] = CONSTANT;
            this._Table.Add(CONTROL, 0);
            this._AssertProperties(CONSTANT, 1, CONSTANT, CONSTANT);
        }

        [TestMethod]
        public void TestAdd_Item_Int32_OutOfRange()
        {
            const int CONTROL = 10;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => this._Table.Add(CONTROL, Int32.MinValue));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => this._Table.Add(CONTROL, -1));
        }
#endregion

#region AddRange tests
        [TestMethod]
        public void TestAddRange_EmptyTable_EmptyCollection()
        {
            this._Table.AddRange(Array.Empty<int>());
            this._AssertProperties(0, 0, 0, 0);
        }

        [TestMethod]
        public void TestAddRange_EmptyTable_NotEmptyCollection()
        {
            int[] sample = FrequencyTableTest._Sample(out Dictionary<int, int> dictionary);
            this._Table.AddRange(sample);
            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
        }

        [TestMethod]
        public void TestAddRange_NotEmptyTable_EmptyCollection()
        {
            int[] sample = FrequencyTableTest._Sample(out Dictionary<int, int> dictionary);
            this._Table = new(sample);
            this._Table.AddRange(Array.Empty<int>());
            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
        }

        [TestMethod]
        public void TestAddRange_NotEmptyTable_NotEmptyCollection()
        {
            const int MIN_LIMIT = 10, MAX_LIMIT = 20;
            int[] originalSample = FrequencyTableTest._Sample(MIN_LIMIT, MAX_LIMIT, out Dictionary<int, int> originalDictionary);
            this._Table = new(originalSample);
            int[] newSample = FrequencyTableTest._Sample(MIN_LIMIT, MAX_LIMIT, out Dictionary<int, int> newDictionary);
            this._Table.AddRange(newSample);
            Dictionary<int, int> mergedDictionary = new Dictionary<int, int>(originalDictionary);

            foreach (int item in newDictionary.Keys)
            {
                if (mergedDictionary.ContainsKey(item))
                {
                    mergedDictionary[item] += newDictionary[item];
                }

                else
                {
                    mergedDictionary.Add(item, newDictionary[item]);
                }
            }

            FrequencyTableTest._GetMaxMin(mergedDictionary, out int min, out int max);
            this._AssertProperties(originalSample.Length + newSample.Length, mergedDictionary.Count, min, max);
        }

        [TestMethod]
        public void TestAddRange_NullCollection() => Assert.ThrowsException<ArgumentNullException>(() => this._Table.AddRange(null));
#endregion
#endregion

#region Clear tests
        [TestMethod]
        public void TestClear_Empty()
        {
            this._Table.Clear();
            this._AssertProperties(0, 0, 0, 0);
        }

        [TestMethod]
        public void TestClear_NotEmpty_MultipleItems()
        {
            int[] sample = FrequencyTableTest._Sample();
            this._Table = new(sample);
            this._Table.Clear();
            this._AssertProperties(0, 0, 0, 0);
        }

        [TestMethod]
        public void TestClear_NotEmpty_SingleItem()
        {
            const int CONSTANT = 10;
            this._Table[CONSTANT] = CONSTANT;
            this._Table.Clear();
            this._AssertProperties(0, 0, 0, 0);
        }
#endregion

#region Contains tests
        [TestMethod]
        public void TestContains_Empty()
        {
            Assert.IsFalse(this._Table.Contains(10));
            this._AssertProperties(0, 0, 0, 0);
        }

        [TestMethod]
        public void TestContains_NotEmpty_Contains()
        {
            int[] sample = FrequencyTableTest._Sample(10, 20, out Dictionary<int, int> dictionary);
            this._Table = new(sample);
            
            foreach (int item in dictionary.Keys)
            {
                Assert.IsTrue(this._Table.Contains(item));
            }

            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
        }

        [TestMethod]
        public void TestContains_NotEmpty_NotContains()
        {
            const int MAX = 20;
            int[] sample = FrequencyTableTest._Sample(10, MAX, out Dictionary<int, int> dictionary);
            this._Table = new(sample);
            Assert.IsFalse(this._Table.Contains(MAX + 1));
            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
        }
#endregion

#region ContainsAll tests
        [TestMethod]
        public void TestContainsAll_Empty()
        {
            int[] sample = FrequencyTableTest._Sample();
            Assert.IsFalse(this._Table.ContainsAll(sample));
            this._AssertProperties(0, 0, 0, 0);
        }

        [TestMethod]
        public void TestContainsAll_NotEmpty_ContainsAll()
        {
            int[] sample = FrequencyTableTest._Sample(10, 20, out Dictionary<int, int> dictionary);
            this._Table = new(sample);
            
            foreach (int item in dictionary.Keys)
            {
                Assert.IsTrue(this._Table.ContainsAll(sample));
            }

            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
        }

        [TestMethod]
        public void TestContainsAll_NotEmpty_ContainsSome()
        {
            const int MAX = 20;
            int[] sample = FrequencyTableTest._Sample(10, MAX, out Dictionary<int, int> dictionary);
            int[] largerSample = new int[sample.Length + 1];
            Array.Copy(sample, largerSample, sample.Length);
            largerSample[sample.Length] = MAX;
            this._Table = new(sample);
            
            foreach (int item in dictionary.Keys)
            {
                Assert.IsFalse(this._Table.ContainsAll(largerSample));
            }

            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
        }

        [TestMethod]
        public void TestContainsAll_NotEmpty_ContainsNone()
        {
            const int MAX = 20;
            int[] sample = FrequencyTableTest._Sample(10, MAX, out Dictionary<int, int> dictionary);
            this._Table[MAX] = MAX;
            Assert.IsFalse(this._Table.ContainsAll(sample));
            this._AssertProperties(MAX, 1, MAX, MAX);
        }

        [TestMethod]
        public void TestContainsAll_Null() => Assert.ThrowsException<ArgumentNullException>(() => this._Table.ContainsAll(null));
#endregion

#region CopyTo tests
#region Generic tests
#region CopyTo(T) tests
        [TestMethod]
        public void TestCopyTo_Item_EmptyTable_NonZeroLengthArray() => this.TestCopyTo_Item_EmptyTable_Array(10);

        [TestMethod]
        public void TestCopyTo_Item_EmptyTable_ZeroLengthArray()
        {
            int[] array = this.TestCopyTo_Item_EmptyTable_Array(0);
            Assert.IsTrue(this._Table.ContainsAll(array));
        }

        private int[] TestCopyTo_Item_EmptyTable_Array(int length)
        {
            int[] array = length == 0 ? Array.Empty<int>() : new int[length];
            this._Table.CopyTo(array);
            Assert.AreEqual(length, array.Length);
            this._AssertProperties(0, 0, 0, 0);
            return array;
        }

        [TestMethod]
        public void TestCopyTo_Item_NonEmptyTable_ArrayEqualSize()
        {
            int[] sample = FrequencyTableTest._Sample(out Dictionary<int, int> dictionary);
            this._Table.AddRange(sample);
            int[] array = new int[sample.Length];
            this._Table.CopyTo(array);
            Assert.IsTrue(this._Table.ContainsAll(array));
            Assert.AreEqual(sample.Length, array.Length);
            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
            IDictionary<int, int> arrayDictionary = new Dictionary<int, int>();

            foreach (int item in array)
            {
                if (arrayDictionary.ContainsKey(item))
                {
                    ++ arrayDictionary[item];
                }

                else
                {
                    arrayDictionary.Add(item, 1);
                }
            }

            Assert.AreEqual(dictionary.Count, arrayDictionary.Count);
            
            foreach (int item in arrayDictionary.Keys)
            {
                Assert.AreEqual(dictionary[item], arrayDictionary[item]);
            }
        }

        [TestMethod]
        public void TestCopyTo_Item_NonEmptyTable_ArrayTooSmall() => this.TestCopyTo_Item_NonEmptyTable_ArrayTooSmall(FrequencyTableTest._SampleLength - 1);

        [TestMethod]
        public void TestCopyTo_Item_NonEmptyTable_ZeroLengthArray() => this.TestCopyTo_Item_NonEmptyTable_ArrayTooSmall(0);

        private void TestCopyTo_Item_NonEmptyTable_ArrayTooSmall(int length)
        {
            int[] sample = FrequencyTableTest._Sample(out Dictionary<int, int> dictionary);
            this._Table.AddRange(sample);
            int[] array = length == 0 ? Array.Empty<int>() : new int[length];
            Exception ex = Assert.ThrowsException<ArgumentException>(() => this._Table.CopyTo(array));
            Assert.AreEqual("The number of items in the source FrequencyTable<T> is greater than the number of items that the destination array can contain.", ex.Message);
            Assert.AreEqual(length, array.Length);
            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
        }

        [TestMethod]
        public void TestCopyTo_Item_Null() => Assert.ThrowsException<ArgumentNullException>(() => this._Table.CopyTo(null));
#endregion

#region CopyTo(T[], int) tests
        [TestMethod]
        public void TestCopyTo_Item_Int32_EmptyTable_NonZeroLengthArray_PositiveIndex()
        {
            this.TestCopyTo_Item_Int32_EmptyTable_Array(10, 1);
            this.TestCopyTo_Item_Int32_EmptyTable_Array(10, Int32.MaxValue);
        }

        [TestMethod]
        public void TestCopyTo_Item_Int32_EmptyTable_NonZeroLengthArray_ZeroIndex() => this.TestCopyTo_Item_Int32_EmptyTable_Array(10, 0);
        
        [TestMethod]
        public void TestCopyTo_Item_Int32_EmptyTable_ZeroLengthArray_PositiveIndex()
        {
            int[] array = this.TestCopyTo_Item_Int32_EmptyTable_Array(0, 1);
            Assert.IsTrue(this._Table.ContainsAll(array));
            array = this.TestCopyTo_Item_Int32_EmptyTable_Array(0, Int32.MaxValue);
            Assert.IsTrue(this._Table.ContainsAll(array));
        }

        [TestMethod]
        public void TestCopyTo_Item_EmptyTable_ZeroLengthArray_ZeroIndex()
        {
            int[] array = this.TestCopyTo_Item_Int32_EmptyTable_Array(0, 0);
            Assert.IsTrue(this._Table.ContainsAll(array));
        }

        private int[] TestCopyTo_Item_Int32_EmptyTable_Array(int length, int arrayIndex)
        {
            int[] array = length == 0 ? Array.Empty<int>() : new int[length];
            this._Table.CopyTo(array, arrayIndex);
            Assert.AreEqual(length, array.Length);
            this._AssertProperties(0, 0, 0, 0);
            return array;
        }

        [TestMethod]
        public void TestCopyTo_Item_NonEmptyTable_ArrayEqualSize_InvalidIndex()
        {
            int[] sample = FrequencyTableTest._Sample(out Dictionary<int, int> dictionary);
            this.TestCopyTo_Item_Int32_NonEmptyTable_ArrayTooSmall(sample.Length, 1, sample, dictionary);
        }

        [TestMethod]
        public void TestCopyTo_Item_Int32_NonEmptyTable_ArrayEqualSize_ValidIndex()
        {
            int[] sample = FrequencyTableTest._Sample(out Dictionary<int, int> dictionary);
            this._Table.AddRange(sample);
            int[] array = new int[sample.Length + 1];
            this._Table.CopyTo(array, 1);
            int[] trimmedArray = new int[array.Length - 1];
            Array.Copy(array, 1, trimmedArray, 0, trimmedArray.Length);
            Assert.IsTrue(this._Table.ContainsAll(trimmedArray));
            Assert.AreEqual(0, array[0]);
            Assert.AreEqual(sample.Length + 1, array.Length);
            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
            IDictionary<int, int> arrayDictionary = new Dictionary<int, int>();

            for (int i = 1; i < array.Length; i ++)
            {
                int item = array[i];

                if (arrayDictionary.ContainsKey(item))
                {
                    ++ arrayDictionary[item];
                }

                else
                {
                    arrayDictionary.Add(item, 1);
                }
            }

            Assert.AreEqual(dictionary.Count, arrayDictionary.Count);
            
            foreach (int item in arrayDictionary.Keys)
            {
                Assert.AreEqual(dictionary[item], arrayDictionary[item]);
            }
        }

        [TestMethod]
        public void TestCopyTo_Item_Int32_NonEmptyTable_ArrayTooSmall() => this.TestCopyTo_Item_Int32_NonEmptyTable_ArrayTooSmall(FrequencyTableTest._SampleLength - 1, 0);

        [TestMethod]
        public void TestCopyTo_Item_Int32_NonEmptyTable_ZeroLengthArray() => this.TestCopyTo_Item_Int32_NonEmptyTable_ArrayTooSmall(0, 0);

        private void TestCopyTo_Item_Int32_NonEmptyTable_ArrayTooSmall(int length, int arrayIndex)
        {
            int[] sample = FrequencyTableTest._Sample(out Dictionary<int, int> dictionary);
            this.TestCopyTo_Item_Int32_NonEmptyTable_ArrayTooSmall(length, arrayIndex, sample, dictionary);
        }

        private void TestCopyTo_Item_Int32_NonEmptyTable_ArrayTooSmall(int length, int arrayIndex, int[] sample, Dictionary<int, int> dictionary)
        {
            this._Table.AddRange(sample);
            int[] array = length == 0 ? Array.Empty<int>() : new int[length];
            Exception ex = Assert.ThrowsException<ArgumentException>(() => this._Table.CopyTo(array, arrayIndex));
            Assert.AreEqual("The number of items in the source FrequencyTable<T> is greater than the available space from arrayIndex to the end of the destination array.", ex.Message);
            Assert.AreEqual(length, array.Length);
            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
        }

        [TestMethod]
        public void TestCopyTo_Item_Int32_NegativeIndex()
        {
            int[] array = Array.Empty<int>();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => this._Table.CopyTo(array, Int32.MinValue));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => this._Table.CopyTo(array, -1));
        }

        [TestMethod]
        public void TestCopyTo_Item_Int32_Null() => Assert.ThrowsException<ArgumentNullException>(() => this._Table.CopyTo(null, 0));
#endregion
#endregion
#endregion

#region GetEnumerator tests
        [TestMethod]
        public void TestGetEnumerator_Disposed()
        {
            using (IEnumerator<int> enumerator = this._Table.GetEnumerator())
            {
                enumerator.Dispose();
                Assert.ThrowsException<ObjectDisposedException>(() => enumerator.MoveNext());
                Assert.ThrowsException<ObjectDisposedException>(() => enumerator.Reset());
                int current = enumerator.Current;
            }
        }

        [TestMethod]
        public void TestGetEnumerator_Empty()
        {
            const int MIN = 10, MAX = 20;
            int[] sample = FrequencyTableTest._Sample(MIN, MAX);
            this._Table.AddRange(sample);

            using (IEnumerator<int> enumerator = this._Table.GetEnumerator())
            {
                this._Table.Add(MIN + MAX);
                int current = enumerator.Current;
                const String MESSAGE = "The collection was modified after the enumerator was created.";
                Exception ex = Assert.ThrowsException<InvalidOperationException>(() => enumerator.MoveNext());
                Assert.AreEqual(MESSAGE, ex.Message);
                ex = Assert.ThrowsException<InvalidOperationException>(() => enumerator.Reset());
                Assert.AreEqual(MESSAGE, ex.Message);
            }
        }

        [TestMethod]
        public void TestGetEnumerator_Modified()
        {
            using (IEnumerator<int> enumerator = this._Table.GetEnumerator())
            {
                enumerator.Dispose();
                Assert.ThrowsException<ObjectDisposedException>(() => enumerator.MoveNext());
                Assert.ThrowsException<ObjectDisposedException>(() => enumerator.Reset());
                int current = enumerator.Current;
            }
        }

        [TestMethod]
        public void TestGetEnumerator_NotEmpty()
        {
            int[] sample = FrequencyTableTest._Sample();
            this._Table.AddRange(sample);
            FrequencyTable<int> newTable = new();

            using (IEnumerator<int> enumerator = this._Table.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    newTable.Add(enumerator.Current);
                }
            }
            
            Assert.AreEqual(this._Table.Count, newTable.Count);
            Assert.AreEqual(this._Table.Frequencies.Count(), newTable.Frequencies.Count());
            Assert.AreEqual(this._Table.Items.Count(), newTable.Items.Count());
            Assert.AreEqual(this._Table.MaxFrequency, newTable.MaxFrequency);
            Assert.AreEqual(this._Table.MinFrequency, newTable.MinFrequency);

            foreach (int item in this._Table)
            {
                Assert.AreEqual(this._Table[item], newTable[item]);
            }

            int[] array = this._Table.ToArray();
            ICollection<int> arrayCollection = new List<int>(array), enumerated = new List<int>(this._Table);
            Assert.AreEqual(array.Length, enumerated.Count);

            foreach (int item in enumerated)
            {
                Assert.IsTrue(arrayCollection.Remove(item));
            }

            foreach (int item in array)
            {
                Assert.IsTrue(enumerated.Remove(item));
            }

            Assert.AreEqual(0, arrayCollection.Count);
            Assert.AreEqual(0, enumerated.Count);
        }
#endregion

#region Get Range functionality
#region GetMax tests
        [TestMethod]
        public void TestGetMax_Empty()
        {
            Exception ex = Assert.ThrowsException<InvalidOperationException>(() => this._Table.GetMax());
            Assert.AreEqual("The FrequencyTable<T> is empty.", ex.Message);
        }

        [TestMethod]
        public void TestGetMax_NotEmpty()
        {
            int[] sample = FrequencyTableTest._Sample(1, 10, out Dictionary<int, int> dictionary);
            this._Table.AddRange(sample);
            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            ICollection<int> dictionaryMaxCollection = new List<int>();

            foreach (KeyValuePair<int, int> keyValuePair in dictionary)
            {
                if (keyValuePair.Value == max)
                {
                    dictionaryMaxCollection.Add(keyValuePair.Key);
                }
            }

            IEnumerable<int> maxCollection = this._Table.GetMax();
            ICollection<int> maxRemaining = new LinkedList<int>(maxCollection);
            Assert.AreEqual(dictionaryMaxCollection.Count, maxRemaining.Count);

            foreach (int item in dictionaryMaxCollection)
            {
                Assert.IsTrue(maxRemaining.Remove(item));
            }

            Assert.AreEqual(0, maxRemaining.Count);

            foreach (int item in maxCollection)
            {
                Assert.IsTrue(dictionaryMaxCollection.Remove(item));
            }

            Assert.AreEqual(0, dictionaryMaxCollection.Count);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
        }
#endregion

#region GetMin tests
        [TestMethod]
        public void TestGetMin_Empty()
        {
            Exception ex = Assert.ThrowsException<InvalidOperationException>(() => this._Table.GetMin());
            Assert.AreEqual("The FrequencyTable<T> is empty.", ex.Message);
        }

        [TestMethod]
        public void TestGetMin_NotEmpty()
        {
            int[] sample = FrequencyTableTest._Sample(1, 10, out Dictionary<int, int> dictionary);
            this._Table.AddRange(sample);
            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            ICollection<int> dictionaryMinCollection = new List<int>();

            foreach (KeyValuePair<int, int> keyValuePair in dictionary)
            {
                if (keyValuePair.Value == min)
                {
                    dictionaryMinCollection.Add(keyValuePair.Key);
                }
            }

            IEnumerable<int> minCollection = this._Table.GetMin();
            ICollection<int> minRemaining = new LinkedList<int>(minCollection);
            Assert.AreEqual(dictionaryMinCollection.Count, minRemaining.Count);

            foreach (int item in dictionaryMinCollection)
            {
                Assert.IsTrue(minRemaining.Remove(item));
            }

            Assert.AreEqual(0, minRemaining.Count);

            foreach (int item in minCollection)
            {
                Assert.IsTrue(dictionaryMinCollection.Remove(item));
            }

            Assert.AreEqual(0, dictionaryMinCollection.Count);
            this._AssertProperties(sample.Length, dictionary.Count, min, max);
        }
#endregion
#endregion

#region Remove functionality
#region Remove tests
        [TestMethod]
        public void TestRemove_Item_Empty()
        {
            Assert.IsFalse(this._Table.Remove(10));
            this._AssertProperties(0, 0, 0, 0);
        }

        [TestMethod]
        public void TestRemove_Item_NotEmpty_Contains()
        {
            const int CONTROL = 10, CONSTANT = 20;
            this._Table[CONTROL] = CONTROL;
            this._Table[CONSTANT] = CONSTANT;
            Assert.IsTrue(this._Table.Remove(CONTROL));
            Assert.AreEqual(CONTROL - 1, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
            this._AssertProperties(CONSTANT + CONTROL - 1, 2, CONTROL - 1, CONSTANT);
        }

        [TestMethod]
        public void TestRemove_Item_NotEmpty_NotContains()
        {
            const int CONTROL = 10, CONSTANT = 20;
            this._Table[CONSTANT] = CONSTANT;
            Assert.IsFalse(this._Table.Remove(CONTROL));
            Assert.AreEqual(0, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
            this._AssertProperties(CONSTANT, 1, CONSTANT, CONSTANT);
        }

        [TestMethod]
        public void TestRemove_Item_Int32_Empty()
        {
            Assert.IsFalse(this._Table.Remove(10, 1));
            this._AssertProperties(0, 0, 0, 0);
        }

        [TestMethod]
        public void TestRemove_Item_Int32_NotEmpty_Contains_Less()
        {
            const int CONTROL = 10, CONSTANT = 20;
            this._Table[CONTROL] = CONTROL;
            this._Table[CONSTANT] = CONSTANT;
            Assert.IsTrue(this._Table.Remove(CONTROL, CONTROL + 1));
            Assert.AreEqual(0, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
            this._AssertProperties(CONSTANT, 1, CONSTANT, CONSTANT);
        }

        [TestMethod]
        public void TestRemove_Item_Int32_NotEmpty_Contains_Equal()
        {
            const int CONTROL = 10, CONSTANT = 20;
            this._Table[CONTROL] = CONTROL;
            this._Table[CONSTANT] = CONSTANT;
            Assert.IsTrue(this._Table.Remove(CONTROL, CONTROL));
            Assert.AreEqual(0, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
            this._AssertProperties(CONSTANT, 1, CONSTANT, CONSTANT);
        }

        [TestMethod]
        public void TestRemove_Item_Int32_NotEmpty_Contains_More()
        {
            const int CONTROL = 10, CONSTANT = 20;
            this._Table[CONTROL] = CONTROL;
            this._Table[CONSTANT] = CONSTANT;
            Assert.IsTrue(this._Table.Remove(CONTROL, CONTROL - 1));
            Assert.AreEqual(1, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
            this._AssertProperties(CONSTANT + 1, 2, 1, CONSTANT);
        }

        [TestMethod]
        public void TestRemove_Item_Int32_NotEmpty_NotContains()
        {
            const int CONTROL = 10, CONSTANT = 20;
            this._Table[CONSTANT] = CONSTANT;
            Assert.IsFalse(this._Table.Remove(CONTROL));
            Assert.AreEqual(0, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
            this._AssertProperties(CONSTANT, 1, CONSTANT, CONSTANT);
        }

        [TestMethod]
        public void TestRemove_Item_Int32_OutOfRange()
        {
            const int CONTROL = 10;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => this._Table.Remove(CONTROL, Int32.MinValue));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => this._Table.Remove(CONTROL, -1));
            this._AssertProperties(0, 0, 0, 0);
        }
#endregion
#endregion

#region ToArray tests
        [TestMethod]
        public void TestToArray_Empty()
        {
            int[] array = this._Table.ToArray();
            Assert.IsTrue(this._Table.ContainsAll(array));
            Assert.IsNotNull(array);
            Assert.AreEqual(0, array.Length);
            this._AssertProperties(0, 0, 0, 0);
        }

        [TestMethod]
        public void TestToArray_NotEmpty()
        {
            int[] sample = FrequencyTableTest._Sample(out Dictionary<int, int> sampleDictionary);
            this._Table.AddRange(sample);
            int[] array = this._Table.ToArray();
            Assert.IsTrue(this._Table.ContainsAll(array));
            Assert.IsNotNull(array);
            Assert.AreEqual(this._Table.Count, array.Length);
            Dictionary<int, int> dictionary = new Dictionary<int, int>();

            foreach (int item in array)
            {
                if (dictionary.ContainsKey(item))
                {
                    ++ dictionary[item];
                }

                else
                {
                    dictionary.Add(item, 1);
                }
            }

            foreach (int item in dictionary.Keys)
            {
                Assert.AreEqual(this._Table[item], dictionary[item]);
            }

            FrequencyTableTest._GetMaxMin(dictionary, out int min, out int max);
            this._AssertProperties(array.Length, dictionary.Count, min, max);
        }
#endregion
    }
}
