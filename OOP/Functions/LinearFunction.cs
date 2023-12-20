using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Functions;
	class LinearFunction:IParametricFunction
	{
		class InternalLinearFunction:IFunction
		{
		public double a, b;
		public double Value(IVector point) => a * point[0] + b;
		}
	public IFunction Bind(IVector parameters) => new InternalLinearFunction() { a = parameters[0], b = parameters[1] };

	}

