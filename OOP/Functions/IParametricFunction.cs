namespace OOP.Functions;
interface IParametricFunction
{
    IFunction Bind(IVector parameters);
}

interface IFunction
{
    double Value(IVector point);
}

interface IDifferentiableFunction : IFunction
{
    // По параметрам исходной IParametricFunction
    IVector Gradient(IVector point);
}

//  IParametricFunction - параметрическая функция, Bind фиксирует параметры и возвращает следующие реализации
//  1. Линейная функция в n-мерном пространстве(число параметров n+1, реализует IDifferentiableFunction)
//  2. Полином n-й степени в одномерном пространстве(число параметров n+1, не реализует IDifferentiableFunction)
//  3. Кусочно-линейная функция(реализует IDifferentiableFunction)
//  4. Сплайн(не линейный)