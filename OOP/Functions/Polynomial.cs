using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Functions;
class Polynomial:IParametricFunction
{
    class InternalPolynomialFunction : IFunction
    {
        public IVector a;
        public double Value(IVector point)
        {
            double sum = 0;
            for(int i = a.Count - 1; i >= 0; i--)
            {
                sum += a[i] * Math.Pow(point[0], i);
            }
            return sum;
        }
    }
    public IFunction Bind(IVector parameters) => new InternalPolynomialFunction() { a = parameters };
}

