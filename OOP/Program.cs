using OOP.Functionals;
using OOP.Functions;
using OOP.Optimizators;

namespace OOP
{
    class Program
    {
        static string path = "D:\\ACS\\Study\\ООП\\OOP\\";


        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            
            //var optimizer = new MonteCarlo();
            var optimizer = new CGM();
            //var optimizer = new GaussNewton();
            var initial = new Vector();
            initial.Add(1);
            initial.Add(1);
            //initial.Add(1);
            //initial.Add(1);

            Vector mesh = MeshForFunction.readLineBorders(path + "Nlinear.txt");
            List<(Vector x, double f)> points = MeshForFunction.BuildMesh(path + "Nlinear.txt");
            //List<(Vector x, double f)> points = PointsGenerator.GenerateRandomPoints(path + "Nlinear.txt", FunctionType.Linear);

            foreach (var point in points)
            {
                foreach (var p in point.x)
                {
                    Console.Write($"({p}, ");
                }
                Console.Write($"{point.f:f7})\n");
            }

            var functinal = new EuclidNorm() { Points = points, ParametersCount = initial.Count };
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

