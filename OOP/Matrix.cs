namespace OOP;

interface IMatrix
{
    double this[int i, int j] { get; set; }
}

public class Matrix : IMatrix
{
    private readonly double[,] _matrix;
    public Matrix(int N, int M)
    {
        _matrix = new double[N, M];
    }
    public double this[int i, int j]
    {
        get => _matrix[i,j];
        set => _matrix[i,j] = value;
    }
}
