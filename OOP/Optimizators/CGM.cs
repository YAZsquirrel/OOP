using OOP.Functionals;
using OOP.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Optimizators
{
    internal class CGM : IOptimizator
    {
        private double epsilon = 1e-14;
        private int maxIter = 10000;
        public double Get(double lambda, IDifferentiableFunctional objective, IVector parameters, IParametricFunction function)
        {
            var vector = new Vector();

            for (int i = 0; i < parameters.Count; i++)
            {
                vector.Add(parameters[i] - lambda * objective.Gradient(function.Bind(parameters))[i]);
            }

            var fun = function.Bind(vector);

            return objective.Value(fun);
        }
        public double GoldSection(double a, double b, IDifferentiableFunctional objective, IVector parameters, IParametricFunction function)
        {
            double phi = 1.61803398875;

            double x1, x2;

            while (true)
            {
                x1 = b - (b - a) / phi;
                x2 = a + (b - a) / phi;

                if (Get(x1, objective, parameters, function) >= Get(x2, objective, parameters, function))
                    a = x1;
                else
                    b = x2;
                if (Math.Abs(a - b) < epsilon)
                    break;
            }
            return (a + b) / 2;
        }
        public IVector Minimize(IFunctional objective, IParametricFunction function, IVector initialParameters, IVector minimumParameters = null, IVector maximumParameters = null)
        {
            var param = new Vector();
            var minparam = new Vector();
            foreach (var p in initialParameters) param.Add(p);
            foreach (var p in initialParameters) minparam.Add(p);
            double functional = objective.Value(function.Bind(param)), prevFunctional, diff = double.MaxValue;
            int i;

            try
            {
                var obj = (IDifferentiableFunctional)objective;

                for (i = 0; i < maxIter && diff > epsilon; i++)
                {                    
                    var lambda = GoldSection(0, 10, obj, param, function);

                    for (int j = 0; j < param.Count; j++)
                    {
                        param[j] = minparam[j] - lambda * obj.Gradient(function.Bind(minparam))[j];
                    }

                    prevFunctional = functional;
                    functional = objective.Value(function.Bind(param));

                    if (functional > epsilon)
                    {
                        minparam = param;
                    }
                    else
                    {
                        break;
                    }
                    diff = Math.Abs(prevFunctional - functional);
                }
            }
            catch (Exception e)
            {
                throw new InvalidDataException("This type of functions have not gradient", e);
            }

            Console.WriteLine($"CGM's calculations are done on diff = {diff}, iteration = {i}");

            return minparam;
        }
    }
}
