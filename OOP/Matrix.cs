namespace OOP;

interface IMatrix
{
    double this[int i, int j] { get; set; }
    Matrix Transposed();

}

public class Matrix : IMatrix
{
    private readonly double[,] _matrix;
    public int RowCount {  get; init; }
    public int ColumnCount {  get; init; }
    public Matrix(int N, int M)
    {
        _matrix = new double[N, M];
    }
    public double this[int i, int j]
    {
        get => _matrix[i,j];
        set => _matrix[i,j] = value;
    }

    public Matrix Transposed()
    {
        Matrix TA = new Matrix(RowCount, ColumnCount);

        for (int i = 0; i < RowCount; i++)
            for (int j = 0; j < ColumnCount; j++)
                TA[j,i] = _matrix[i,j];

        return TA;
    }

    public static Matrix operator*(Matrix a, Matrix b)
    {
        if (a.ColumnCount != b.RowCount)
            throw new Exception("Trying to multiply matricies, that cannot be multiplied.");

        int S = a.ColumnCount;

        int N = a.RowCount;
        int M = b.ColumnCount;
        Matrix A = new(N, M);

        for (int i = 0; i < N; i++)
            for (int j = 0; j < M; j++)
                for (int k = 0; k < S; k++)
                    A[i,j] = a[i, k] * b[k, j];
        return A;
    }

    public static Vector operator*(Matrix m, Vector v)
    {
        if (m.ColumnCount != v.Count)
            throw new Exception($"Trying to multiply Matrix with vector with different dimensions (dim(v) = {v.Count}, dim(M) = {m.ColumnCount})");
        Vector mv = new();

        for (int i = 0; i < m.RowCount; i++)
        {
            double sum = 0d;
            for (int j = 0; j < m.ColumnCount; j++)
                sum += m[i, j] * v[j];

            mv.Add(sum);
        }
        return mv;
    }

    public override string ToString() => _matrix?.ToString();

}
