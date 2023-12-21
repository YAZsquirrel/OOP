using OOP.Functionals;
using OOP.Functions;
using OOP.Optimizators;

namespace OOP
{
    class Program
    {


        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            
            var optimizer = new MonteCarlo() { Range = new double[]{-5, 5 }};
            //var optimizer = new CGM();
            //var optimizer = new GaussNewton();
            var initial = new Vector
            {
                1,
                1,
            //    1,
            //    1,
            };

            Vector mesh = MeshForFunction.readLineBorders("Nlinear.txt");
            //List<(Vector x, double f)> points = MeshForFunction.BuildMesh(path + "Nlinear.txt");
            List<(Vector x, double f)> points = PointsGenerator.GenerateRandomPoints("Nlinear.txt", FunctionType.Linear);

            foreach (var point in points)
            {
                foreach (var p in point.x)
                {
                    Console.Write($"({p:f7}, ");
                }
                Console.Write($"{point.f:f7})\n");
            }

            //var functinal = new EuclidNorm() { Points = points, ParametersCount = initial.Count };
            var functinal = new InfNorm() { Points = points };
            //var functinal = new TaxicabNorm() { Points = points };
            var fun = new LinearFunction();
            //var fun = new PiecewiseLinearFunction(new Vector() { 0, 20} );
            //var fun = new Polynomial();

            var res = optimizer.Minimize(functinal, fun, initial);
            foreach ( var p in res )
            {
                Console.Write($"{p:f7}\t");
            }
        }
    }
}

