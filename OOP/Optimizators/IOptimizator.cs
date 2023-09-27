using OOP.Functionals;
using OOP.Functions;

namespace OOP.Optimizators;

interface IOptimizator
{
    IVector Minimize(IFunctional objective,
        IParametricFunction function,
        IVector initialParameters,
        IVector minimumParameters = default,
        IVector maximumParameters = default);
}
