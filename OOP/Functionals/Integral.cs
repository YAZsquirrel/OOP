using OOP.Functions;

namespace OOP.Functionals;

internal class Integral : IFunctional
{
    double[] xj = new []{ .7745966692414833, 0.0, -.7745966692414833 }; // sqrt(0.6)
                                            //= { -sqrt(5. + 2. * (sqrt(10. / 7.))) / 3., -sqrt(5. - 2. * (sqrt(10. / 7.))) / 3.,	// Scales
                                            //           0. , sqrt(5. - 2. * (sqrt(10. / 7.))) / 3. , sqrt(5. + 2. * (sqrt(10. / 7.))) / 3. };

    double[] qj = new []{ .55555555555555555, .8888888888888888, .55555555555555555 };
    //= { (322. - 13. * sqrt(70.)) / 900., (322. + 13. * sqrt(70.)) / 900., 128. / 225.,	// Weights
    //               (322. + 13. * sqrt(70.)) / 900., (322. - 13. * sqrt(70.)) / 900. };

    //public int Dimension { get; init; }
    public (double X0, double X1) Range { get; init; }  


    public double Value(IFunction function)
    {
        double result = 0;

        //Vector w = new Vector(), x = new Vector(); 
        //result = GetNGaussSums(0, w, x, function);
        Vector x = new Vector() {0};
        for (int i = 0; i < 3; i++){
            x[0] = xj[i];
            result += qj[i] * function.Value(x);
        }

        return result / (Range.X1 - Range.X0);
    }

    //double GetNGaussSums(int N, Vector w, Vector x, IFunction function)
    //{
    //    double result = 0;
    //    w.Add(0);
    //    x.Add(0);

    //    for (int i = 0; i < 3; i++)
    //    {
    //        x[N] = xj[i];
    //        result += function.Value(x); 

    //    }
    //    N++;


    //    return result;
    //}

    public Integral(int dimension, (double x0, double x1) range) 
    {
        //Dimension = dimension;
        Range = range; 
    }

    private Integral() { }
}
