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
            //var optimizer = new MonteCarlo();
            //var optimizer = new CGM();
            var optimizer = new GaussNewton();
            var initial = new Vector();
            initial.Add(1);
            initial.Add(1);

            Vector mesh = MeshForFunction.readLineBorders(path + "Nlinear.txt");
            List<(Vector x, double f)> points = MeshForFunction.BuildMesh(path + "Nlinear.txt");

            var functinal = new EuclidNorm() { Points = points, ParametersCount = initial.Count };
            //var fun = new LinearFunction();
            var fun = new PiecewiseLinearFunction(new Vector() { 0, 20} );

            var res = optimizer.Minimize(functinal, fun, initial);
            Console.WriteLine($"a={res[0]},b={res[1]}");
        }
    }
}

