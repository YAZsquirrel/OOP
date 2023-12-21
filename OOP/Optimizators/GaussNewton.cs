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
        private double epsilon = 1e-8;
        private double getSquareNorm(IVector vector)
        {
            double result = 0;
            foreach (var v in vector) 
            {
                result += v * v;
            }
            return result;
        }
        public IVector Minimize(IFunctional objective, IParametricFunction function, IVector initialParameters, IVector minimumParameters = null, IVector maximumParameters = null)
        {
            try
            {
                var obj = (ILeastSquaresFunctional)objective;

                var param = new Vector();
                foreach (var p in initialParameters) param.Add(p);

                IFunction func = function.Bind(param);

                Matrix J = (Matrix)obj.Jacobian(func);
                Vector residual = (Vector)obj.Residual(func);

                Matrix A = J * J.Transposed();
                Vector b = J * residual;

                IVector delta = solveSLAE(A, b);

                for (int i = 0; i < maxIter && getSquareNorm(delta) > epsilon; i++)
                {
                    for (int j = 0; j < param.Count; j++)
                    {
                        param[j] -= delta[j];
                    }

                    func = function.Bind(param);

                    J = (Matrix)obj.Jacobian(func);
                    residual = (Vector)obj.Residual(func);

                    A = J * J.Transposed();
                    b = J * residual;

                    delta = solveSLAE(A, b);
                }
                return param;

            }
            catch (InvalidDataException e)
            {
                throw new InvalidDataException("This type of functions have not gradient");
            }

        }

        IVector solveSLAE(IMatrix A, IVector b)
        {
            int i, j, k, n, m;
            n = b.Count();
            double aa, bb;
            for (k = 0; k < n; k++) //Поиск максимального элемента в первом столбце
            {
                aa = Math.Abs(A[k, k]);
                i = k;
                for (m = k + 1; m < n; m++)
                    if (Math.Abs(A[m, k]) > aa)
                    {
                        i = m;
                        aa = Math.Abs(A[m, k]);
                    }

                if (aa == 0)   //проверка на нулевой элемент
                {
                    Console.WriteLine("Система не имеет решений");
                }

                if (i != k)  //  перестановка i-ой строки, содержащей главный элемент k-ой строки
                {
                    for (j = k; j < n; j++)
                    {
                        bb = A[k, j];
                        A[k, j] = A[i, j];
                        A[i, j] = bb;
                    }
                    bb = b[k];
                    b[k] = b[i];
                    b[i] = bb;
                }
                aa = A[k, k];//преобразование k-ой строки (Вычисление масштабирующих множителей)
                A[k, k] = 1;
                for (j = k + 1; j < n; j++)
                    A[k, j] = A[k, j] / aa;
                b[k] /= aa;

                for (i = k + 1; i < n; i++)//преобразование строк с помощью k-ой строки
                {
                    bb = A[i, k];
                    A[i, k] = 0;
                    if (bb != 0)
                    {
                        for (j = k + 1; j < n; j++)
                            A[i, j] = A[i, j] - bb * A[k, j];
                        b[i] -= bb * b[k];
                    }

                }
            }

            for (i = n - 1; i >= 0; i--)   //Нахождение решений СЛАУ
            {

                for (j = n - 1; j > i; j--)
                    b[i] -= A[i, j] * b[j];

            }

            return b;
        }
    }
}
