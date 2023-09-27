using OOP.Functions;

namespace OOP.Functionals;
interface IFunctional
{
    double Value(IFunction function);
}

interface IDifferentiableFunctional : IFunctional
{
    IVector Gradient(IFunction function);
}

interface ILeastSquaresFunctional : IFunctional
{
    IVector Residual(IFunction function);
    IMatrix Jacobian(IFunction function);
}

