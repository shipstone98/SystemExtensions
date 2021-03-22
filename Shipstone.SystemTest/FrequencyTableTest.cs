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
        }
#endregion
#endregion
    }
}
