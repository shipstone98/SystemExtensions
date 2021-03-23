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
        public void TestIsIdentity_EqualSize_Identity()
        {
            this._Matrix = Matrix.CreateIndentity(MatrixTest._Rows);
            Assert.IsTrue(this._Matrix.IsIdentity);
        }

        [TestMethod]
        public void TestIsIdentity_EqualSize_NotIdentity()
        {
            this._Matrix = Matrix.CreateIndentity(MatrixTest._Rows);
            this._Matrix[0, 0] = 0;
            Assert.IsFalse(this._Matrix.IsIdentity);
        }

        [TestMethod]
        public void TestIsIdentity_NotEqualSize()
        {
            this._Matrix = new Matrix(MatrixTest._Rows, MatrixTest._Rows + 1);
            Assert.IsFalse(this._Matrix.IsIdentity);
        }

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

#region Add tests
        [TestMethod]
        public void TestAdd_EqualSize_Empty()
        {
            Matrix matrix = new(this._Matrix);
            Matrix result = this._Matrix.Add(matrix);
            Assert.AreEqual(result.Rows, this._Matrix.Rows);
            Assert.AreEqual(result.Columns, this._Matrix.Columns);

            for (int i = 0; i < result.Rows; i ++)
            {
                for (int j = 0; j < result.Columns; j ++)
                {
                    Assert.AreEqual(0.0, result[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestAdd_EqualSize_NotEmpty()
        {
            double[,] answers = new double[this._Matrix.Rows, this._Matrix.Columns];
            Matrix matrix = new(this._Matrix);

            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    double n = MatrixTest._Random.NextDouble();
                    this._Matrix[i, j] = n;
                    matrix[i, j] = 2 * n;
                    answers[i, j] = 3 * n;
                }
            }

            Matrix result = this._Matrix.Add(matrix);
            Assert.AreEqual(result.Rows, this._Matrix.Rows);
            Assert.AreEqual(result.Columns, this._Matrix.Columns);

            for (int i = 0; i < result.Rows; i ++)
            {
                for (int j = 0; j < result.Columns; j ++)
                {
                    Assert.AreEqual(answers[i, j], result[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestAdd_NotEqualSize()
        {
            Matrix matrix = new Matrix(this._Matrix.Rows + 1, this._Matrix.Columns);
            Exception ex = Assert.ThrowsException<ArgumentException>(() => this._Matrix.Add(matrix));
            Assert.AreEqual("Matrix.Rows is not equal to the number of rows in matrix.", ex.Message);
            matrix = new Matrix(this._Matrix.Rows, this._Matrix.Columns + 1);
            ex = Assert.ThrowsException<ArgumentException>(() => this._Matrix.Add(matrix));
            Assert.AreEqual("Matrix.Columns is not equal to the number of columns in matrix.", ex.Message);
        }

        [TestMethod]
        public void TestAdd_Null() => Assert.ThrowsException<ArgumentNullException>(() => this._Matrix.Add(null));

        [TestMethod]
        public void TestAddDirect_NotNull()
        {
            Matrix a = new Matrix(2, 3), b = new Matrix(2, 2);
            a[0, 0] = 1;
            a[0, 1] = 3;
            a[0, 2] = 2;
            a[1, 0] = 2;
            a[1, 1] = 3;
            a[1, 2] = 1;
            b[0, 0] = 1;
            b[0, 1] = 6;
            b[1, 1] = 1;
            Matrix c = a.AddDirect(b);
            Assert.AreEqual(4, c.Rows);
            Assert.AreEqual(5, c.Columns);
            double[,] answers = new double[4, 5];
            answers[0, 0] = 1;
            answers[0, 1] = 3;
            answers[0, 2] = 2;
            answers[1, 0] = 2;
            answers[1, 1] = 3;
            answers[1, 2] = 1;
            answers[2, 3] = 1;
            answers[2, 4] = 6;
            answers[3, 4] = 1;

            for (int i = 0; i < c.Rows; i ++)
            {
                for (int j = 0; j < c.Columns; j ++)
                {
                    Assert.AreEqual(answers[i, j], c[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestAddDirect_Null() => Assert.ThrowsException<ArgumentNullException>(() => this._Matrix.AddDirect(null));
#endregion

        [TestMethod]
        public void TestCreateIdentity_InvalidSize()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Matrix.CreateIndentity(Int32.MinValue));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Matrix.CreateIndentity(-1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Matrix.CreateIndentity(0));
        }

        [TestMethod]
        public void TestCreateIdentity_ValidSize()
        {
            const int SIZE = MatrixTest._Rows;
            this._Matrix = Matrix.CreateIndentity(SIZE);
            Assert.AreEqual(SIZE, this._Matrix.Columns);
            Assert.AreEqual(SIZE, this._Matrix.Rows);

            for (int i = 0; i < SIZE; i ++)
            {
                for (int j = 0; j < SIZE; j ++)
                {
                    Assert.AreEqual(i == j ? 1.0 : 0.0, this._Matrix[i, j]);
                }
            }
        }

#region Equals test
        [TestMethod]
        public void TestEquals_Equal()
        {
            Matrix matrix = new Matrix(this._Matrix.Rows, this._Matrix.Columns);

            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    matrix[i, j] = this._Matrix[i, j] = MatrixTest._Random.NextDouble();
                }
            }

            Assert.AreEqual(this._Matrix, matrix);
            Assert.IsTrue(this._Matrix == matrix);
            Assert.IsFalse(this._Matrix != matrix);
            Assert.IsTrue(Object.Equals(this._Matrix, matrix));
            Assert.IsTrue(this._Matrix.Equals(matrix as Matrix));
        }

        [TestMethod]
        public void TestEquals_IncorrectType() => Assert.IsFalse(this._Matrix.Equals(null as String));

        [TestMethod]
        public void TestEquals_Null()
        {
            Assert.IsFalse(this._Matrix.Equals(null as Object));
            Assert.IsFalse(this._Matrix.Equals(null));
        }
        [TestMethod]
        public void TestEquals_Unequal()
        {
            Matrix matrix = new Matrix(this._Matrix.Rows, this._Matrix.Columns);

            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    this._Matrix[i, j] = MatrixTest._Random.NextDouble();
                    matrix[i, j] = this._Matrix[i, j] * 2;
                }
            }

            Assert.AreNotEqual(this._Matrix, matrix);
            Assert.IsFalse(this._Matrix == matrix);
            Assert.IsTrue(this._Matrix != matrix);
            Assert.IsFalse(Object.Equals(this._Matrix, matrix));
            Assert.IsFalse(this._Matrix.Equals(matrix as Matrix));
        }
#endregion

        [TestMethod]
        public void TestGetHashCode()
        {
            Matrix matrix = new Matrix(this._Matrix.Rows, this._Matrix.Columns);

            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    matrix[i, j] = this._Matrix[i, j] = MatrixTest._Random.NextDouble();
                }
            }

            Assert.AreEqual(this._Matrix.GetHashCode(), matrix.GetHashCode());
        }

#region Multiply tests
        [TestMethod]
        public void TestMultiply_Scalar()
        {
            const double SCALAR = 4.7;
            double[,] answers = new double[this._Matrix.Rows, this._Matrix.Columns];

            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    double n = MatrixTest._Random.NextDouble();
                    this._Matrix[i, j] = n;
                    answers[i, j] = n * SCALAR;
                }
            }

            Matrix result = this._Matrix.Multiply(SCALAR);
            Assert.AreEqual(result.Rows, this._Matrix.Rows);
            Assert.AreEqual(result.Columns, this._Matrix.Columns);

            for (int i = 0; i < result.Rows; i ++)
            {
                for (int j = 0; j < result.Columns; j ++)
                {
                    Assert.AreEqual(0, MathExtensionsTest._CompareDouble(answers[i, j], result[i, j]));
                }
            }
        }

        [TestMethod]
        public void TestMultiply_EqualSize()
        {
            const int SIZE = 5; // Changing this breaks the test
            Matrix matrix1 = new(SIZE, SIZE);

            for (int i = 0; i < matrix1.Rows; i ++)
            {
                for (int j = 0; j < matrix1.Columns; j ++)
                {
                    matrix1[i, j] = i + j;
                }
            }

            Matrix matrix2 = new(matrix1);
            Matrix result = matrix1.Multiply(matrix2);
            double[][] resultArray = new double[SIZE][];
            resultArray[0] = new double[SIZE] { 30, 40, 50, 60, 70 };
            resultArray[1] = new double[SIZE] { 40, 55, 70, 85, 100 };
            resultArray[2] = new double[SIZE] { 50, 70, 90, 110, 130 };
            resultArray[3] = new double[SIZE] { 60, 85, 110, 135, 160 };
            resultArray[4] = new double[SIZE] { 70, 100, 130, 160, 190 };

            for (int i = 0; i < result.Rows; i ++)
            {
                for (int j = 0; j < result.Columns; j ++)
                {
                    Assert.AreEqual(0, MathExtensionsTest._CompareDouble(resultArray[i][j], result[i, j]));
                }
            }
        }

        [TestMethod]
        public void TestMultiply_InvalidSize()
        {
            Matrix matrix = new(MatrixTest._Rows + MatrixTest._Columns, MatrixTest._Columns + MatrixTest._Rows + 1);
            Exception ex = Assert.ThrowsException<ArgumentException>(() => this._Matrix.Multiply(matrix));
            Assert.AreEqual("Matrix.Columns is not equal to the number of rows in matrix.", ex.Message);
            matrix = new(this._Matrix);
            ex = Assert.ThrowsException<ArgumentException>(() => this._Matrix.Multiply(matrix));
            Assert.AreEqual("Matrix.Columns is not equal to the number of rows in matrix.", ex.Message);
        }

        [TestMethod]
        public void TestMultiply_Null() => Assert.ThrowsException<ArgumentNullException>(() => this._Matrix.Multiply(null));

        [TestMethod]
        public void TestMultiply_ValidSize()
        {
            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    this._Matrix[i, j] = i + j;
                }
            }
            
            Matrix matrix = new Matrix(this._Matrix.Columns, MatrixTest._Rows + 1);

            for (int i = 0; i < matrix.Rows; i ++)
            {
                for (int j = 0; j < matrix.Columns; j ++)
                {
                    matrix[i, j] = i + j;
                }
            }

            Matrix result = this._Matrix.Multiply(matrix);
            double[][] resultArray = new double[result.Rows][];
            resultArray[0] = new double[MatrixTest._Rows + 1] { 91, 112, 133, 154, 175, 196, 217, 238, 259 };
            resultArray[1] = new double[MatrixTest._Rows + 1] { 112, 140, 168, 196, 224, 252, 280, 308, 336 };
            resultArray[2] = new double[MatrixTest._Rows + 1] { 133, 168, 203, 238, 273, 308, 343, 378, 413 };
            resultArray[3] = new double[MatrixTest._Rows + 1] { 154, 196, 238, 280, 322, 364, 406, 448, 490 };
            resultArray[4] = new double[MatrixTest._Rows + 1] { 175, 224, 273, 322, 371, 420, 469, 518, 567 };
            resultArray[5] = new double[MatrixTest._Rows + 1] { 196, 252, 308, 364, 420, 476, 532, 588, 644 };
            resultArray[6] = new double[MatrixTest._Rows + 1] { 217, 280, 343, 406, 469, 532, 595, 658, 721 };
            resultArray[7] = new double[MatrixTest._Rows + 1] { 238, 308, 378, 448, 518, 588, 658, 728, 798 };

            for (int i = 0; i < result.Rows; i ++)
            {
                for (int j = 0; j < result.Columns; j ++)
                {
                    Assert.AreEqual(0, MathExtensionsTest._CompareDouble(resultArray[i][j], result[i, j]));
                }
            }
        }
#endregion

#region Subtract tests
        [TestMethod]
        public void TestSubtract_EqualSize_Empty()
        {
            Matrix matrix = new(this._Matrix);
            Matrix result = this._Matrix.Subtract(matrix);
            Assert.AreEqual(result.Rows, this._Matrix.Rows);
            Assert.AreEqual(result.Columns, this._Matrix.Columns);

            for (int i = 0; i < result.Rows; i ++)
            {
                for (int j = 0; j < result.Columns; j ++)
                {
                    Assert.AreEqual(0.0, result[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestSubtract_EqualSize_NotEmpty()
        {
            double[,] answers = new double[this._Matrix.Rows, this._Matrix.Columns];
            Matrix matrix = new(this._Matrix);

            for (int i = 0; i < this._Matrix.Rows; i ++)
            {
                for (int j = 0; j < this._Matrix.Columns; j ++)
                {
                    double n = MatrixTest._Random.NextDouble();
                    this._Matrix[i, j] = n;
                    matrix[i, j] = 2 * n;
                    answers[i, j] = n * -1;
                }
            }

            Matrix result = this._Matrix.Subtract(matrix);
            Assert.AreEqual(result.Rows, this._Matrix.Rows);
            Assert.AreEqual(result.Columns, this._Matrix.Columns);

            for (int i = 0; i < result.Rows; i ++)
            {
                for (int j = 0; j < result.Columns; j ++)
                {
                    Assert.AreEqual(answers[i, j], result[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestSubtract_NotEqualSize()
        {
            Matrix matrix = new Matrix(this._Matrix.Rows + 1, this._Matrix.Columns);
            Exception ex = Assert.ThrowsException<ArgumentException>(() => this._Matrix.Subtract(matrix));
            Assert.AreEqual("Matrix.Rows is not equal to the number of rows in matrix.", ex.Message);
            matrix = new Matrix(this._Matrix.Rows, this._Matrix.Columns + 1);
            ex = Assert.ThrowsException<ArgumentException>(() => this._Matrix.Subtract(matrix));
            Assert.AreEqual("Matrix.Columns is not equal to the number of columns in matrix.", ex.Message);
        }

        [TestMethod]
        public void TestSubtract_Null() => Assert.ThrowsException<ArgumentNullException>(() => this._Matrix.Subtract(null));
#endregion
    }
}