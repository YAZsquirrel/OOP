using OOP.Functions;
namespace OOP.Functionals;

// linf норма разности с требуемыми значениями в наборе точек(не реализует IDifferentiableFunctional, не реализует ILeastSquaresFunctional)
internal class InfNorm : IFunctional
{
    public List<(Vector X, double Y)> Points { get; set; } = new List<(Vector X, double Y)>();

    public double Value(IFunction function)
    {
        double max = 0;

        foreach (var point in Points) 
            max = Math.Max(max, Math.Abs(function.Value(point.X) - point.Y));

        return max;
    }
}
