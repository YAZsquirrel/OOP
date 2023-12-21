using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Functions;
class PiecewiseLinearFunction : IParametricFunction
{
    public IVector Borders;

    public PiecewiseLinearFunction(IVector borders)
    {
        Borders = borders;
    }

    class InternalPiecewiseLinearFunction : IDifferentiableFunction
    {
        public IVector a;   // коэффициенты
        public IVector Borders_local;
        public InternalPiecewiseLinearFunction(IVector coefs, IVector bordersrefs)
        {
            a = coefs;
            Borders_local = bordersrefs;
        }

        public IVector Gradient(IVector point)
        {
            IVector gradient = new Vector();

            int i;
            if (point[0] < Borders_local[0] || point[0] > Borders_local.Last())
            {
                for (i = 0; i < Borders_local.Count - 1; i++)
                {
                    gradient.Add(0);
                    gradient.Add(0);
                }
            }
            for (i = 0; i < Borders_local.Count - 2 && point[0] > Borders_local[i]; i++)
            {
                gradient.Add(0);
                gradient.Add(0);
            }
            gradient.Add(point[0]);
            gradient.Add(1);
            i++;
            for (; i < Borders_local.Count - 1; i++)
            {
                gradient.Add(0);
                gradient.Add(0);
            }
            return gradient;
        }

        public double Value(IVector point)
        {
            double val;

            if (point[0] < Borders_local[0] || point[0] > Borders_local.Last()) return 0;
            int i;
            for (i = 1; i < Borders_local.Count - 1; i++)
            {
                if (point[0] >= Borders_local[i])
                {
                    val = a[2 * (i - 1)] * point[0] + a[2 * (i - 1) + 1];
                    return val;
                }
            }
            //i--;
            val = a[2 * (i - 1)] * point[0] + a[2 * (i - 1) + 1];
            return val;
        }
    }
    public IFunction Bind(IVector parameters) => new InternalPiecewiseLinearFunction(parameters, Borders);
}

