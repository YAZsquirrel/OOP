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

//   IOptimizator - метод минимизации, должны быть следующие реализации
//   1. (универсальный) Метод Монте-карло(лучше алгоритм имитации отжига)
//   2. (требующий IDifferentiableFunctional) Метод градиентного спуска(лучше метод сопряжённых градиентов)
//   3. (требующий ILeastSquaresFunctional) Алгоритм Гаусса-Ньютона
