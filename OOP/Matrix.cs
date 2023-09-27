namespace OOP;

interface IMatrix : IList<IList<double>>
{
}

// TODO:
// Поменять????
class Matrix : List<IList<double>>, IMatrix
{

}
