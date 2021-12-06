using System.Linq;
using FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi;
using FemSoftwarePackage.MathematicalObjects;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.ConstructMatrix
{
    /// <summary>
    /// Сборка матрицы жесткости
    /// </summary>
    public class StiffnessMatrix : BaseConstructMatrixAndVectors
    {
        public StiffnessMatrix(Region region)
            : base(region)
        {
        }

        public override double[,] CreateMatrix()
        {
            double sum = 0;

            var functions = new RotPsiVectorFunctions().Functions.ToArray();
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
    }
}
