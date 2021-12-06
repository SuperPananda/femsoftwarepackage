using FemSoftwarePackage.MathematicalObjects.Mesh;
using System;
using System.Collections.Generic;

namespace FemSoftwarePackage.Logic.ConstructMatrix
{
    public abstract class BaseConstructMatrixAndVectors
    {
        protected double[] Weight;

        private readonly double[,] _gaussPoint;

        protected int Size;

        protected Point[] CalcPoint;

        protected double DetJ;

        protected Region Region;

        protected BaseConstructMatrixAndVectors(Region region)
        {
            Region = region;

            Weight = new[]
            {
                320.0 / 361.0,
                320.0 / 361.0,
                320.0 / 361.0,
                320.0 / 361.0,
                320.0 / 361.0,
                320.0 / 361.0,
                121.0 / 361.0,
                121.0 / 361.0,
                121.0 / 361.0,
                121.0 / 361.0,
                121.0 / 361.0,
                121.0 / 361.0,
                121.0 / 361.0,
                121.0 / 361.0,
            };

            _gaussPoint = new[,]
            {
                { -Math.Sqrt(19.0 / 30.0), 0, 0 },
                { Math.Sqrt(19.0 / 30.0), 0, 0 },
                { 0, -Math.Sqrt(19.0 / 30.0), 0},
                { 0, Math.Sqrt(19.0 / 30.0), 0},
                { 0, 0, -Math.Sqrt(19.0 / 30.0) },
                { 0, 0, Math.Sqrt(19.0 / 30.0) },
                { -Math.Sqrt(19.0 / 33.0), -Math.Sqrt(19.0 / 33.0), -Math.Sqrt(19.0 / 33.0) },
                { -Math.Sqrt(19.0 / 33.0), -Math.Sqrt(19.0 / 33.0), Math.Sqrt(19.0 / 33.0) },
                { -Math.Sqrt(19.0 / 33.0), Math.Sqrt(19.0 / 33.0), -Math.Sqrt(19.0 / 33.0) },
                { -Math.Sqrt(19.0 / 33.0), Math.Sqrt(19.0 / 33.0), Math.Sqrt(19.0 / 33.0) },
                { Math.Sqrt(19.0 / 33.0), -Math.Sqrt(19.0 / 33.0), -Math.Sqrt(19.0 / 33.0) },
                { Math.Sqrt(19.0 / 33.0), -Math.Sqrt(19.0 / 33.0), Math.Sqrt(19.0 / 33.0) },
                { Math.Sqrt(19.0 / 33.0), Math.Sqrt(19.0 / 33.0) , -Math.Sqrt(19.0 / 33.0) },
                { Math.Sqrt(19.0 / 33.0), Math.Sqrt(19.0 / 33.0) , Math.Sqrt(19.0 / 33.0) }
            };

            Size = Weight.Length;

            CalcPoint = GetCalcPoints();

            DetJ = GetDetJ();
        }

        private double GetDetJ()
        {
            return Region.Hx * Region.Hy * Region.Hz / 8.0;
        }

        private Point[] GetCalcPoints()
        {
            var calcPoint = new List<Point>();

            for (var i = 0; i < Size; i++)
            {
                var x = Region.Hx * (_gaussPoint[i, 0] + 1) / 2.0 + Region.BeginX;
                var y = Region.Hy * (_gaussPoint[i, 1] + 1) / 2.0 + Region.BeginY;
                var z = Region.Hz * (_gaussPoint[i, 2] + 1) / 2.0 + Region.BeginZ;
                calcPoint.Add(new Point
                {
                    X = x,
                    Y = y,
                    Z = z
                });
            }

            return calcPoint.ToArray();
        }

        public virtual double[,] CreateMatrix()
        {
            return null;
        }
    }
}
