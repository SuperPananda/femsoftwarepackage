using System.Linq;
using FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi;
using FemSoftwarePackage.MathematicalObjects;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.ConstructMatrix
{
    /// <summary>
    /// Сборка матрицы масс
    /// </summary>
    public class MassMatrix : BaseConstructMatrixAndVectors
    {
        private readonly double _gamma;

        public MassMatrix(double gamma, Region region)
            : base(region)
        {
            _gamma = gamma;
        }

        private double[,] Matrix()
        {
            double sum = 0;

            var functions = new PsiVectorFunctions().Functions.ToArray();
            var matrixA = new double[12, 12];

            int k = 0, j = 0;
            foreach (var psiI in functions)
            {
                foreach (var psiJ in functions)
                {
                    for (var i = 0; i < Size; i++)
                    {
                        sum += Weight[i] * DetJ * Vector.Multiply(psiI.Psi(CalcPoint[i], Region), psiJ.Psi(CalcPoint[i], Region));
                    }
                    matrixA[k, j] = sum;
                    sum = 0;
                    j++;
                }
                j = 0;
                k++;
            }

            return matrixA;
        }

        public override double[,] CreateMatrix()
        {
            var matrixA = Matrix();
            var result = new double[12, 12];

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    result[i, j] = matrixA[i, j] * _gamma;
                }
            }

            return result;
        }
    }
}
