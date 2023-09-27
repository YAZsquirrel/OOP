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

//  IFunctional - минимизируемый функционал, должны быть следующие реализации
//  1. l1 норма разности с требуемыми значениями в наборе точек(реализует IDifferentiableFunctional, не реализует ILeastSquaresFunctional)
//  2. l2 норма разности с требуемыми значениями в наборе точек(реализует IDifferentiableFunctional, реализует ILeastSquaresFunctional)
//  3. linf норма разности с требуемыми значениями в наборе точек(не реализует IDifferentiableFunctional, не реализует ILeastSquaresFunctional)
//  4. Интеграл по некоторой области(численно)
