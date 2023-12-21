using OOP.Functionals;
using OOP.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Optimizators
{
    internal class MonteCarlo
    {
        public int MaxIter = 100000;
        public double[] Range { get; set; }
        public IVector Minimize(IFunctional objective, IParametricFunction function, IVector initialParameters, IVector minimumParameters = null, IVector maximumParameters = null)
        {
            var param = new Vector();
            var minparam = new Vector();
            foreach (var p in initialParameters) param.Add(p);
            foreach (var p in initialParameters) minparam.Add(p);
            var fun = function.Bind(param);
            var currentmin = objective.Value(fun);
            var rand = new Random(0);
            int i;
            for (i = 0; i < MaxIter; i++)
            {
                for (int j = 0; j < param.Count; j++) param[j] = Range[0] + rand.NextDouble() * (Range[1] - Range[0]);
                var f = objective.Value(function.Bind(param));
                if (f < currentmin)
                {
                    currentmin = f;
                    for (int j = 0; j < param.Count; j++) minparam[j] = param[j];
                }
            }

            Console.WriteLine($"MonteCarlo calculations are done on iteration = {i}");

            return minparam;
        }
    }
}
