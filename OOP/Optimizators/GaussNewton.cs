using OOP.Functionals;
using OOP.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Optimizators
{
    internal class GaussNewton : IOptimizator
    {
        private int maxIter = 10000;
        public IVector Minimize(IFunctional objective, IParametricFunction function, IVector initialParameters, IVector minimumParameters = null, IVector maximumParameters = null)
        {
            var param = new Vector();
            var minparam = new Vector();
            foreach (var p in initialParameters) param.Add(p);
            foreach (var p in initialParameters) minparam.Add(p);

            try
            {
                var obj = (ILeastSquaresFunctional)objective;

                for (int i = 0; i < maxIter; i++)
                {

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
