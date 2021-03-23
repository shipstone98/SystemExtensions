using System;

namespace Shipstone.System.Numerics
{
    /// <summary>
    /// Represents a matrix of specified size.
    /// </summary>
    public class Matrix : IEquatable<Matrix>
    {
        private readonly double[,] _Array;
        private readonly int _Columns;
        private readonly int _Rows;

        /// <summary>
        /// Gets the number of columns in the <see cref="Matrix" />.
        /// </summary>
        /// <value>The number of columns in the <see cref="Matrix" />.</value>
        public int Columns => this._Columns;

        public bool IsIdentity => throw new NotImplementedException();

        /// <summary>
        /// Gets the number of rows in the <see cref="Matrix" />.
        /// </summary>
        /// <value>The number of rows in the <see cref="Matrix" />.</value>
        public int Rows => this._Rows;

        /// <summary>
        /// Gets or sets the value at the specified entry in the <see cref="Matrix" />.
        /// </summary>
        /// <param name="row">The zero-based row index of the entry to get or set.</param>
        /// <param name="column">The zero-based column index of the entry to get or set.</param>
        /// <value>The value at the specified entry.</value>
        /// <exception cref="IndexOutOfRangeException"><c><paramref name="row" /></c> is less than 0 -or- <c><paramref name="row" /></c> is greater than or equal to <see cref="Matrix.Rows" /> -or- <c><paramref name="column" /></c> is less than 0 -or- <c><paramref name="column" /></c> is greater than or equal to <see cref="Matrix.Columns" />.</exception>
        public double this[int row, int column]
        {
            get
            {
                this._CheckRowIndex(row);
                this._CheckColumnIndex(column);
                return this._Array[row, column];
            }

            set
            {
                this._CheckRowIndex(row);
                this._CheckColumnIndex(column);
                this._Array[row, column] = value;
            }
        }

        private Matrix(int rows, int columns, bool check)
        {
            if (check)
            {
                if (rows <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof (rows));
                }

                if (columns <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof (columns));
                }
            }

            this._Array = new double[rows, columns];
            this._Columns = columns;
            this._Rows = rows;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix" /> class that is empty and contains the specified number of rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows in the <see cref="Matrix" />.</param>
        /// <param name="columns">The number of columns in the <see cref="Matrix" />.</param>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="rows" /></c> is less than or equal to 0 -or- <c><paramref name="columns" /></c> is less than or equal to 0.</exception>
        public Matrix(int rows, int columns) : this(rows, columns, true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix" /> class that contains values copied from the specified matrix and has an equal number of rows and columns.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix" /> to copy.</param>
        /// <exception cref="ArgumentNullException"><c><paramref name="matrix" /></c> is <c>null</c>.</exception>
        public Matrix(Matrix matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof (matrix));
            }

