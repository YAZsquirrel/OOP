using OOP.Functions;
using OOP;

namespace OOP.Functionals;

// l2 норма разности с требуемыми значениями в наборе точек(реализует IDifferentiableFunctional, реализует ILeastSquaresFunctional)
internal class EuclidNorm : IDifferentiableFunctional, ILeastSquaresFunctional
{
    public IVector Gradient(IFunction function)
    {
        if (function is not IDifferentiableFunction dif_f)
            throw new InvalidDataException($"Function {nameof(function)} is not differentiable (does not implement IDifferentiableFunction)");
        if (function is null)
            throw new NullReferenceException($"{nameof(function)} was null.");
        int dim = Parameters.Count;

        Vector grad = new Vector();

        for (int i = 0; i < dim; i++)
        {
            Vector point = Points[i].X;
            double gradf = dif_f.Gradient(point)[i];
            double f = dif_f.Value(point) - Points[i].Y;
            
            grad.Add(2 * f * gradf);
        }

        return grad;
    }

    public IMatrix Jacobian(IFunction function)
    {
        
        if (function is null) throw new NullReferenceException($"{nameof(function)} was null.");
        if (function is not IDifferentiableFunction diff)
            throw new ArgumentException($"{nameof(function)} was not IDifferentiableFunction");

        int M = Points.Count;
        int N = Parameters.Count;
        Matrix J = new(M, N);

        for (int i = 0; i < M; i++)
        {

            var df = Gradient(function);
            //var df = diff.Gradient(Points[i].X);
            for (int j = 0; j < N; j++)
            {
                double f = function.Value(Points[i].X);
                J[i, j] = 2d * f * df[j];

            }
        }

        return J;
    }

    public IVector Residual(IFunction function)
    {
        Vector res = new Vector();
        foreach (var point in Points)
        {
            var f = function.Value(point.X);
            var s = f - point.Y;
            res.Add(s * s / point.Y / point.Y);
        }

        return res;
    }

    public List<(Vector X, double Y)> Points { get; set; } = new List<(Vector X, double Y)>();
    public List<double> Parameters { get; set; } = new();

    public double Value(IFunction function)
    {

        double value = 0;
        foreach (var point in Points)
        {
            var s = function.Value(point.X) - point.Y;
            value += s * s;
        }

        return value;
    }
}
