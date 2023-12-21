using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Functions
{
    class LinearFunction : IParametricFunction
    {
        class InternalLinearFunction : IFunction, IDifferentiableFunction
        {
            public double a, b;

            public IVector Gradient(IVector point) => new Vector() { point[0], 1 };

            public double Value(IVector point) => a * point[0] + b;
        }
        public IFunction Bind(IVector parameters) => new InternalLinearFunction() { a = parameters[0], b = parameters[1] };
    }
}
	


