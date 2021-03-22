using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.System.Numerics;

namespace Shipstone.SystemTest
{
    [TestClass]
    public class MatrixTest
    {
        private const int _Columns = 7;
        private const int _Rows = 8;

        private static readonly Random _Random;

        private Matrix _Matrix;

        static MatrixTest() => MatrixTest._Random = new Random();

        private static void _AssertMatrixNotNullEquals(Matrix a, Matrix b)
        {
            Assert.IsNotNull(a);
            Assert.IsNotNull(b);
            Assert.AreEqual(a.Columns, b.Columns);
            Assert.AreEqual(a.Rows, b.Rows);

            for (int i = 0; i < a.Rows; i ++)
            {
                for (int j = 0; j < a.Columns; j ++)
                {
                    Assert.AreEqual(a[i, j], b[i, j]);
                }
            }
        }

        [TestInitialize]
        public void Initialize() => this._Matrix = new Matrix(MatrixTest._Rows, MatrixTest._Columns);

        [TestMethod]
        public void TestItem_InvalidRange()
        {
            const String COLUMN_HIGH = "column is greater than or equal to Matrix.Columns.";
            const String COLUMN_LOW = "column is less than 0.";
            const String ROW_HIGH = "row is greater than or equal to Matrix.Rows.";
            const String ROW_LOW = "row is less than 0.";
            Exception ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[Int32.MinValue, 0]);
            Assert.AreEqual(ROW_LOW, ex.Message);
            ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[-1, 0]);
            Assert.AreEqual(ROW_LOW, ex.Message);
            ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[0, Int32.MinValue]);
            Assert.AreEqual(COLUMN_LOW, ex.Message);
            ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[0, -1]);
            Assert.AreEqual(COLUMN_LOW, ex.Message);
            ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[Int32.MinValue, 0] = 0);
            Assert.AreEqual(ROW_LOW, ex.Message);
            ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[-1, 0] = 0);
            Assert.AreEqual(ROW_LOW, ex.Message);
            ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[0, Int32.MinValue] = 0);
            Assert.AreEqual(COLUMN_LOW, ex.Message);
            ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[0, -1] = 0);
            Assert.AreEqual(COLUMN_LOW, ex.Message);

            if (MatrixTest._Rows < Int32.MaxValue)
            {
                ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[Int32.MaxValue, 0]);
                Assert.AreEqual(ROW_HIGH, ex.Message);
                ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[Int32.MaxValue, 0] = 0);
                Assert.AreEqual(ROW_HIGH, ex.Message);
            }

            if (MatrixTest._Columns < Int32.MaxValue)
            {
                ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[0, Int32.MaxValue]);
                Assert.AreEqual(COLUMN_HIGH, ex.Message);
                ex = Assert.ThrowsException<IndexOutOfRangeException>(() => this._Matrix[0, Int32.MaxValue] = 0);
                Assert.AreEqual(COLUMN_HIGH, ex.Message);
            }
        }

        [TestMethod]
        public void TestItem_ValidRange_Empty()
        {
            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    Assert.AreEqual(0.0, this._Matrix[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestItem_ValidRange_NotEmpty()
        {
            double[,] matrix = new double[this._Matrix.Rows, this._Matrix.Columns];

            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    matrix[i, j] = this._Matrix[i, j] = MatrixTest._Random.NextDouble();
                }
            }

            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    Assert.AreEqual(matrix[i, j], this._Matrix[i, j]);
                }
            }
        }

#region Constructor tests
        [TestMethod]
        public void TestConstructor_Int32_Int32_InvalidRange()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(Int32.MinValue, MatrixTest._Columns));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(-1, MatrixTest._Columns));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(0, MatrixTest._Columns));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(MatrixTest._Rows, Int32.MinValue));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(MatrixTest._Rows, -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(MatrixTest._Rows, 0));
        }

        [TestMethod]
        public void TestConstructor_Int32_Int32_ValidRange()
        {
            Assert.AreEqual(MatrixTest._Columns, this._Matrix.Columns);
            Assert.AreEqual(MatrixTest._Rows, this._Matrix.Rows);

            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    Assert.AreEqual(0.0, this._Matrix[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestConstructor_Matrix_Empty() => MatrixTest._AssertMatrixNotNullEquals(this._Matrix, new(this._Matrix));

        [TestMethod]
        public void TestConstructor_Matrix_NotEmpty()
        {
            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    this._Matrix[i, j] = MatrixTest._Random.NextDouble();
                }
            }

            MatrixTest._AssertMatrixNotNullEquals(this._Matrix, new(this._Matrix));
        }

        [TestMethod]
        public void TestConstructor_Matrix_Null() => Assert.ThrowsException<ArgumentNullException>(() => new Matrix(null));
#endregion
    }
}