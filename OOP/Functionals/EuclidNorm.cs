using OOP.Functions;

namespace OOP.Functionals;

// l2 норма разности с требуемыми значениями в наборе точек(реализует IDifferentiableFunctional, реализует ILeastSquaresFunctional)
internal class EuclidNorm : IDifferentiableFunctional, ILeastSquaresFunctional
{
    public IVector Gradient(IFunction function)
    {
        if (function is not IDifferentiableFunction)
            throw new InvalidDataException("Function is not differentiable (does not implement IDifferentiableFunction)");
        if (function is null)
            throw new NullReferenceException($"{nameof(function)} was null.");
        int dim = Points[0].X.Count;

        var dif_f = function as IDifferentiableFunction;
        Vector grad = new Vector();

        for (int i = 0; i < Points.Count; i++)
        {
            Vector point = Points[i].X;
            double gradf = dif_f.Gradient(point)[i];
            double f = dif_f.Value(point) - Points[i].Y;

            for (int n = 0; n < dim; n++)
                grad.Add(2 * f * gradf);
        }

        return grad;
    }

    public IMatrix Jacobian(IFunction function)
    {
        throw new NotImplementedException();
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
