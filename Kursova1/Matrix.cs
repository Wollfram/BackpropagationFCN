using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Kursova1 {

    /// <summary>Matrix provides the fundamental operations of numerical linear algebra.</summary>
    [Serializable]
    public class Matrix {
        private double[][] data;
        private int rows;
        private int columns;

        private static Random random = new Random();

        /// <summary>
        /// Apply delegate   double Func (double) to each elements and return new Matrix
        /// </summary>
        /// <param name="funk">delegate double Func (double)</param>
        /// <returns></returns>
        public Matrix ApplyFunc(Func<double, double> funk) {
            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) { x[i][j] = funk(data[i][j]); }
            }

            return X;
        }

        /// <summary>
        /// add row to the matrix
        /// </summary>
        /// <param name="addition"> new row</param>
        /// <param name="idx">where insert (-1 - to the end)</param>
        /// <returns></returns>
        public Matrix ComposeAddRow(Matrix addition, int idx) {
            if (addition.columns != this.columns || addition.rows != 1) throw new Exception("Unnable to compose row");
            if (idx == -1) idx = rows;
            if (idx > this.rows || idx < 0) throw new ArgumentOutOfRangeException(nameof(idx));

            Matrix X = new Matrix(rows + 1, columns);
            double[][] x = X.Array;
            for (int i = 0; i < idx; i++) {
                for (int j = 0; j < columns; j++) { x[i][j] = data[i][j]; }
            }

            for (int j = 0; j < columns; j++) { x[idx][j] = addition[0, j]; }
            for (int i = idx; i < rows; i++) {
                for (int j = 0; j < columns; j++) { x[i + 1][j] = data[i][j]; }
            }

            return X;
        }

        /// <summary>
        /// add row to the matrix
        /// </summary>
        /// <param name="addition"> new row</param>
        /// <param name="idx">where insert (-1 - to the end)</param>
        /// <returns></returns>
        public Matrix ComposeAddRow(double[] addition, int idx) {
            if (addition.Length != this.columns) throw new Exception("Unnable to compose row");
            if (idx == -1) idx = rows;
            if (idx > this.rows || idx < 0) throw new ArgumentOutOfRangeException(nameof(idx));
            Matrix X = new Matrix(rows + 1, columns);
            double[][] x = X.Array;
            for (int i = 0; i < idx; i++) {
                for (int j = 0; j < columns; j++) { x[i][j] = data[i][j]; }
            }

            for (int j = 0; j < columns; j++) { x[idx][j] = addition[j]; }
            for (int i = idx; i < rows; i++) {
                for (int j = 0; j < columns; j++) { x[i + 1][j] = data[i][j]; }
            }

            return X;
        }

        /// <summary>
        /// add column to the matrix
        /// </summary>
        /// <param name="addition"> new row</param>
        /// <param name="idx">where insert (-1 - to the end)</param>
        /// <returns></returns>
        public Matrix ComposeAddColumn(Matrix addition, int idx) {
            if (addition.rows != this.rows || addition.columns != 1) throw new Exception("Unnable to compose column");
            if (idx == -1) idx = columns;
            if (idx > this.columns - 1 || idx < 0) throw new ArgumentOutOfRangeException(nameof(idx));

            Matrix X = new Matrix(rows, columns + 1);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < idx; j++) { x[i][j] = data[i][j]; }
            }

            for (int j = 0; j < rows; j++) { x[j][idx] = addition[j, 0]; }
            for (int i = 0; i < rows; i++) {
                for (int j = idx; j < columns; j++) { x[i][j + 1] = data[i][j]; }
            }

            return X;
        }

        /// <summary>
        /// add column to the matrix
        /// </summary>
        /// <param name="addition"> new row</param>
        /// <param name="idx">where insert (-1 - to the end)</param>
        /// <returns></returns>
        public Matrix ComposeAddColumn(double[] addition, int idx) {
            if (addition.Length != this.rows) throw new Exception("Unnable to compose column");
            if (idx == -1) idx = columns;
            if (idx > this.columns - 1 || idx < 0) throw new ArgumentOutOfRangeException(nameof(idx));

            Matrix X = new Matrix(rows, columns + 1);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < idx; j++) { x[i][j] = data[i][j]; }
            }

            for (int j = 0; j < rows; j++) { x[j][idx] = addition[j]; }
            for (int i = 0; i < rows; i++) {
                for (int j = idx; j < columns; j++) { x[i][j + 1] = data[i][j]; }
            }

            return X;
        }


        /// <summary>Constructs an empty matrix of the given size.</summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        public Matrix(int rows, int columns) {
            this.rows = rows;
            this.columns = columns;
            this.data = new double[rows][];
            for (int i = 0; i < rows; i++) { this.data[i] = new double[columns]; }
        }

        /// <summary>Constructs a matrix of the given size and assigns a given value to all  elements.</summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        /// <param name="value">Value to assign to the elements.</param>
        public Matrix(int rows, int columns, double value) {
            this.rows = rows;
            this.columns = columns;
            this.data = new double[rows][];

            for (int i = 0; i < rows; i++) { data[i] = new double[columns]; }

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) { data[i][j] = value; }
            }

        }

        /// <summary>Constructs a matrix from the given array.</summary>
        /// <param name="value">The array the matrix gets constructed from.</param>
        [CLSCompliant(false)] public Matrix(double[][] value) {
            this.rows = value.Length;
            this.columns = value[0].Length;

            for (int i = 0; i < rows; i++) {
                if (value[i].Length != columns) { throw new ArgumentException("Argument out of range."); }
            }

            this.data = value;
        }

        /// <summary>Determines weather two instances are equal.</summary>
        public override bool Equals(object obj) {
            return Equals(this, (Matrix) obj);
        }

        /// <summary>Determines weather two instances are equal.</summary>
        public static bool Equals(Matrix left, Matrix right) {
            if (((object) left) == ((object) right)) { return true; }

            if ((((object) left) == null) || (((object) right) == null)) { return false; }

            if ((left.Rows != right.Rows) || (left.Columns != right.Columns)) { return false; }

            for (int i = 0; i < left.Rows; i++) {
                for (int j = 0; j < left.Columns; j++) {
                    if (left[i, j] != right[i, j]) { return false; }
                }
            }

            return true;
        }

        /// <summary>Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.</summary>
        public override int GetHashCode() {
            return (this.Rows + this.Columns);
        }

        internal double[][] Array {
            get { return this.data; }
        }

        /// <summary>Returns the number of columns.</summary>
        public int Rows {
            get { return this.rows; }
        }

        /// <summary>Returns the number of columns.</summary>
        public int Columns {
            get { return this.columns; }
        }

        /// <summary>Return <see langword="true"/> if the matrix is a square matrix.</summary>
        public bool Square {
            get { return (rows == columns); }
        }

        /// <summary>Returns <see langword="true"/> if the matrix is symmetric.</summary>
        public bool Symmetric {
            get {
                if (this.Square) {
                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j <= i; j++) {
                            if (data[i][j] != data[j][i]) { return false; }
                        }
                    }

                    return true;
                }

                return false;
            }
        }

        /// <summary>Access the value at the given location.</summary>
        public double this[int row, int column] {
            set { this.data[row][column] = value; }

            get { return this.data[row][column]; }
        }

        ///// <summary>
        ///// returns new matrix, which present one row
        ///// </summary>
        ///// <param name="row">index of row</param>
        ///// <param name="sure">set true</param>
        ///// <returns></returns>
        //public Matrix this[int row, bool sure] {
        //    get {
        //        Matrix rez = new Matrix(1, this.columns);
        //        for (int i = 0; i < this.columns; i++) rez[0, i] = this.data[row][i];
        //        return rez;
        //    }
        //}

        ///// <summary>
        ///// return new matrix, which present one column
        ///// </summary>
        ///// <param name="sure">set true</param>
        ///// <param name="column">index of column</param>
        ///// <returns></returns>
        //public Matrix this[bool sure, int column] {
        //    get {
        //        Matrix rez = new Matrix(this.rows, 1);
        //        for (int i = 0; i < this.columns; i++) rez[i, 0] = this.data[i][column];
        //        return rez;
        //    }
        //}

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="startRow">Start row index</param>
        /// <param name="endRow">End row index</param>
        /// <param name="startColumn">Start column index</param>
        /// <param name="endColumn">End column index</param>
        public Matrix Submatrix(int startRow, int endRow, int startColumn, int endColumn) {
            if ((startRow > endRow) || (startColumn > endColumn) || (startRow < 0) || (startRow >= this.rows) || (endRow < 0) || (endRow >= this.rows) ||
                (startColumn < 0) || (startColumn >= this.columns) || (endColumn < 0) || (endColumn >= this.columns)) {
                throw new ArgumentException("Argument out of range.");
            }

            Matrix X = new Matrix(endRow - startRow + 1, endColumn - startColumn + 1);
            double[][] x = X.Array;
            for (int i = startRow; i <= endRow; i++) {
                for (int j = startColumn; j <= endColumn; j++) { x[i - startRow][j - startColumn] = data[i][j]; }
            }

            return X;
        }

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="rowIndexes">Array of row indices</param>
        /// <param name="columnIndexes">Array of column indices</param>
        public Matrix Submatrix(int[] rowIndexes, int[] columnIndexes) {
            Matrix X = new Matrix(rowIndexes.Length, columnIndexes.Length);
            double[][] x = X.Array;
            for (int i = 0; i < rowIndexes.Length; i++) {
                for (int j = 0; j < columnIndexes.Length; j++) {
                    if ((rowIndexes[i] < 0) || (rowIndexes[i] >= rows) || (columnIndexes[j] < 0) || (columnIndexes[j] >= columns)) {
                        throw new ArgumentException("Argument out of range.");
                    }

                    x[i][j] = data[rowIndexes[i]][columnIndexes[j]];
                }
            }

            return X;
        }

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="i0">Starttial row index</param>
        /// <param name="i1">End row index</param>
        /// <param name="c">Array of columns indices</param>
        public Matrix Submatrix(int i0, int i1, int[] c) {
            if ((i0 > i1) || (i0 < 0) || (i0 >= this.rows) || (i1 < 0) || (i1 >= this.rows)) { throw new ArgumentException("Argument out of range."); }

            Matrix X = new Matrix(i1 - i0 + 1, c.Length);
            double[][] x = X.Array;
            for (int i = i0; i <= i1; i++) {
                for (int j = 0; j < c.Length; j++) {
                    if ((c[j] < 0) || (c[j] >= columns)) { throw new ArgumentException("Argument out of range."); }

                    x[i - i0][j] = data[i][c[j]];
                }
            }

            return X;
        }

        /// <summary>Returns a sub matrix extracted from the current matrix.</summary>
        /// <param name="r">Array of row indices</param>
        /// <param name="j0">Start column index</param>
        /// <param name="j1">End column index</param>
        public Matrix Submatrix(int[] r, int j0, int j1) {
            if ((j0 > j1) || (j0 < 0) || (j0 >= columns) || (j1 < 0) || (j1 >= columns)) { throw new ArgumentException("Argument out of range."); }

            Matrix X = new Matrix(r.Length, j1 - j0 + 1);
            double[][] x = X.Array;
            for (int i = 0; i < r.Length; i++) {
                for (int j = j0; j <= j1; j++) {
                    if ((r[i] < 0) || (r[i] >= this.rows)) { throw new ArgumentException("Argument out of range."); }

                    x[i][j - j0] = data[r[i]][j];
                }
            }

            return X;
        }

        /// <summary>Creates a copy of the matrix.</summary>
        public Matrix Clone() {
            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) { x[i][j] = data[i][j]; }
            }

            return X;
        }

        /// <summary>Returns the transposed matrix.</summary>
        public Matrix Transpose() {
            Matrix X = new Matrix(columns, rows);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) { x[j][i] = data[i][j]; }
            }

            return X;
        }

        /// <summary>Returns the One Norm for the matrix.</summary>
        /// <value>The maximum column sum.</value>
        public double Norm1 {
            get {
                double f = 0;
                for (int j = 0; j < columns; j++) {
                    double s = 0;
                    for (int i = 0; i < rows; i++) { s += Math.Abs(data[i][j]); }

                    f = Math.Max(f, s);
                }
                return f;
            }
        }

        /// <summary>Returns the Infinity Norm for the matrix.</summary>
        /// <value>The maximum row sum.</value>
        public double InfinityNorm {
            get {
                double f = 0;
                for (int i = 0; i < rows; i++) {
                    double s = 0;
                    for (int j = 0; j < columns; j++) s += Math.Abs(data[i][j]);
                    f = Math.Max(f, s);
                }
                return f;
            }
        }

        /// <summary>Returns the Frobenius Norm for the matrix.</summary>
        /// <value>The square root of sum of squares of all elements.</value>
        public double FrobeniusNorm {
            get {
                double f = 0;
                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < columns; j++) { f = Hypotenuse(f, data[i][j]); }
                }

                return f;
            }
        }

        /// <summary>Unary minus.</summary>
        public static Matrix Negate(Matrix value) {
            if (value == null) { throw new ArgumentNullException("value"); }

            int rows = value.Rows;
            int columns = value.Columns;
            double[][] data = value.Array;

            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) { x[i][j] = -data[i][j]; }
            }

            return X;
        }

        /// <summary>Unary minus.</summary>
        public static Matrix operator -(Matrix value) {
            if (value == null) { throw new ArgumentNullException("value"); }

            return Negate(value);
        }

        /// <summary>Matrix equality.</summary>
        public static bool operator ==(Matrix left, Matrix right) {
            return Equals(left, right);
        }

        /// <summary>Matrix inequality.</summary>
        public static bool operator !=(Matrix left, Matrix right) {
            return !Equals(left, right);
        }

        /// <summary>Matrix addition.</summary>
        public static Matrix Add(Matrix left, Matrix right) {
            if (left == null) { throw new ArgumentNullException("left"); }

            if (right == null) { throw new ArgumentNullException("right"); }

            int rows = left.Rows;
            int columns = left.Columns;
            double[][] data = left.Array;

            if ((rows != right.Rows) || (columns != right.Columns)) { throw new ArgumentException("Matrix dimension do not match."); }

            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) { x[i][j] = data[i][j] + right[i, j]; }
            }
            return X;
        }

        /// <summary>Matrix simple Multiplication (as adding, operation on each elements).</summary>
        public static Matrix MultiplySimple(Matrix left, Matrix right) {
            if (left == null) { throw new ArgumentNullException("left"); }

            if (right == null) { throw new ArgumentNullException("right"); }

            int rows = left.Rows;
            int columns = left.Columns;
            double[][] data = left.Array;

            if ((rows != right.Rows) || (columns != right.Columns)) { throw new ArgumentException("Matrix dimension do not match."); }

            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) { x[i][j] = data[i][j] * right[i, j]; }
            }
            return X;
        }

        /// <summary>Matrix addition.</summary>
        public static Matrix operator +(Matrix left, Matrix right) {
            if (left == null) { throw new ArgumentNullException("left"); }

            if (right == null) { throw new ArgumentNullException("right"); }

            return Add(left, right);
        }

        /// <summary>Matrix subtraction.</summary>
        public static Matrix Subtract(Matrix left, Matrix right) {
            if (left == null) { throw new ArgumentNullException("left"); }

            if (right == null) { throw new ArgumentNullException("right"); }

            int rows = left.Rows;
            int columns = left.Columns;
            double[][] data = left.Array;

            if ((rows != right.Rows) || (columns != right.Columns)) { throw new ArgumentException("Matrix dimension do not match."); }

            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) { x[i][j] = data[i][j] - right[i, j]; }
            }
            return X;
        }

        /// <summary>Matrix subtraction.</summary>
        public static Matrix operator -(Matrix left, Matrix right) {
            if (left == null) { throw new ArgumentNullException("left"); }

            if (right == null) { throw new ArgumentNullException("right"); }

            return Subtract(left, right);
        }

        /// <summary>Matrix-scalar multiplication.</summary>
        public static Matrix Multiply(Matrix left, double right) {
            if (left == null) { throw new ArgumentNullException("left"); }

            int rows = left.Rows;
            int columns = left.Columns;
            double[][] data = left.Array;

            Matrix X = new Matrix(rows, columns);

            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) { x[i][j] = data[i][j] * right; }
            }

            return X;
        }

        /// <summary>Matrix-scalar multiplication.</summary>
        public static Matrix operator *(Matrix left, double right) {
            if (left == null) { throw new ArgumentNullException("left"); }

            return Multiply(left, right);
        }

        /// <summary>Matrix-matrix multiplication.</summary>
        public static Matrix Multiply(Matrix left, Matrix right) {
            if (left == null) { throw new ArgumentNullException("left"); }

            if (right == null) { throw new ArgumentNullException("right"); }

            int rows = left.Rows;
            double[][] data = left.Array;

            if (right.Rows != left.columns) { throw new ArgumentException("Matrix dimensions are not valid."); }

            int columns = right.Columns;
            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;

            int size = left.columns;
            double[] column = new double[size];
            for (int j = 0; j < columns; j++) {
                for (int k = 0; k < size; k++) { column[k] = right[k, j]; }
                for (int i = 0; i < rows; i++) {
                    double[] row = data[i];
                    double s = 0;
                    for (int k = 0; k < size; k++) { s += row[k] * column[k]; }
                    x[i][j] = s;
                }
            }

            return X;
        }

        /// <summary>Matrix-matrix multiplication.</summary>
        public static Matrix operator *(Matrix left, Matrix right) {
            if (left == null) { throw new ArgumentNullException("left"); }

            if (right == null) { throw new ArgumentNullException("right"); }

            return Multiply(left, right);
        }

        /// <summary>Returns the LHS solution vetor if the matrix is square or the least squares solution otherwise.</summary>
        public Matrix Solve(Matrix rightHandSide) {
            return (rows == columns) ? new LuDecomposition(this).Solve(rightHandSide) : new QrDecomposition(this).Solve(rightHandSide);
        }

        /// <summary>Inverse of the matrix if matrix is square, pseudoinverse otherwise.</summary>
        public Matrix Inverse {
            get { return this.Solve(Diagonal(rows, rows, 1.0)); }
        }

        /// <summary>Determinant if matrix is square.</summary>
        public double Determinant {
            get { return new LuDecomposition(this).Determinant; }
        }

        /// <summary>Returns the trace of the matrix.</summary>
        /// <returns>Sum of the diagonal elements.</returns>
        public double Trace {
            get {
                double trace = 0;
                for (int i = 0; i < Math.Min(rows, columns); i++) { trace += data[i][i]; }
                return trace;
            }
        }

        /// <summary>Returns a matrix filled with random values.</summary>
        public static Matrix Random(int rows, int columns) {
            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                   for (int j = 0; j < columns; j++) { x[i][j] = random.NextDouble(); }
          //      for (int j = 0; j < columns; j++) { x[i][j] = random.NextDouble()*100; }
            }
            return X;
        }

        /// <summary>Returns a diagonal matrix of the given size.</summary>
        public static Matrix Diagonal(int rows, int columns, double value) {
            Matrix X = new Matrix(rows, columns);
            double[][] x = X.Array;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) { x[i][j] = ((i == j) ? value : 0.0); }
            }
            return X;
        }

        /// <summary>Returns the matrix in a textual form.</summary>
        public override string ToString() {
            using (StringWriter writer = new StringWriter(CultureInfo.InvariantCulture)) {
                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < columns; j++) { writer.Write(this.data[i][j] + " "); }

                    writer.WriteLine();
                }

                return writer.ToString();
            }
        }

        private static double Hypotenuse(double a, double b) {
            if (Math.Abs(a) > Math.Abs(b)) {
                double r = b / a;
                return Math.Abs(a) * Math.Sqrt(1 + r * r);
            }

            if (b != 0) {
                double r = a / b;
                return Math.Abs(b) * Math.Sqrt(1 + r * r);
            }

            return 0.0;
        }
    }

    /// <summary>
    ///		Cholesky Decomposition of a symmetric, positive definite matrix.
    ///	</summary>
    /// <remarks>
    ///		For a symmetric, positive definite matrix <c>A</c>, the Cholesky decomposition is a
    ///		lower triangular matrix <c>L</c> so that <c>A = L * L'</c>.
    ///		If the matrix is not symmetric or positive definite, the constructor returns a partial 
    ///		decomposition and sets two internal variables that can be queried using the
    ///		<see cref="Symmetric"/> and <see cref="PositiveDefinite"/> properties.
    ///	</remarks>
    [Serializable]
    public class CholeskyDecomposition {
        private Matrix L;
        private bool symmetric;
        private bool positiveDefinite;

        /// <summary>Construct a Cholesky Decomposition.</summary>
        public CholeskyDecomposition(Matrix value) {
            if (value == null) { throw new ArgumentNullException("value"); }

            if (!value.Square) { throw new ArgumentException("Matrix is not square.", "value"); }

            int dimension = value.Rows;
            L = new Matrix(dimension, dimension);

            double[][] a = value.Array;
            double[][] l = L.Array;

            this.positiveDefinite = true;
            this.symmetric = true;

            for (int j = 0; j < dimension; j++) {
                double[] Lrowj = l[j];
                double d = 0.0;
                for (int k = 0; k < j; k++) {
                    double[] Lrowk = l[k];
                    double s = 0.0;
                    for (int i = 0; i < k; i++) { s += Lrowk[i] * Lrowj[i]; }
                    Lrowj[k] = s = (a[j][k] - s) / l[k][k];
                    d = d + s * s;

                    this.symmetric = this.symmetric & (a[k][j] == a[j][k]);
                }

                d = a[j][j] - d;

                this.positiveDefinite = this.positiveDefinite & (d > 0.0);
                l[j][j] = Math.Sqrt(Math.Max(d, 0.0));
                for (int k = j + 1; k < dimension; k++) { l[j][k] = 0.0; }
            }
        }

        /// <summary>Returns <see langword="true"/> if the matrix is symmetric.</summary>
        public bool Symmetric {
            get { return this.symmetric; }
        }

        /// <summary>Returns <see langword="true"/> if the matrix is positive definite.</summary>
        public bool PositiveDefinite {
            get { return this.positiveDefinite; }
        }

        /// <summary>Returns the left triangular factor <c>L</c> so that <c>A = L * L'</c>.</summary>
        public Matrix LeftTriangularFactor {
            get { return this.L; }
        }

        /// <summary>Solves a set of equation systems of type <c>A * X = B</c>.</summary>
        /// <param name="value">Right hand side matrix with as many rows as <c>A</c> and any number of columns.</param>
        /// <returns>Matrix <c>X</c> so that <c>L * L' * X = B</c>.</returns>
        /// <exception cref="T:System.ArgumentException">Matrix dimensions do not match.</exception>
        /// <exception cref="T:System.InvalidOperationException">Matrix is not symmetrix and positive definite.</exception>
        public Matrix Solve(Matrix value) {
            if (value == null) { throw new ArgumentNullException("value"); }

            if (value.Rows != L.Rows) { throw new ArgumentException("Matrix dimensions do not match."); }

            if (!this.symmetric) { throw new InvalidOperationException("Matrix is not symmetric."); }

            if (!this.positiveDefinite) { throw new InvalidOperationException("Matrix is not positive definite."); }

            int dimension = L.Rows;
            int count = value.Columns;

            Matrix B = (Matrix) value.Clone();
            double[][] l = L.Array;

            // Solve L*Y = B;
            for (int k = 0; k < L.Rows; k++) {
                for (int i = k + 1; i < dimension; i++) {
                    for (int j = 0; j < count; j++) { B[i, j] -= B[k, j] * l[i][k]; }
                }

                for (int j = 0; j < count; j++) { B[k, j] /= l[k][k]; }
            }

            // Solve L'*X = Y;
            for (int k = dimension - 1; k >= 0; k--) {
                for (int j = 0; j < count; j++) { B[k, j] /= l[k][k]; }

                for (int i = 0; i < k; i++) {
                    for (int j = 0; j < count; j++) { B[i, j] -= B[k, j] * l[k][i]; }
                }
            }

            return B;
        }
    }

    /// <summary>
    /// Determines the eigenvalues and eigenvectors of a real square matrix.
    /// </summary>
    /// <remarks>
    /// If <c>A</c> is symmetric, then <c>A = V * D * V'</c> and <c>A = V * V'</c>
    /// where the eigenvalue matrix <c>D</c> is diagonal and the eigenvector matrix <c>V</c> is orthogonal.
    /// If <c>A</c> is not symmetric, the eigenvalue matrix <c>D</c> is block diagonal
    /// with the real eigenvalues in 1-by-1 blocks and any complex eigenvalues,
    /// <c>lambda+i*mu</c>, in 2-by-2 blocks, <c>[lambda, mu; -mu, lambda]</c>.
    /// The columns of <c>V</c> represent the eigenvectors in the sense that <c>A * V = V * D</c>.
    /// The matrix V may be badly conditioned, or even singular, so the validity of the equation
    /// <c>A=V*D*inverse(V)</c> depends upon the condition of <c>V</c>.
    /// </remarks>
    [Serializable]
    public class EigenvalueDecomposition {
        private int n; // matrix dimension
        private double[] d, e; // storage of eigenvalues.
        private Matrix V; // storage of eigenvectors.
        private Matrix H; // storage of nonsymmetric Hessenberg form.
        private double[] ort; // storage for nonsymmetric algorithm.
        private double cdivr, cdivi;
        private bool symmetric;

        /// <summary>Construct an eigenvalue decomposition.</summary>
        public EigenvalueDecomposition(Matrix value) {
            if (value == null) { throw new ArgumentNullException("value"); }

            if (value.Rows != value.Columns) { throw new ArgumentException("Matrix is not a square matrix.", "value"); }

            n = value.Columns;
            V = new Matrix(n, n);
            d = new double[n];
            e = new double[n];

            // Check for symmetry.
            this.symmetric = value.Symmetric;

            if (this.symmetric) {
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) { V[i, j] = value[i, j]; }
                }

                // Tridiagonalize.
                this.tred2();

                // Diagonalize.
                this.tql2();
            } else {
                H = new Matrix(n, n);
                ort = new double[n];

                for (int j = 0; j < n; j++) {
                    for (int i = 0; i < n; i++) { H[i, j] = value[i, j]; }
                }

                // Reduce to Hessenberg form.
                this.orthes();

                // Reduce Hessenberg to real Schur form.
                this.hqr2();
            }
        }

        private void tred2() {
            // Symmetric Householder reduction to tridiagonal form.
            // This is derived from the Algol procedures tred2 by Bowdler, Martin, Reinsch, and Wilkinson, 
            // Handbook for Auto. Comp., Vol.ii-Linear Algebra, and the corresponding Fortran subroutine in EISPACK.
            for (int j = 0; j < n; j++) d[j] = V[n - 1, j];

            // Householder reduction to tridiagonal form.
            for (int i = n - 1; i > 0; i--) {
                // Scale to avoid under/overflow.
                double scale = 0.0;
                double h = 0.0;
                for (int k = 0; k < i; k++) scale = scale + Math.Abs(d[k]);

                if (scale == 0.0) {
                    e[i] = d[i - 1];
                    for (int j = 0; j < i; j++) {
                        d[j] = V[i - 1, j];
                        V[i, j] = 0.0;
                        V[j, i] = 0.0;
                    }
                } else {
                    // Generate Householder vector.
                    for (int k = 0; k < i; k++) {
                        d[k] /= scale;
                        h += d[k] * d[k];
                    }

                    double f = d[i - 1];
                    double g = Math.Sqrt(h);
                    if (f > 0) g = -g;

                    e[i] = scale * g;
                    h = h - f * g;
                    d[i - 1] = f - g;
                    for (int j = 0; j < i; j++) e[j] = 0.0;

                    // Apply similarity transformation to remaining columns.
                    for (int j = 0; j < i; j++) {
                        f = d[j];
                        V[j, i] = f;
                        g = e[j] + V[j, j] * f;
                        for (int k = j + 1; k <= i - 1; k++) {
                            g += V[k, j] * d[k];
                            e[k] += V[k, j] * f;
                        }
                        e[j] = g;
                    }

                    f = 0.0;
                    for (int j = 0; j < i; j++) {
                        e[j] /= h;
                        f += e[j] * d[j];
                    }

                    double hh = f / (h + h);
                    for (int j = 0; j < i; j++) e[j] -= hh * d[j];

                    for (int j = 0; j < i; j++) {
                        f = d[j];
                        g = e[j];
                        for (int k = j; k <= i - 1; k++) V[k, j] -= (f * e[k] + g * d[k]);

                        d[j] = V[i - 1, j];
                        V[i, j] = 0.0;
                    }
                }
                d[i] = h;
            }

            // Accumulate transformations.
            for (int i = 0; i < n - 1; i++) {
                V[n - 1, i] = V[i, i];
                V[i, i] = 1.0;
                double h = d[i + 1];
                if (h != 0.0) {
                    for (int k = 0; k <= i; k++) d[k] = V[k, i + 1] / h;

                    for (int j = 0; j <= i; j++) {
                        double g = 0.0;
                        for (int k = 0; k <= i; k++) g += V[k, i + 1] * V[k, j];
                        for (int k = 0; k <= i; k++) V[k, j] -= g * d[k];
                    }
                }

                for (int k = 0; k <= i; k++) V[k, i + 1] = 0.0;
            }

            for (int j = 0; j < n; j++) {
                d[j] = V[n - 1, j];
                V[n - 1, j] = 0.0;
            }

            V[n - 1, n - 1] = 1.0;
            e[0] = 0.0;
        }

        private void tql2() {
            // Symmetric tridiagonal QL algorithm.
            // This is derived from the Algol procedures tql2, by Bowdler, Martin, Reinsch, and Wilkinson, 
            // Handbook for Auto. Comp., Vol.ii-Linear Algebra, and the corresponding Fortran subroutine in EISPACK.
            for (int i = 1; i < n; i++) e[i - 1] = e[i];

            e[n - 1] = 0.0;

            double f = 0.0;
            double tst1 = 0.0;
            double eps = Math.Pow(2.0, -52.0);

            for (int l = 0; l < n; l++) {
                // Find small subdiagonal element.
                tst1 = Math.Max(tst1, Math.Abs(d[l]) + Math.Abs(e[l]));
                int m = l;
                while (m < n) {
                    if (Math.Abs(e[m]) <= eps * tst1) break;
                    m++;
                }

                // If m == l, d[l] is an eigenvalue, otherwise, iterate.
                if (m > l) {
                    int iter = 0;
                    do {
                        iter = iter + 1; // (Could check iteration count here.)

                        // Compute implicit shift
                        double g = d[l];
                        double p = (d[l + 1] - g) / (2.0 * e[l]);
                        double r = Hypotenuse(p, 1.0);
                        if (p < 0) { r = -r; }

                        d[l] = e[l] / (p + r);
                        d[l + 1] = e[l] * (p + r);
                        double dl1 = d[l + 1];
                        double h = g - d[l];
                        for (int i = l + 2; i < n; i++) { d[i] -= h; }

                        f = f + h;

                        // Implicit QL transformation.
                        p = d[m];
                        double c = 1.0;
                        double c2 = c;
                        double c3 = c;
                        double el1 = e[l + 1];
                        double s = 0.0;
                        double s2 = 0.0;
                        for (int i = m - 1; i >= l; i--) {
                            c3 = c2;
                            c2 = c;
                            s2 = s;
                            g = c * e[i];
                            h = c * p;
                            r = Hypotenuse(p, e[i]);
                            e[i + 1] = s * r;
                            s = e[i] / r;
                            c = p / r;
                            p = c * d[i] - s * g;
                            d[i + 1] = h + s * (c * g + s * d[i]);

                            // Accumulate transformation.
                            for (int k = 0; k < n; k++) {
                                h = V[k, i + 1];
                                V[k, i + 1] = s * V[k, i] + c * h;
                                V[k, i] = c * V[k, i] - s * h;
                            }
                        }

                        p = -s * s2 * c3 * el1 * e[l] / dl1;
                        e[l] = s * p;
                        d[l] = c * p;

                        // Check for convergence.
                    } while (Math.Abs(e[l]) > eps * tst1);
                }
                d[l] = d[l] + f;
                e[l] = 0.0;
            }

            // Sort eigenvalues and corresponding vectors.
            for (int i = 0; i < n - 1; i++) {
                int k = i;
                double p = d[i];
                for (int j = i + 1; j < n; j++) {
                    if (d[j] < p) {
                        k = j;
                        p = d[j];
                    }
                }

                if (k != i) {
                    d[k] = d[i];
                    d[i] = p;
                    for (int j = 0; j < n; j++) {
                        p = V[j, i];
                        V[j, i] = V[j, k];
                        V[j, k] = p;
                    }
                }
            }
        }

        private void orthes() {
            // Nonsymmetric reduction to Hessenberg form.
            // This is derived from the Algol procedures orthes and ortran, by Martin and Wilkinson, 
            // Handbook for Auto. Comp., Vol.ii-Linear Algebra, and the corresponding Fortran subroutines in EISPACK.
            int low = 0;
            int high = n - 1;

            for (int m = low + 1; m <= high - 1; m++) {
                // Scale column.

                double scale = 0.0;
                for (int i = m; i <= high; i++) scale = scale + Math.Abs(H[i, m - 1]);

                if (scale != 0.0) {
                    // Compute Householder transformation.
                    double h = 0.0;
                    for (int i = high; i >= m; i--) {
                        ort[i] = H[i, m - 1] / scale;
                        h += ort[i] * ort[i];
                    }

                    double g = Math.Sqrt(h);
                    if (ort[m] > 0) g = -g;

                    h = h - ort[m] * g;
                    ort[m] = ort[m] - g;

                    // Apply Householder similarity transformation
                    // H = (I - u * u' / h) * H * (I - u * u') / h)
                    for (int j = m; j < n; j++) {
                        double f = 0.0;
                        for (int i = high; i >= m; i--) f += ort[i] * H[i, j];

                        f = f / h;
                        for (int i = m; i <= high; i++) H[i, j] -= f * ort[i];
                    }

                    for (int i = 0; i <= high; i++) {
                        double f = 0.0;
                        for (int j = high; j >= m; j--) f += ort[j] * H[i, j];

                        f = f / h;
                        for (int j = m; j <= high; j++) H[i, j] -= f * ort[j];
                    }

                    ort[m] = scale * ort[m];
                    H[m, m - 1] = scale * g;
                }
            }

            // Accumulate transformations (Algol's ortran).
            for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) V[i, j] = (i == j ? 1.0 : 0.0);

            for (int m = high - 1; m >= low + 1; m--) {
                if (H[m, m - 1] != 0.0) {
                    for (int i = m + 1; i <= high; i++) ort[i] = H[i, m - 1];

                    for (int j = m; j <= high; j++) {
                        double g = 0.0;
                        for (int i = m; i <= high; i++) g += ort[i] * V[i, j];

                        // Double division avoids possible underflow.
                        g = (g / ort[m]) / H[m, m - 1];
                        for (int i = m; i <= high; i++) V[i, j] += g * ort[i];
                    }
                }
            }
        }

        private void cdiv(double xr, double xi, double yr, double yi) {
            // Complex scalar division.
            double r;
            double d;
            if (Math.Abs(yr) > Math.Abs(yi)) {
                r = yi / yr;
                d = yr + r * yi;
                cdivr = (xr + r * xi) / d;
                cdivi = (xi - r * xr) / d;
            } else {
                r = yr / yi;
                d = yi + r * yr;
                cdivr = (r * xr + xi) / d;
                cdivi = (r * xi - xr) / d;
            }
        }

        private void hqr2() {
            // Nonsymmetric reduction from Hessenberg to real Schur form.   
            // This is derived from the Algol procedure hqr2, by Martin and Wilkinson, Handbook for Auto. Comp.,
            // Vol.ii-Linear Algebra, and the corresponding  Fortran subroutine in EISPACK.
            int nn = this.n;
            int n = nn - 1;
            int low = 0;
            int high = nn - 1;
            double eps = Math.Pow(2.0, -52.0);
            double exshift = 0.0;
            double p = 0;
            double q = 0;
            double r = 0;
            double s = 0;
            double z = 0;
            double t;
            double w;
            double x;
            double y;

            // Store roots isolated by balanc and compute matrix norm
            double norm = 0.0;
            for (int i = 0; i < nn; i++) {
                if (i < low | i > high) {
                    d[i] = H[i, i];
                    e[i] = 0.0;
                }

                for (int j = Math.Max(i - 1, 0); j < nn; j++) norm = norm + Math.Abs(H[i, j]);
            }

            // Outer loop over eigenvalue index
            int iter = 0;
            while (n >= low) {
                // Look for single small sub-diagonal element
                int l = n;
                while (l > low) {
                    s = Math.Abs(H[l - 1, l - 1]) + Math.Abs(H[l, l]);
                    if (s == 0.0) s = norm;
                    if (Math.Abs(H[l, l - 1]) < eps * s) break;

                    l--;
                }

                // Check for convergence
                if (l == n) {
                    // One root found
                    H[n, n] = H[n, n] + exshift;
                    d[n] = H[n, n];
                    e[n] = 0.0;
                    n--;
                    iter = 0;
                } else if (l == n - 1) {
                    // Two roots found
                    w = H[n, n - 1] * H[n - 1, n];
                    p = (H[n - 1, n - 1] - H[n, n]) / 2.0;
                    q = p * p + w;
                    z = Math.Sqrt(Math.Abs(q));
                    H[n, n] = H[n, n] + exshift;
                    H[n - 1, n - 1] = H[n - 1, n - 1] + exshift;
                    x = H[n, n];

                    if (q >= 0) {
                        // Real pair
                        z = (p >= 0) ? (p + z) : (p - z);
                        d[n - 1] = x + z;
                        d[n] = d[n - 1];
                        if (z != 0.0) d[n] = x - w / z;
                        e[n - 1] = 0.0;
                        e[n] = 0.0;
                        x = H[n, n - 1];
                        s = Math.Abs(x) + Math.Abs(z);
                        p = x / s;
                        q = z / s;
                        r = Math.Sqrt(p * p + q * q);
                        p = p / r;
                        q = q / r;

                        // Row modification
                        for (int j = n - 1; j < nn; j++) {
                            z = H[n - 1, j];
                            H[n - 1, j] = q * z + p * H[n, j];
                            H[n, j] = q * H[n, j] - p * z;
                        }

                        // Column modification
                        for (int i = 0; i <= n; i++) {
                            z = H[i, n - 1];
                            H[i, n - 1] = q * z + p * H[i, n];
                            H[i, n] = q * H[i, n] - p * z;
                        }

                        // Accumulate transformations
                        for (int i = low; i <= high; i++) {
                            z = V[i, n - 1];
                            V[i, n - 1] = q * z + p * V[i, n];
                            V[i, n] = q * V[i, n] - p * z;
                        }
                    } else {
                        // Complex pair
                        d[n - 1] = x + p;
                        d[n] = x + p;
                        e[n - 1] = z;
                        e[n] = -z;
                    }

                    n = n - 2;
                    iter = 0;
                } else {
                    // No convergence yet	 

                    // Form shift
                    x = H[n, n];
                    y = 0.0;
                    w = 0.0;
                    if (l < n) {
                        y = H[n - 1, n - 1];
                        w = H[n, n - 1] * H[n - 1, n];
                    }

                    // Wilkinson's original ad hoc shift
                    if (iter == 10) {
                        exshift += x;
                        for (int i = low; i <= n; i++) H[i, i] -= x;

                        s = Math.Abs(H[n, n - 1]) + Math.Abs(H[n - 1, n - 2]);
                        x = y = 0.75 * s;
                        w = -0.4375 * s * s;
                    }

                    // MATLAB's new ad hoc shift
                    if (iter == 30) {
                        s = (y - x) / 2.0;
                        s = s * s + w;
                        if (s > 0) {
                            s = Math.Sqrt(s);
                            if (y < x) s = -s;
                            s = x - w / ((y - x) / 2.0 + s);
                            for (int i = low; i <= n; i++) H[i, i] -= s;
                            exshift += s;
                            x = y = w = 0.964;
                        }
                    }

                    iter = iter + 1;

                    // Look for two consecutive small sub-diagonal elements
                    int m = n - 2;
                    while (m >= l) {
                        z = H[m, m];
                        r = x - z;
                        s = y - z;
                        p = (r * s - w) / H[m + 1, m] + H[m, m + 1];
                        q = H[m + 1, m + 1] - z - r - s;
                        r = H[m + 2, m + 1];
                        s = Math.Abs(p) + Math.Abs(q) + Math.Abs(r);
                        p = p / s;
                        q = q / s;
                        r = r / s;
                        if (m == l) break;
                        if (Math.Abs(H[m, m - 1]) * (Math.Abs(q) + Math.Abs(r)) <
                            eps * (Math.Abs(p) * (Math.Abs(H[m - 1, m - 1]) + Math.Abs(z) + Math.Abs(H[m + 1, m + 1])))) break;
                        m--;
                    }

                    for (int i = m + 2; i <= n; i++) {
                        H[i, i - 2] = 0.0;
                        if (i > m + 2) H[i, i - 3] = 0.0;
                    }

                    // Double QR step involving rows l:n and columns m:n
                    for (int k = m; k <= n - 1; k++) {
                        bool notlast = (k != n - 1);
                        if (k != m) {
                            p = H[k, k - 1];
                            q = H[k + 1, k - 1];
                            r = (notlast ? H[k + 2, k - 1] : 0.0);
                            x = Math.Abs(p) + Math.Abs(q) + Math.Abs(r);
                            if (x != 0.0) {
                                p = p / x;
                                q = q / x;
                                r = r / x;
                            }
                        }

                        if (x == 0.0) break;

                        s = Math.Sqrt(p * p + q * q + r * r);
                        if (p < 0) s = -s;

                        if (s != 0) {
                            if (k != m) H[k, k - 1] = -s * x;
                            else if (l != m) H[k, k - 1] = -H[k, k - 1];

                            p = p + s;
                            x = p / s;
                            y = q / s;
                            z = r / s;
                            q = q / p;
                            r = r / p;

                            // Row modification
                            for (int j = k; j < nn; j++) {
                                p = H[k, j] + q * H[k + 1, j];
                                if (notlast) {
                                    p = p + r * H[k + 2, j];
                                    H[k + 2, j] = H[k + 2, j] - p * z;
                                }

                                H[k, j] = H[k, j] - p * x;
                                H[k + 1, j] = H[k + 1, j] - p * y;
                            }

                            // Column modification
                            for (int i = 0; i <= Math.Min(n, k + 3); i++) {
                                p = x * H[i, k] + y * H[i, k + 1];
                                if (notlast) {
                                    p = p + z * H[i, k + 2];
                                    H[i, k + 2] = H[i, k + 2] - p * r;
                                }

                                H[i, k] = H[i, k] - p;
                                H[i, k + 1] = H[i, k + 1] - p * q;
                            }

                            // Accumulate transformations
                            for (int i = low; i <= high; i++) {
                                p = x * V[i, k] + y * V[i, k + 1];
                                if (notlast) {
                                    p = p + z * V[i, k + 2];
                                    V[i, k + 2] = V[i, k + 2] - p * r;
                                }

                                V[i, k] = V[i, k] - p;
                                V[i, k + 1] = V[i, k + 1] - p * q;
                            }
                        }
                    }
                }
            }

            // Backsubstitute to find vectors of upper triangular form
            if (norm == 0.0) { return; }

            for (n = nn - 1; n >= 0; n--) {
                p = d[n];
                q = e[n];

                // Real vector
                if (q == 0) {
                    int l = n;
                    H[n, n] = 1.0;
                    for (int i = n - 1; i >= 0; i--) {
                        w = H[i, i] - p;
                        r = 0.0;
                        for (int j = l; j <= n; j++) r = r + H[i, j] * H[j, n];

                        if (e[i] < 0.0) {
                            z = w;
                            s = r;
                        } else {
                            l = i;
                            if (e[i] == 0.0) {
                                H[i, n] = (w != 0.0) ? (-r / w) : (-r / (eps * norm));
                            } else {
                                // Solve real equations
                                x = H[i, i + 1];
                                y = H[i + 1, i];
                                q = (d[i] - p) * (d[i] - p) + e[i] * e[i];
                                t = (x * s - z * r) / q;
                                H[i, n] = t;
                                H[i + 1, n] = (Math.Abs(x) > Math.Abs(z)) ? ((-r - w * t) / x) : ((-s - y * t) / z);
                            }

                            // Overflow control
                            t = Math.Abs(H[i, n]);
                            if ((eps * t) * t > 1) for (int j = i; j <= n; j++) H[j, n] = H[j, n] / t;
                        }
                    }
                } else if (q < 0) {
                    // Complex vector
                    int l = n - 1;

                    // Last vector component imaginary so matrix is triangular
                    if (Math.Abs(H[n, n - 1]) > Math.Abs(H[n - 1, n])) {
                        H[n - 1, n - 1] = q / H[n, n - 1];
                        H[n - 1, n] = -(H[n, n] - p) / H[n, n - 1];
                    } else {
                        cdiv(0.0, -H[n - 1, n], H[n - 1, n - 1] - p, q);
                        H[n - 1, n - 1] = cdivr;
                        H[n - 1, n] = cdivi;
                    }

                    H[n, n - 1] = 0.0;
                    H[n, n] = 1.0;
                    for (int i = n - 2; i >= 0; i--) {
                        double ra, sa, vr, vi;
                        ra = 0.0;
                        sa = 0.0;
                        for (int j = l; j <= n; j++) {
                            ra = ra + H[i, j] * H[j, n - 1];
                            sa = sa + H[i, j] * H[j, n];
                        }

                        w = H[i, i] - p;

                        if (e[i] < 0.0) {
                            z = w;
                            r = ra;
                            s = sa;
                        } else {
                            l = i;
                            if (e[i] == 0) {
                                cdiv(-ra, -sa, w, q);
                                H[i, n - 1] = cdivr;
                                H[i, n] = cdivi;
                            } else {
                                // Solve complex equations
                                x = H[i, i + 1];
                                y = H[i + 1, i];
                                vr = (d[i] - p) * (d[i] - p) + e[i] * e[i] - q * q;
                                vi = (d[i] - p) * 2.0 * q;
                                if (vr == 0.0 & vi == 0.0) vr = eps * norm * (Math.Abs(w) + Math.Abs(q) + Math.Abs(x) + Math.Abs(y) + Math.Abs(z));
                                cdiv(x * r - z * ra + q * sa, x * s - z * sa - q * ra, vr, vi);
                                H[i, n - 1] = cdivr;
                                H[i, n] = cdivi;
                                if (Math.Abs(x) > (Math.Abs(z) + Math.Abs(q))) {
                                    H[i + 1, n - 1] = (-ra - w * H[i, n - 1] + q * H[i, n]) / x;
                                    H[i + 1, n] = (-sa - w * H[i, n] - q * H[i, n - 1]) / x;
                                } else {
                                    cdiv(-r - y * H[i, n - 1], -s - y * H[i, n], z, q);
                                    H[i + 1, n - 1] = cdivr;
                                    H[i + 1, n] = cdivi;
                                }
                            }

                            // Overflow control
                            t = Math.Max(Math.Abs(H[i, n - 1]), Math.Abs(H[i, n]));
                            if ((eps * t) * t > 1)
                                for (int j = i; j <= n; j++) {
                                    H[j, n - 1] = H[j, n - 1] / t;
                                    H[j, n] = H[j, n] / t;
                                }
                        }
                    }
                }
            }

            // Vectors of isolated roots
            for (int i = 0; i < nn; i++) if (i < low | i > high) for (int j = i; j < nn; j++) V[i, j] = H[i, j];

            // Back transformation to get eigenvectors of original matrix
            for (int j = nn - 1; j >= low; j--)
            for (int i = low; i <= high; i++) {
                z = 0.0;
                for (int k = low; k <= Math.Min(j, high); k++) z = z + V[i, k] * H[k, j];
                V[i, j] = z;
            }
        }

        /// <summary>Returns the real parts of the eigenvalues.</summary>
        public double[] RealEigenvalues {
            get { return this.d; }
        }

        /// <summary>Returns the imaginary parts of the eigenvalues.</summary>	
        public double[] ImaginaryEigenvalues {
            get { return this.e; }
        }

        /// <summary>Returns the eigenvector matrix.</summary>
        public Matrix EigenvectorMatrix {
            get { return this.V; }
        }

        /// <summary>Returns the block diagonal eigenvalue matrix.</summary>
        public Matrix DiagonalMatrix {
            get {
                Matrix X = new Matrix(n, n);
                double[][] x = X.Array;

                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) x[i][j] = 0.0;

                    x[i][i] = d[i];
                    if (e[i] > 0) { x[i][i + 1] = e[i]; } else if (e[i] < 0) { x[i][i - 1] = e[i]; }
                }

                return X;
            }
        }

        private static double Hypotenuse(double a, double b) {
            if (Math.Abs(a) > Math.Abs(b)) {
                double r = b / a;
                return Math.Abs(a) * Math.Sqrt(1 + r * r);
            }

            if (b != 0) {
                double r = a / b;
                return Math.Abs(b) * Math.Sqrt(1 + r * r);
            }

            return 0.0;
        }
    }

    /// <summary>
    ///   LU decomposition of a rectangular matrix.
    /// </summary>
    /// <remarks>
    ///   For an m-by-n matrix <c>A</c> with m >= n, the LU decomposition is an m-by-n
    ///   unit lower triangular matrix <c>L</c>, an n-by-n upper triangular matrix <c>U</c>,
    ///   and a permutation vector <c>piv</c> of length m so that <c>A(piv)=L*U</c>.
    ///   If m &lt; n, then <c>L</c> is m-by-m and <c>U</c> is m-by-n.
    ///   The LU decompostion with pivoting always exists, even if the matrix is
    ///   singular, so the constructor will never fail.  The primary use of the
    ///   LU decomposition is in the solution of square systems of simultaneous
    ///   linear equations. This will fail if <see cref="NonSingular"/> returns <see langword="false"/>.
    /// </remarks>
    [Serializable]
    public class LuDecomposition {
        private Matrix LU;
        private int pivotSign;
        private int[] pivotVector;

        /// <summary>Construct a LU decomposition.</summary>	
        public LuDecomposition(Matrix value) {
            if (value == null) { throw new ArgumentNullException("value"); }

            this.LU = (Matrix) value.Clone();
            double[][] lu = LU.Array;
            int rows = value.Rows;
            int columns = value.Columns;
            pivotVector = new int[rows];
            for (int i = 0; i < rows; i++) { pivotVector[i] = i; }

            pivotSign = 1;
            double[] LUrowi;
            double[] LUcolj = new double[rows];

            // Outer loop.
            for (int j = 0; j < columns; j++) {
                // Make a copy of the j-th column to localize references.
                for (int i = 0; i < rows; i++) { LUcolj[i] = lu[i][j]; }

                // Apply previous transformations.
                for (int i = 0; i < rows; i++) {
                    LUrowi = lu[i];

                    // Most of the time is spent in the following dot product.
                    int kmax = Math.Min(i, j);
                    double s = 0.0;
                    for (int k = 0; k < kmax; k++) { s += LUrowi[k] * LUcolj[k]; }
                    LUrowi[j] = LUcolj[i] -= s;
                }

                // Find pivot and exchange if necessary.
                int p = j;
                for (int i = j + 1; i < rows; i++) {
                    if (Math.Abs(LUcolj[i]) > Math.Abs(LUcolj[p])) { p = i; }
                }

                if (p != j) {
                    for (int k = 0; k < columns; k++) {
                        double t = lu[p][k];
                        lu[p][k] = lu[j][k];
                        lu[j][k] = t;
                    }

                    int v = pivotVector[p];
                    pivotVector[p] = pivotVector[j];
                    pivotVector[j] = v;

                    pivotSign = -pivotSign;
                }

                // Compute multipliers.

                if (j < rows & lu[j][j] != 0.0) {
                    for (int i = j + 1; i < rows; i++) { lu[i][j] /= lu[j][j]; }
                }
            }
        }

        /// <summary>Returns if the matrix is non-singular.</summary>
        public bool NonSingular {
            get {
                for (int j = 0; j < LU.Columns; j++) if (LU[j, j] == 0) return false;
                return true;
            }
        }

        /// <summary>Returns the determinant of the matrix.</summary>
        public double Determinant {
            get {
                if (LU.Rows != LU.Columns) throw new ArgumentException("Matrix must be square.");
                double determinant = (double) pivotSign;
                for (int j = 0; j < LU.Columns; j++) determinant *= LU[j, j];
                return determinant;
            }
        }

        /// <summary>Returns the lower triangular factor <c>L</c> with <c>A=LU</c>.</summary>
        public Matrix LowerTriangularFactor {
            get {
                int rows = LU.Rows;
                int columns = LU.Columns;
                Matrix X = new Matrix(rows, columns);
                for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    if (i > j) X[i, j] = LU[i, j];
                    else if (i == j) X[i, j] = 1.0;
                    else X[i, j] = 0.0;
                return X;
            }
        }

        /// <summary>Returns the lower triangular factor <c>L</c> with <c>A=LU</c>.</summary>
        public Matrix UpperTriangularFactor {
            get {
                int rows = LU.Rows;
                int columns = LU.Columns;
                Matrix X = new Matrix(rows, columns);
                for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    if (i <= j) X[i, j] = LU[i, j];
                    else X[i, j] = 0.0;
                return X;
            }
        }

        /// <summary>Returns the pivot permuation vector.</summary>
        public double[] PivotPermutationVector {
            get {
                int rows = LU.Rows;

                double[] p = new double[rows];
                for (int i = 0; i < rows; i++) { p[i] = (double) this.pivotVector[i]; }

                return p;
            }
        }

        /// <summary>Solves a set of equation systems of type <c>A * X = B</c>.</summary>
        /// <param name="value">Right hand side matrix with as many rows as <c>A</c> and any number of columns.</param>
        /// <returns>Matrix <c>X</c> so that <c>L * U * X = B</c>.</returns>
        public Matrix Solve(Matrix value) {
            if (value == null) { throw new ArgumentNullException("value"); }

            if (value.Rows != this.LU.Rows) { throw new ArgumentException("Invalid matrix dimensions.", "value"); }

            if (!this.NonSingular) { throw new InvalidOperationException("Matrix is singular"); }

            // Copy right hand side with pivoting
            int count = value.Columns;
            Matrix X = value.Submatrix(pivotVector, 0, count - 1);

            int rows = LU.Rows;
            int columns = LU.Columns;
            double[][] lu = LU.Array;

            // Solve L*Y = B(piv,:)
            for (int k = 0; k < columns; k++) {
                for (int i = k + 1; i < columns; i++) {
                    for (int j = 0; j < count; j++) { X[i, j] -= X[k, j] * lu[i][k]; }
                }
            }

            // Solve U*X = Y;
            for (int k = columns - 1; k >= 0; k--) {
                for (int j = 0; j < count; j++) { X[k, j] /= lu[k][k]; }

                for (int i = 0; i < k; i++) {
                    for (int j = 0; j < count; j++) { X[i, j] -= X[k, j] * lu[i][k]; }
                }
            }

            return X;
        }
    }

    /// <summary>
    ///	  QR decomposition for a rectangular matrix.
    /// </summary>
    /// <remarks>
    ///   For an m-by-n matrix <c>A</c> with <c>m &gt;= n</c>, the QR decomposition is an m-by-n
    ///   orthogonal matrix <c>Q</c> and an n-by-n upper triangular 
    ///   matrix <c>R</c> so that <c>A = Q * R</c>.
    ///   The QR decompostion always exists, even if the matrix does not have
    ///   full rank, so the constructor will never fail.  The primary use of the
    ///   QR decomposition is in the least squares solution of nonsquare systems
    ///   of simultaneous linear equations.
    ///   This will fail if <see cref="FullRank"/> returns <see langword="false"/>.
    /// </remarks>
    [Serializable]
    public class QrDecomposition {
        private Matrix QR;
        private double[] Rdiag;

        /// <summary>Construct a QR decomposition.</summary>	
        public QrDecomposition(Matrix value) {
            if (value == null) { throw new ArgumentNullException("value"); }

            this.QR = (Matrix) value.Clone();
            double[][] qr = this.QR.Array;
            int m = value.Rows;
            int n = value.Columns;
            this.Rdiag = new double[n];

            for (int k = 0; k < n; k++) {
                // Compute 2-norm of k-th column without under/overflow.
                double nrm = 0;
                for (int i = k; i < m; i++) { nrm = Hypotenuse(nrm, qr[i][k]); }

                if (nrm != 0.0) {
                    // Form k-th Householder vector.
                    if (qr[k][k] < 0) { nrm = -nrm; }

                    for (int i = k; i < m; i++) { qr[i][k] /= nrm; }

                    qr[k][k] += 1.0;

                    // Apply transformation to remaining columns.
                    for (int j = k + 1; j < n; j++) {
                        double s = 0.0;

                        for (int i = k; i < m; i++) { s += qr[i][k] * qr[i][j]; }

                        s = -s / qr[k][k];

                        for (int i = k; i < m; i++) { qr[i][j] += s * qr[i][k]; }
                    }
                }

                this.Rdiag[k] = -nrm;
            }
        }

        /// <summary>Least squares solution of <c>A * X = B</c></summary>
        /// <param name="value">Right-hand-side matrix with as many rows as <c>A</c> and any number of columns.</param>
        /// <returns>A matrix that minimized the two norm of <c>Q * R * X - B</c>.</returns>
        /// <exception cref="T:System.ArgumentException">Matrix row dimensions must be the same.</exception>
        /// <exception cref="T:System.InvalidOperationException">Matrix is rank deficient.</exception>
        public Matrix Solve(Matrix value) {
            if (value == null) { throw new ArgumentNullException("value"); }

            if (value.Rows != QR.Rows) { throw new ArgumentException("Matrix row dimensions must agree."); }

            if (!this.FullRank) { throw new InvalidOperationException("Matrix is rank deficient."); }

            // Copy right hand side
            int count = value.Columns;
            Matrix X = value.Clone();
            int m = QR.Rows;
            int n = QR.Columns;
            double[][] qr = QR.Array;

            // Compute Y = transpose(Q)*B
            for (int k = 0; k < n; k++) {
                for (int j = 0; j < count; j++) {
                    double s = 0.0;

                    for (int i = k; i < m; i++) { s += qr[i][k] * X[i, j]; }

                    s = -s / qr[k][k];

                    for (int i = k; i < m; i++) { X[i, j] += s * qr[i][k]; }
                }
            }

            // Solve R*X = Y;
            for (int k = n - 1; k >= 0; k--) {
                for (int j = 0; j < count; j++) { X[k, j] /= Rdiag[k]; }

                for (int i = 0; i < k; i++) {
                    for (int j = 0; j < count; j++) { X[i, j] -= X[k, j] * qr[i][k]; }
                }
            }

            return X.Submatrix(0, n - 1, 0, count - 1);
        }

        /// <summary>Shows if the matrix <c>A</c> is of full rank.</summary>
        /// <value>The value is <see langword="true"/> if <c>R</c>, and hence <c>A</c>, has full rank.</value>
        public bool FullRank {
            get {
                int columns = this.QR.Columns;
                for (int i = 0; i < columns; i++) {
                    if (this.Rdiag[i] == 0) { return false; }
                }

                return true;
            }
        }

        /// <summary>Returns the upper triangular factor <c>R</c>.</summary>
        public Matrix UpperTriangularFactor {
            get {
                int n = this.QR.Columns;
                Matrix X = new Matrix(n, n);
                double[][] x = X.Array;
                double[][] qr = QR.Array;
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        if (i < j) { x[i][j] = qr[i][j]; } else if (i == j) { x[i][j] = Rdiag[i]; } else { x[i][j] = 0.0; }
                    }
                }

                return X;
            }
        }

        /// <summary>Returns the orthogonal factor <c>Q</c>.</summary>
        public Matrix OrthogonalFactor {
            get {
                Matrix X = new Matrix(QR.Rows, QR.Columns);
                double[][] x = X.Array;
                double[][] qr = QR.Array;
                for (int k = QR.Columns - 1; k >= 0; k--) {
                    for (int i = 0; i < QR.Rows; i++) { x[i][k] = 0.0; }

                    x[k][k] = 1.0;
                    for (int j = k; j < QR.Columns; j++) {
                        if (qr[k][k] != 0) {
                            double s = 0.0;

                            for (int i = k; i < QR.Rows; i++) { s += qr[i][k] * x[i][j]; }

                            s = -s / qr[k][k];

                            for (int i = k; i < QR.Rows; i++) { x[i][j] += s * qr[i][k]; }
                        }
                    }
                }

                return X;
            }
        }

        private static double Hypotenuse(double a, double b) {
            if (Math.Abs(a) > Math.Abs(b)) {
                double r = b / a;
                return Math.Abs(a) * Math.Sqrt(1 + r * r);
            }

            if (b != 0) {
                double r = a / b;
                return Math.Abs(b) * Math.Sqrt(1 + r * r);
            }

            return 0.0;
        }
    }

    /// <summary>
    /// 	Singular Value Decomposition for a rectangular matrix.
    /// </summary>
    /// <remarks>
    ///	  For an m-by-n matrix <c>A</c> with <c>m >= n</c>, the singular value decomposition is
    ///   an m-by-n orthogonal matrix <c>U</c>, an n-by-n diagonal matrix <c>S</c>, and
    ///   an n-by-n orthogonal matrix <c>V</c> so that <c>A = U * S * V'</c>.
    ///   The singular values, <c>sigma[k] = S[k,k]</c>, are ordered so that
    ///   <c>sigma[0] >= sigma[1] >= ... >= sigma[n-1]</c>.
    ///   The singular value decompostion always exists, so the constructor will
    ///   never fail. The matrix condition number and the effective numerical
    ///   rank can be computed from this decomposition.
    /// </remarks>
    [Serializable]
    public class SingularValueDecomposition {
        private Matrix U;
        private Matrix V;
        private double[] s; // singular values
        private int m;
        private int n;

        /// <summary>Construct singular value decomposition.</summary>
        public SingularValueDecomposition(Matrix value) {
            if (value == null) { throw new ArgumentNullException("value"); }

            Matrix copy = (Matrix) value.Clone();
            double[][] a = copy.Array;
            m = value.Rows;
            n = value.Columns;
            int nu = Math.Min(m, n);
            s = new double[Math.Min(m + 1, n)];
            U = new Matrix(m, nu);
            V = new Matrix(n, n);
            double[][] u = U.Array;
            double[][] v = V.Array;
            double[] e = new double[n];
            double[] work = new double[m];
            bool wantu = true;
            bool wantv = true;

            // Reduce A to bidiagonal form, storing the diagonal elements in s and the super-diagonal elements in e.
            int nct = Math.Min(m - 1, n);
            int nrt = Math.Max(0, Math.Min(n - 2, m));
            for (int k = 0; k < Math.Max(nct, nrt); k++) {
                if (k < nct) {
                    // Compute the transformation for the k-th column and place the k-th diagonal in s[k].
                    // Compute 2-norm of k-th column without under/overflow.
                    s[k] = 0;
                    for (int i = k; i < m; i++) { s[k] = Hypotenuse(s[k], a[i][k]); }

                    if (s[k] != 0.0) {
                        if (a[k][k] < 0.0) { s[k] = -s[k]; }

                        for (int i = k; i < m; i++) { a[i][k] /= s[k]; }

                        a[k][k] += 1.0;
                    }

                    s[k] = -s[k];
                }

                for (int j = k + 1; j < n; j++) {
                    if ((k < nct) & (s[k] != 0.0)) {
                        // Apply the transformation.
                        double t = 0;
                        for (int i = k; i < m; i++) t += a[i][k] * a[i][j];
                        t = -t / a[k][k];
                        for (int i = k; i < m; i++) a[i][j] += t * a[i][k];
                    }

                    // Place the k-th row of A into e for the subsequent calculation of the row transformation.
                    e[j] = a[k][j];
                }

                if (wantu & (k < nct)) {
                    // Place the transformation in U for subsequent back
                    // multiplication.
                    for (int i = k; i < m; i++) u[i][k] = a[i][k];
                }

                if (k < nrt) {
                    // Compute the k-th row transformation and place the k-th super-diagonal in e[k].
                    // Compute 2-norm without under/overflow.
                    e[k] = 0;
                    for (int i = k + 1; i < n; i++) { e[k] = Hypotenuse(e[k], e[i]); }

                    if (e[k] != 0.0) {
                        if (e[k + 1] < 0.0) e[k] = -e[k];

                        for (int i = k + 1; i < n; i++) e[i] /= e[k];

                        e[k + 1] += 1.0;
                    }

                    e[k] = -e[k];
                    if ((k + 1 < m) & (e[k] != 0.0)) {
                        // Apply the transformation.
                        for (int i = k + 1; i < m; i++) work[i] = 0.0;

                        for (int j = k + 1; j < n; j++) for (int i = k + 1; i < m; i++) work[i] += e[j] * a[i][j];

                        for (int j = k + 1; j < n; j++) {
                            double t = -e[j] / e[k + 1];
                            for (int i = k + 1; i < m; i++) a[i][j] += t * work[i];
                        }
                    }

                    if (wantv) {
                        // Place the transformation in V for subsequent back multiplication.
                        for (int i = k + 1; i < n; i++) v[i][k] = e[i];
                    }
                }
            }

            // Set up the final bidiagonal matrix or order p.
            int p = Math.Min(n, m + 1);
            if (nct < n) s[nct] = a[nct][nct];
            if (m < p) s[p - 1] = 0.0;
            if (nrt + 1 < p) e[nrt] = a[nrt][p - 1];
            e[p - 1] = 0.0;

            // If required, generate U.
            if (wantu) {
                for (int j = nct; j < nu; j++) {
                    for (int i = 0; i < m; i++) u[i][j] = 0.0;
                    u[j][j] = 1.0;
                }

                for (int k = nct - 1; k >= 0; k--) {
                    if (s[k] != 0.0) {
                        for (int j = k + 1; j < nu; j++) {
                            double t = 0;
                            for (int i = k; i < m; i++) t += u[i][k] * u[i][j];

                            t = -t / u[k][k];
                            for (int i = k; i < m; i++) u[i][j] += t * u[i][k];
                        }

                        for (int i = k; i < m; i++) u[i][k] = -u[i][k];

                        u[k][k] = 1.0 + u[k][k];
                        for (int i = 0; i < k - 1; i++) u[i][k] = 0.0;
                    } else {
                        for (int i = 0; i < m; i++) u[i][k] = 0.0;
                        u[k][k] = 1.0;
                    }
                }
            }

            // If required, generate V.
            if (wantv) {
                for (int k = n - 1; k >= 0; k--) {
                    if ((k < nrt) & (e[k] != 0.0)) {
                        for (int j = k + 1; j < nu; j++) {
                            double t = 0;
                            for (int i = k + 1; i < n; i++) t += v[i][k] * v[i][j];

                            t = -t / v[k + 1][k];
                            for (int i = k + 1; i < n; i++) v[i][j] += t * v[i][k];
                        }
                    }

                    for (int i = 0; i < n; i++) v[i][k] = 0.0;
                    v[k][k] = 1.0;
                }
            }

            // Main iteration loop for the singular values.
            int pp = p - 1;
            int iter = 0;
            double eps = Math.Pow(2.0, -52.0);
            while (p > 0) {
                int k, kase;

                // Here is where a test for too many iterations would go.
                // This section of the program inspects for
                // negligible elements in the s and e arrays.  On
                // completion the variables kase and k are set as follows.
                // kase = 1     if s(p) and e[k-1] are negligible and k<p
                // kase = 2     if s(k) is negligible and k<p
                // kase = 3     if e[k-1] is negligible, k<p, and s(k), ..., s(p) are not negligible (qr step).
                // kase = 4     if e(p-1) is negligible (convergence).
                for (k = p - 2; k >= -1; k--) {
                    if (k == -1) break;

                    if (Math.Abs(e[k]) <= eps * (Math.Abs(s[k]) + Math.Abs(s[k + 1]))) {
                        e[k] = 0.0;
                        break;
                    }
                }

                if (k == p - 2) {
                    kase = 4;
                } else {
                    int ks;
                    for (ks = p - 1; ks >= k; ks--) {
                        if (ks == k) break;

                        double t = (ks != p ? Math.Abs(e[ks]) : 0.0) + (ks != k + 1 ? Math.Abs(e[ks - 1]) : 0.0);
                        if (Math.Abs(s[ks]) <= eps * t) {
                            s[ks] = 0.0;
                            break;
                        }
                    }

                    if (ks == k) kase = 3;
                    else if (ks == p - 1) kase = 1;
                    else {
                        kase = 2;
                        k = ks;
                    }
                }

                k++;

                // Perform the task indicated by kase.
                switch (kase) {
                    // Deflate negligible s(p).
                    case 1: {
                        double f = e[p - 2];
                        e[p - 2] = 0.0;
                        for (int j = p - 2; j >= k; j--) {
                            double t = Hypotenuse(s[j], f);
                            double cs = s[j] / t;
                            double sn = f / t;
                            s[j] = t;
                            if (j != k) {
                                f = -sn * e[j - 1];
                                e[j - 1] = cs * e[j - 1];
                            }

                            if (wantv) {
                                for (int i = 0; i < n; i++) {
                                    t = cs * v[i][j] + sn * v[i][p - 1];
                                    v[i][p - 1] = -sn * v[i][j] + cs * v[i][p - 1];
                                    v[i][j] = t;
                                }
                            }
                        }
                    }
                        break;

                    // Split at negligible s(k).
                    case 2: {
                        double f = e[k - 1];
                        e[k - 1] = 0.0;
                        for (int j = k; j < p; j++) {
                            double t = Hypotenuse(s[j], f);
                            double cs = s[j] / t;
                            double sn = f / t;
                            s[j] = t;
                            f = -sn * e[j];
                            e[j] = cs * e[j];
                            if (wantu) {
                                for (int i = 0; i < m; i++) {
                                    t = cs * u[i][j] + sn * u[i][k - 1];
                                    u[i][k - 1] = -sn * u[i][j] + cs * u[i][k - 1];
                                    u[i][j] = t;
                                }
                            }
                        }
                    }
                        break;

                    // Perform one qr step.
                    case 3: {
                        // Calculate the shift.
                        double scale = Math.Max(Math.Max(Math.Max(Math.Max(Math.Abs(s[p - 1]), Math.Abs(s[p - 2])), Math.Abs(e[p - 2])), Math.Abs(s[k])),
                            Math.Abs(e[k]));
                        double sp = s[p - 1] / scale;
                        double spm1 = s[p - 2] / scale;
                        double epm1 = e[p - 2] / scale;
                        double sk = s[k] / scale;
                        double ek = e[k] / scale;
                        double b = ((spm1 + sp) * (spm1 - sp) + epm1 * epm1) / 2.0;
                        double c = (sp * epm1) * (sp * epm1);
                        double shift = 0.0;
                        if ((b != 0.0) | (c != 0.0)) {
                            shift = Math.Sqrt(b * b + c);
                            if (b < 0.0) shift = -shift;
                            shift = c / (b + shift);
                        }

                        double f = (sk + sp) * (sk - sp) + shift;
                        double g = sk * ek;

                        // Chase zeros.
                        for (int j = k; j < p - 1; j++) {
                            double t = Hypotenuse(f, g);
                            double cs = f / t;
                            double sn = g / t;
                            if (j != k) e[j - 1] = t;
                            f = cs * s[j] + sn * e[j];
                            e[j] = cs * e[j] - sn * s[j];
                            g = sn * s[j + 1];
                            s[j + 1] = cs * s[j + 1];
                            if (wantv) {
                                for (int i = 0; i < n; i++) {
                                    t = cs * v[i][j] + sn * v[i][j + 1];
                                    v[i][j + 1] = -sn * v[i][j] + cs * v[i][j + 1];
                                    v[i][j] = t;
                                }
                            }

                            t = Hypotenuse(f, g);
                            cs = f / t;
                            sn = g / t;
                            s[j] = t;
                            f = cs * e[j] + sn * s[j + 1];
                            s[j + 1] = -sn * e[j] + cs * s[j + 1];
                            g = sn * e[j + 1];
                            e[j + 1] = cs * e[j + 1];
                            if (wantu && (j < m - 1)) {
                                for (int i = 0; i < m; i++) {
                                    t = cs * u[i][j] + sn * u[i][j + 1];
                                    u[i][j + 1] = -sn * u[i][j] + cs * u[i][j + 1];
                                    u[i][j] = t;
                                }
                            }
                        }

                        e[p - 2] = f;
                        iter = iter + 1;
                    }
                        break;

                    // Convergence.
                    case 4: {
                        // Make the singular values positive.
                        if (s[k] <= 0.0) {
                            s[k] = (s[k] < 0.0 ? -s[k] : 0.0);
                            if (wantv) for (int i = 0; i <= pp; i++) v[i][k] = -v[i][k];
                        }

                        // Order the singular values.
                        while (k < pp) {
                            if (s[k] >= s[k + 1]) break;

                            double t = s[k];
                            s[k] = s[k + 1];
                            s[k + 1] = t;
                            if (wantv && (k < n - 1))
                                for (int i = 0; i < n; i++) {
                                    t = v[i][k + 1];
                                    v[i][k + 1] = v[i][k];
                                    v[i][k] = t;
                                }

                            if (wantu && (k < m - 1))
                                for (int i = 0; i < m; i++) {
                                    t = u[i][k + 1];
                                    u[i][k + 1] = u[i][k];
                                    u[i][k] = t;
                                }

                            k++;
                        }

                        iter = 0;
                        p--;
                    }
                        break;
                }
            }
        }

        /// <summary>Returns the condition number <c>max(S) / min(S)</c>.</summary>
        public double Condition {
            get { return s[0] / s[Math.Min(m, n) - 1]; }
        }

        /// <summary>Returns the Two norm.</summary>
        public double Norm2 {
            get { return s[0]; }
        }

        /// <summary>Returns the effective numerical matrix rank.</summary>
        /// <value>Number of non-negligible singular values.</value>
        public int Rank {
            get {
                double eps = Math.Pow(2.0, -52.0);
                double tol = Math.Max(m, n) * s[0] * eps;
                int r = 0;
                for (int i = 0; i < s.Length; i++) {
                    if (s[i] > tol) { r++; }
                }

                return r;
            }
        }

        /// <summary>Return the one-dimensional array of singular values.</summary>		
        public double[] Diagonal {
            get { return this.s; }
        }

        private static double Hypotenuse(double a, double b) {
            if (Math.Abs(a) > Math.Abs(b)) {
                double r = b / a;
                return Math.Abs(a) * Math.Sqrt(1 + r * r);
            }

            if (b != 0) {
                double r = a / b;
                return Math.Abs(b) * Math.Sqrt(1 + r * r);
            }

            return 0.0;
        }
    }

}