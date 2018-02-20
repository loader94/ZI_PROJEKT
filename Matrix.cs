namespace Projekt_PPMI
{
    class Matrix<T>
    {
        T[,] matrix;
        public Matrix(int i, int j)
        {
            matrix = new T[i, j];
        }
        public void wypelnij_macierz(int i, int j, T value)
        {
            matrix[i, j] = value;
        }
        public T get_matrix_value(int i, int j)
        {
            return matrix[i, j];
        }
    }
}
