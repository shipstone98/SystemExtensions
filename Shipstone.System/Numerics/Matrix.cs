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

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix" /> class that is empty and contains the specified number of rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows in the <see cref="Matrix" />.</param>
        /// <param name="columns">The number of columns in the <see cref="Matrix" />.</param>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="rows" /></c> is less than or equal to 0 -or- <c><paramref name="columns" /></c> is less than or equal to 0.</exception>
        public Matrix(int rows, int columns)
        {
            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof (rows));
            }

            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof (columns));
            }

            this._Array = new double[rows, columns];
            this._Columns = columns;
            this._Rows = rows;
        }

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

        private void _CheckColumnIndex(int column)
        {
            if (column < 0)
            {
                throw new IndexOutOfRangeException(nameof (column) + " is less than 0.");
            }

            if (column >= this.Rows)
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

            if (row >= this.Rows)
            {
                throw new IndexOutOfRangeException(nameof (row) + " is greater than or equal to Matrix.Rows.");
            }
        }

        public Matrix Add(Matrix matrix) => throw new NotImplementedException();
        public override bool Equals(Object obj) => this.Equals(obj as Matrix);
        public bool Equals(Matrix matrix) => throw new NotImplementedException();
        public override int GetHashCode() => throw new NotImplementedException();
        public Matrix Multiply(double scalar) => throw new NotImplementedException();
        public Matrix Multiply(Matrix matrix) => throw new NotImplementedException();
        public Matrix Subtract(Matrix matrix) => throw new NotImplementedException();
        public override String ToString() => this.ToString(false);
        public String ToString(bool includeBorders) => throw new NotImplementedException();

        public static Matrix CreateIndentity(int size) => throw new NotImplementedException();

        public static bool operator ==(Matrix a, Matrix b) => a is null ? b is null : a.Equals(b);
        public static bool operator !=(Matrix a, Matrix b) => !(a == b);
    }
}