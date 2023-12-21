using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Functions;
class Polynomial:IParametricFunction
{
    class InternalPolynomial : IFunction
    {
        public IVector a;
        public InternalPolynomial(IVector coefs)
        {
            a = coefs;
        }
        public double Value(IVector point)
        {
            double sum = 0;
            int deg = a.Count - 1;
            for(int i = 0; i < a.Count-1; i++)
            {
                sum += a[i] * Math.Pow(point[0], deg);
                deg--;
            }
            sum += a.Last();
            return sum;
        }
    }
    public IFunction Bind(IVector parameters) => new InternalPolynomial(parameters);
}