            this._Array = new double[matrix._Rows, matrix._Columns];
            this._Columns = matrix._Columns;
            this._Rows = matrix._Rows;
            Array.Copy(matrix._Array, this._Array, this._Rows * this._Columns);
        }

        private Matrix _Add(Matrix matrix, bool subtract)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof (matrix));
            }

            if (this._Rows != matrix._Rows)
            {
                throw new ArgumentException($"Matrix.Rows is not equal to the number of rows in {nameof (matrix)}.");
            }

            if (this._Columns != matrix._Columns)
            {
                throw new ArgumentException($"Matrix.Columns is not equal to the number of columns in {nameof (matrix)}.");
            }

            Matrix result = new Matrix(this._Rows, this._Columns);

            if (subtract)
            {
                for (int i = 0; i < this._Rows; i ++)
                {
                    for (int j = 0; j < this._Columns; j ++)
                    {
                        result._Array[i, j] = this._Array[i, j] - matrix._Array[i, j];
                    }
                }
            }

            else
            {
                for (int i = 0; i < this._Rows; i ++)
                {
                    for (int j = 0; j < this._Columns; j ++)
                    {
                        result._Array[i, j] = this._Array[i, j] + matrix._Array[i, j];
                    }
                }
            }

            return result;
        }

        private void _CheckColumnIndex(int column)
        {
            if (column < 0)
            {
                throw new IndexOutOfRangeException(nameof (column) + " is less than 0.");
            }

            if (column >= this._Columns)
            {
                throw new IndexOutOfRangeException(nameof (column) + " is greater than or equal to Matrix.Columns.");
            }
        }

        private void _CheckRowIndex(int row)
        {
            if (row < 0)
            {
                throw new IndexOutOfRangeException(nameof (row) + " is less than 0.");
            }

            if (row >= this._Rows)
            {
                throw new IndexOutOfRangeException(nameof (row) + " is greater than or equal to Matrix.Rows.");
            }
        }

        /// <summary>
        /// Adds <c><paramref name="matrix" /></c> to the current <see cref="Matrix" /> instance and returns the result as a new <see cref="Matrix" />.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix" /> to add to the current instance.</param>
        /// <returns>The result of <c><paramref name="matrix" /></c> added to the current <see cref="Matrix" /> instance.</returns>
        /// <exception cref="ArgumentException"><see cref="Matrix.Rows" /> is not equal to the number of rows in <c><paramref name="matrix" /></c> -or- <see cref="Matrix.Columns" /> is not equal to the number of columns in <c><paramref name="matrix" /></c>.</exception>
        /// <exception cref="ArgumentNullException"><c><paramref name="matrix" /></c> is <c>null</c>.</exception>
        public Matrix Add(Matrix matrix) => this._Add(matrix, false);

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns><c>true</c> if <c><paramref name="obj" /></c> is an instance of <see cref="Matrix" /> and equals the value of this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(Object obj) => this.Equals(obj as Matrix);

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="Matrix" />.
        /// </summary>
        /// <param name="matrix">A <see cref="Matrix" /> to compare with this instance.</param>
        /// <returns><c>true</c> if <c><paramref name="matrix" /></c> has the same number of rows and columns and values in all entries as this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(Matrix matrix)
        {
            if (matrix is null || !(this._Rows == matrix._Rows && this._Columns == matrix._Columns))
            {
                return false;
            }

            for (int i = 0; i < this._Rows; i ++)
            {
                for (int j = 0; j < this._Columns; j ++)
                {
                    if (this._Array[i, j] != matrix._Array[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            int hashCode = this._Rows.GetHashCode() ^ this._Columns.GetHashCode();

            for (int i = 0; i < this._Rows; i ++)
            {
                for (int j = 0; j < this._Columns; j ++)
                {
                    hashCode ^= this._Array[i, j].GetHashCode();
                }
            }

            return hashCode ^ 31;
        }

        public Matrix Multiply(double scalar) => throw new NotImplementedException();

        /// <summary>
        /// Multiples the current <see cref="Matrix" /> instance by <c><paramref name="matrix" /></c> and returns the result as a new <see cref="Matrix" />.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix" /> to multiply the current instance by.</param>
        /// <returns>The result of the current <see cref="Matrix" /> instance multiplied by <c><paramref name="matrix" /></c>.</returns>
        /// <exception cref="ArgumentException"><see cref="Matrix.Columns" /> is not equal to the number of rows in <c><paramref name="matrix" /></c>.</exception>
        /// <exception cref="ArgumentNullException"><c><paramref name="matrix" /></c> is <c>null</c>.</exception>
        public Matrix Multiply(Matrix matrix)
        {
            if (matrix is null)
            {
                throw new ArgumentNullException(nameof (matrix));
            }

            if (this._Columns != matrix._Rows)
            {
                throw new ArgumentException($"Matrix.Columns is not equal to the number of rows in {nameof (matrix)}.");
            }

            Matrix result = new Matrix(this._Rows, this._Columns);

            for (int i = 0; i < result._Rows; i ++)
            {
                for (int j = 0; j < result._Columns; j ++)
                {
                    for (int k = 0; k < this._Columns; k ++)
                    {
                        result._Array[i, j] += this._Array[i, k] * matrix._Array[k, j];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Subtracts <c><paramref name="matrix" /></c> from the current <see cref="Matrix" /> instance and returns the result as a new <see cref="Matrix" />.
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix" /> to subtract from the current instance.</param>
        /// <returns>The result of <c><paramref name="matrix" /></c> subtracted from the current <see cref="Matrix" /> instance.</returns>
        /// <exception cref="ArgumentException"><see cref="Matrix.Rows" /> is not equal to the number of rows in <c><paramref name="matrix" /></c> -or- <see cref="Matrix.Columns" /> is not equal to the number of columns in <c><paramref name="matrix" /></c>.</exception>
        /// <exception cref="ArgumentNullException"><c><paramref name="matrix" /></c> is <c>null</c>.</exception>
        public Matrix Subtract(Matrix matrix) => this._Add(matrix, true);

        public override String ToString() => this.ToString(false);
        public String ToString(bool includeBorders) => throw new NotImplementedException();

        /// <summary>
        /// Creates an identity matrix of the specified size.
        /// </summary>
        /// <param name="size">The number of rows and columns in the <see cref="Matrix" />.</param>
        /// <returns>A new <see cref="Matrix" /> that is the identity matrix for its <c><paramref name="size" /></c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="size" /></c> is less than or equal to 0.</exception>
        public static Matrix CreateIndentity(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof (size));
            }

            Matrix matrix = new Matrix(size, size, false);

            for (int i = 0; i < size; i ++)
            {
                matrix._Array[i, i] = 1;
            }

            return matrix;
        }

        /// <summary>
        /// Determines whether two matrices are equal.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix" /> to compare, or <c>null</c>.</param>
        /// <param name="b">The second <see cref="Matrix" /> to compare, or <c>null</c>.</param>
        /// <returns><c>true</c> if <c><paramref name="a" /></c> and <c><paramref name="b" /></c> contain the same number of rows and columns and values in all entries; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Matrix a, Matrix b) => a is null ? b is null : a.Equals(b);

        /// <summary>
        /// Determines whether two matrices are unequal.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix" /> to compare, or <c>null</c>.</param>
        /// <param name="b">The second <see cref="Matrix" /> to compare, or <c>null</c>.</param>
        /// <returns><c>true</c> if <c><paramref name="a" /></c> and <c><paramref name="b" /></c> contain a different number of rows or columns or values in any entry; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Matrix a, Matrix b) => !(a == b);
    }
}