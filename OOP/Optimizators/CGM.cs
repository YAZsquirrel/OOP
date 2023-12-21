﻿using OOP.Functionals;
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
        private double epsilon = 1e-7;
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
        public double GoldSection(double a, double b, IDifferentiableFunctional objective, IVector initialParameters, IParametricFunction function)
        {
            double phi = 1.61803398875;

            double x1, x2;

            while (true)
            {
                x1 = b - (b - a) / phi;
                x2 = a + (b - a) / phi;

                if (Get(x1, objective, initialParameters, function) >= Get(x2, objective, initialParameters, function))
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

            try
            {
                var obj = (IDifferentiableFunctional)objective;

                for (int i = 0; i < maxIter; i++)
                {
                    var lambda = GoldSection(0, 10, obj, initialParameters, function);

                    for (int j = 0; j < param.Count; j++)
                    {
                        param[j] = minparam[j] - lambda * obj.Gradient(function.Bind(minparam))[j];
                    }

                    var functional = objective.Value(function.Bind(param));

                    if (functional > epsilon)
                    {
                        minparam = param;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            catch (Exception e)
            {
                throw new InvalidDataException("This type of functions have not gradient");
            }

            return param;
        }
    }
}