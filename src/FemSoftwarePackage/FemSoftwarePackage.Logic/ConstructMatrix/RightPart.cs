using System;
using System.Linq;
using FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi;
using FemSoftwarePackage.MathematicalObjects;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.ConstructMatrix
{
    /// <summary>
    /// Сборка вектора правой части
    /// </summary>
    public class RightPart : BaseConstructMatrixAndVectors
    {
        public RightPart(Region region)
            : base(region)
        {
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

        public double[] CreateRightPart(int[] numElem, double[] q1, double[] q2, double k1, double k2, double[,] M)
        {
            var funcF = GetFuncF();

            var rightPart = new double[12];
            var matrixM = Matrix();

            for (var i = 0; i < 12; i++)
            {
                for (var j = 0; j < 12; j++)
                {
                    rightPart[i] += matrixM[i, j] * funcF[j] + k1 * M[i, j] * q1[numElem[j]] - k2 * M[i, j] * q2[numElem[j]];
                }
            }

            return rightPart;
        }

        private double[] GetFuncF()
        {
            var func = new[]
            {
                FuncFX((Region.EndX-Region.BeginX)/2.0, Region.BeginY, Region.BeginZ),
                FuncFX((Region.EndX-Region.BeginX)/2.0, Region.EndY, Region.BeginZ),
                FuncFX((Region.EndX-Region.BeginX)/2.0, Region.BeginY, Region.EndZ),
                FuncFX((Region.EndX-Region.BeginX)/2.0, Region.EndY, Region.EndZ),

                FuncFY(Region.BeginX, (Region.EndY-Region.BeginY)/2.0, Region.BeginZ),
                FuncFY(Region.EndX, (Region.EndY-Region.BeginY)/2.0, Region.BeginZ),
                FuncFY(Region.BeginX, (Region.EndY-Region.BeginY)/2.0, Region.EndZ),
                FuncFY(Region.BeginX, (Region.EndY-Region.BeginY)/2.0, Region.EndZ),

                FuncFZ(Region.BeginX, Region.BeginY, (Region.EndZ-Region.BeginZ)/2.0),
                FuncFZ(Region.EndX, Region.BeginY, (Region.EndZ-Region.BeginZ)/2.0),
                FuncFZ(Region.BeginX, Region.EndY, (Region.EndZ-Region.BeginZ)/2.0),
                FuncFZ(Region.EndX, Region.EndY, (Region.EndZ-Region.BeginZ)/2.0),
            };

            return func;
        }

        private static double FuncFX(double x, double y, double z)
        {
            return -2 * Math.Exp(y + z);
        }

        private static double FuncFY(double x, double y, double z)
        {
            return -2 * Math.Exp(x + z);
        }

        private static double FuncFZ(double x, double y, double z)
        {
            return -2 * Math.Exp(y + x);
        }
    }
}
