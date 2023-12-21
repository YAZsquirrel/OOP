using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OOP.Functions.MeshForFunction;

namespace OOP.Functions
{
    public enum FunctionType
    {
        Linear,
        Quad,
        Cube
    }
    public static class PointsGenerator
    {
        public static double alpha = 0.2;
        private static Vector coef;
        private static FunctionType type;
        private static double GetNoiseFuncValue(IVector x)
        {
            double result = 0;

            for (int i = 0, d = (int)type + 1; i <= (int)type + 1; i++, d--)
            {
                result += (1 - alpha) * coef[i] * Math.Pow(x[0], d);
            }

            return result;
        }
        public static List<(Vector x, double f)> GenerateRandomPoints(string filename, FunctionType _type)
        {
            type = _type;
            List<MeshParams> split_params = new List<MeshParams>();
            using StreamReader reader = new(filename);
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split('\t');
                    split_params.Add(new MeshParams
                    {
                        begin_point = double.Parse(words[0]),
                        end_point = double.Parse(words[1]),
                        count = int.Parse(words[2])
                    });
                }
            }

            int dimension_count = split_params.Count;

            Random random = new Random();
            coef = new Vector();
            for (int i = 0, d = (int)type + 1; i <= (int)type + 1; i++, d--)
            {
                coef.Add(random.Next(1, 5));
                Console.Write($"{coef[i]}x^{d}\t");
            }

            int points_count = 1;

            foreach (MeshParams split_param in split_params)
                points_count *= split_param.count;

            List<(Vector x, double f)> result = new List<(Vector x, double f)>();
            result.Capacity = points_count;

            int[] index = new int[dimension_count];
            int PPPoint = 0;
            double[] h = new double[dimension_count];

            for (int i = 0; i < dimension_count; i++)
                h[i] = (split_params[i].end_point - split_params[i].begin_point) / split_params[i].count;

            Vector point = new Vector();
            for (int i = 0; i < dimension_count; i++)
                point.Add(split_params[i].begin_point);

            int dim_index = dimension_count - 1;
            while (PPPoint != points_count + 1)
            {
                Vector temp_point = new Vector();
                temp_point.Capacity = dimension_count;
                for (int i = 0; i < dimension_count; i++)
                    temp_point.Add(point[i]);
                PPPoint++;

                result.Add((temp_point, GetNoiseFuncValue(point)));
                index[dim_index]++;
                point[dim_index] = split_params[dim_index].begin_point + index[dim_index] * h[dim_index];
            }
            return result;
        }
    }    
}
