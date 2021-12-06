using System;
using System.Linq;

namespace FemSoftwarePackage.MathematicalObjects
{
    /// <summary>
    /// 
    /// </summary>
    public static class Vector
    {
        /// <summary>
        /// Функция переможения векторов
        /// </summary>
        /// <param name="a">вектор 1</param>
        /// <param name="b">вектор 2</param>
        /// <returns></returns>
        public static double Multiply(double[] a, double[] b)
        {
            return a.Select((t, i) => t * b[i]).Sum();
        }

        /// <summary>
        /// Перемножение вектора на число
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Multiply(double[] a, double b)
        {
            return a.Sum(t => t * b);
        }

        /// <summary>
        /// Сложение матриц
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double[,] MatrixAddition(double[,] a, double[,] b)
        {
            var rows = GetDimensionMatrix(a).Item1;
            var columns = GetDimensionMatrix(a).Item2;

            if (rows != GetDimensionMatrix(b).Item1 &&
                columns != GetDimensionMatrix(b).Item2)
            {
                throw new Exception("Сложение матриц не возможно, размерность разная.");
            }

            var m = new double[rows, columns];

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    m[i, j] = a[i, j] + b[i, j];
                }
            }

            return m;
        }

        /// <summary>
        /// Получить размерность матрицы
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static (int, int) GetDimensionMatrix(double[,] a)
        {
            return (a.GetLength(0), a.GetLength(1));
        }
    }
}
