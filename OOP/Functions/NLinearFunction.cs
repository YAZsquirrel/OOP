using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Functions;
class NLinearFunction:IParametricFunction
{
	class InternalNLineFunction : IDifferentiableFunction
	{
		public IVector a;
		public double Value(IVector point)
		{
			double sum = 0;
			for(int i = 0; i < point.Count; i++)
			{
				sum += a[i] * point[i];
			}
			sum += a.Last();
			return sum;
		}
		public IVector Gradient(IVector point)
		{
			IVector gradient = new Vector();

			for (int i = 0; i<point.Count;i++)
			{
				gradient.Add(point[i]);
			}
			gradient.Add(0);
			return gradient;
		}
	}
	public IFunction Bind(IVector parameters) => new InternalNLineFunction() { a = parameters };
}
