using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Functions
{
	static class MeshForFunction
	{
		struct MeshParams
		{
			public double begin_point;    // левая граница отрезка
			public double end_point;      // правая граница отрезка
			public int count;       // число интервалов разбиения
		}
		
		// кусочно-линейная функция в явном виде
		static public double PiecewiseFunction(IVector x)
		{
			if (x[0] > 0 && x[0] < 1) return 5 * x[0] + 1;
			return x[0] - 2;
		}

		static public List<(Vector x, double f)> BuildMesh(string filename)
		{
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

				result.Add((temp_point, PiecewiseFunction(point)));
				index[dim_index]++;
				if (index[dim_index] == split_params[dim_index].count)
				{
					temp_point = new Vector();
					temp_point.Capacity = dimension_count;
					for (int i = 0; i < dim_index; i++)
						temp_point.Add(point[i]);
					temp_point.Add(split_params[dim_index].end_point);
					result.Add((temp_point, PiecewiseFunction(point)));
					PPPoint++;
					index[dim_index] = 0;
					point[dim_index] = split_params[dim_index].begin_point;

					bool flag;
					flag = dim_index != 0;   // true если dim_index!=0, false если dim_index==0
					while (flag)
					{
						dim_index--;
						index[dim_index]++;
						if (index[dim_index] < split_params[dim_index].count + 1)
						{
							if (index[dim_index] != split_params[dim_index].count)
								point[dim_index] = split_params[dim_index].begin_point + index[dim_index] * h[dim_index];
							else
								point[dim_index] = split_params[dim_index].end_point;
							dim_index = dimension_count - 1;
							flag = false;
						}
						else
						{
							index[dim_index] = 0;
							point[dim_index] = split_params[dim_index].begin_point;
							flag = dim_index != 0;
						}
					}
				}
				else
					point[dim_index] = split_params[dim_index].begin_point + index[dim_index] * h[dim_index];
			}
			return result;
		}

		static public Vector readLineBorders(string filename)
		{
			MeshParams split_params = new MeshParams();
			using (StreamReader sreader = new StreamReader(filename))
			{
				string? line;
				line = sreader.ReadLine();
				if (line == null)
					return null;
				string[] words = line.Split('\t');
				if (words.Length != 3)
					return null;
				split_params.begin_point = double.Parse(words[0]);
				split_params.end_point = double.Parse(words[1]);
				split_params.count = int.Parse(words[2]);
			}
			Vector result = new Vector();
			result.Capacity = split_params.count + 1;
			double h = (split_params.end_point - split_params.begin_point) / split_params.count;
			for (int i = 0; i < split_params.count; i++)
				result.Add(split_params.begin_point + i * h);
			result.Add(split_params.end_point);

			return result;
		}


	}
}
