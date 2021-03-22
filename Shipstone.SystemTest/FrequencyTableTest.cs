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

        static FrequencyTableTest() => FrequencyTableTest._Random = new Random();

        private static int[] _Sample() => FrequencyTableTest._Sample(0, Int32.MaxValue);
        private static int[] _Sample(out Dictionary<int, int> dictionary) => FrequencyTableTest._Sample(0, Int32.MaxValue, out dictionary);
        private static int[] _Sample(int min, int max) => FrequencyTableTest._Sample(min, max, out Dictionary<int, int> dictionary);

        private static int[] _Sample(int min, int max, out Dictionary<int, int> dictionary)
        {
            int[] sample = new int[FrequencyTableTest._SampleLength];
            dictionary = new Dictionary<int, int>();

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

        [TestInitialize]
        public void Initialize() => this._Table = new FrequencyTable<int>();

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
            Assert.AreEqual(CONSTANT * 2, this._Table.Count);
            Assert.AreEqual(2, this._Table.Frequencies.Count());
            Assert.AreEqual(2, this._Table.Items.Count());
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
            Assert.AreEqual(CONSTANT, this._Table.Count);
            Assert.AreEqual(1, this._Table.Frequencies.Count());
            Assert.AreEqual(1, this._Table.Items.Count());
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
            Assert.AreEqual(CONTROL + CONSTANT, this._Table.Count);
            Assert.AreEqual(2, this._Table.Frequencies.Count());
            Assert.AreEqual(2, this._Table.Items.Count());
            Assert.IsTrue(this._Table.Items.Contains(CONTROL));
            Assert.IsTrue(this._Table.Items.Contains(CONSTANT));
            Assert.AreEqual(CONTROL, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
        }

        [TestMethod]
        public void TestItem_ZeroToZero_MultipleItems()
        {
            const int CONTROL = 10, CONSTANT = CONTROL * 2;
            this._Table[CONTROL] = 0;
            this._Table[CONSTANT] = CONSTANT;
            Assert.AreEqual(CONSTANT, this._Table.Count);
            Assert.AreEqual(1, this._Table.Frequencies.Count());
            Assert.AreEqual(1, this._Table.Items.Count());
            Assert.IsFalse(this._Table.Items.Contains(CONTROL));
            Assert.IsTrue(this._Table.Items.Contains(CONSTANT));
            Assert.AreEqual(0, this._Table[CONTROL]);
            Assert.AreEqual(CONSTANT, this._Table[CONSTANT]);
        }

        [TestMethod]
        public void TestItem_NonZeroToNonZero_SingleItem()
        {
            const int ITEM = 10, FREQ_1 = ITEM, FREQ_2 = ITEM * 2;
            this._Table[ITEM] = FREQ_1;
            this._Table[ITEM] = FREQ_2;
            Assert.AreEqual(FREQ_2, this._Table.Count);
            Assert.AreEqual(1, this._Table.Frequencies.Count());
            Assert.AreEqual(1, this._Table.Items.Count());
            Assert.IsTrue(this._Table.Items.Contains(ITEM));
            Assert.AreEqual(FREQ_2, this._Table[ITEM]);
        }

        [TestMethod]
        public void TestItem_NonZeroToZero_SingleItem()
        {
            const int ITEM_1 = 10;
            this._Table[ITEM_1] = ITEM_1;
            this._Table[ITEM_1] = 0;
            Assert.AreEqual(0, this._Table.Count);
            Assert.AreEqual(0, this._Table.Frequencies.Count());
            Assert.AreEqual(0, this._Table.Items.Count());
            Assert.IsFalse(this._Table.Items.Contains(ITEM_1));
            Assert.AreEqual(0, this._Table[ITEM_1]);
        }

        [TestMethod]
        public void TestItem_ZeroToNonZero_SingleItem()
        {
            const int ITEM = 10;
            this._Table[ITEM] = ITEM;
            Assert.AreEqual(ITEM, this._Table.Count);
            Assert.AreEqual(1, this._Table.Frequencies.Count());
            Assert.AreEqual(1, this._Table.Items.Count());
            Assert.IsTrue(this._Table.Items.Contains(ITEM));
            Assert.AreEqual(ITEM, this._Table[ITEM]);
        }

        [TestMethod]
        public void TestItem_ZeroToZero_SingleItem()
        {
            const int ITEM = 10;
            this._Table[ITEM] = 0;
            Assert.AreEqual(0, this._Table.Count);
            Assert.AreEqual(0, this._Table.Frequencies.Count());
            Assert.AreEqual(0, this._Table.Items.Count());
            Assert.IsFalse(this._Table.Items.Contains(ITEM));
            Assert.AreEqual(0, this._Table[ITEM]);
        }
#endregion

#region Constructor tests
        [TestMethod]
        public void TestConstructor_IEnumerable_Empty()
        {
            this._Table = new FrequencyTable<int>(Array.Empty<int>());
            Assert.AreEqual(0, this._Table.Count);
            Assert.AreEqual(0, this._Table.Frequencies.Count());
            Assert.AreEqual(0, this._Table.Items.Count());
        }

        [TestMethod]
        public void TestConstructor_IEnumerable_NotEmpty()
        {
            int[] sample = FrequencyTableTest._Sample(1, 10, out Dictionary<int, int> dictionary);
            this._Table = new FrequencyTable<int>(sample);
            Assert.AreEqual(sample.Length, this._Table.Count);

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
            this._Table = new FrequencyTable<int>(this._Table);
            Assert.AreEqual(0, this._Table.Count);
            Assert.AreEqual(0, this._Table.Frequencies.Count());
            Assert.AreEqual(0, this._Table.Items.Count());
        }

        [TestMethod]
        public void TestConstructor_FrequencyTable_NotEmpty()
        {
            int[] sample = FrequencyTableTest._Sample(1, 10, out Dictionary<int, int> dictionary);
            this._Table = new FrequencyTable<int>(sample);
            FrequencyTable<int> table = new FrequencyTable<int>(this._Table);
            Assert.AreEqual(this._Table.Count, table.Count);
            Assert.AreEqual(this._Table.Frequencies.Count(), table.Frequencies.Count());
            Assert.AreEqual(this._Table.Items.Count(), table.Items.Count());
            
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

#region Clear tests
        [TestMethod]
        public void TestClear_Empty()
        {
            this._Table.Clear();
            Assert.AreEqual(0, this._Table.Count);
            Assert.AreEqual(0, this._Table.Frequencies.Count());
            Assert.AreEqual(0, this._Table.Items.Count());
        }

        [TestMethod]
        public void TestClear_NotEmpty_MultipleItems()
        {
            int[] sample = FrequencyTableTest._Sample();
            this._Table = new FrequencyTable<int>(sample);
            this._Table.Clear();
            Assert.AreEqual(0, this._Table.Count);
            Assert.AreEqual(0, this._Table.Frequencies.Count());
            Assert.AreEqual(0, this._Table.Items.Count());
        }

        [TestMethod]
        public void TestClear_NotEmpty_SingleItem()
        {
            const int CONSTANT = 10;
            this._Table[CONSTANT] = CONSTANT;
            this._Table.Clear();
            Assert.AreEqual(0, this._Table.Count);
            Assert.AreEqual(0, this._Table.Frequencies.Count());
            Assert.AreEqual(0, this._Table.Items.Count());
        }
#endregion    
    }
}
