using System.Collections.Generic;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.ConstructMesh
{
    public class BuldMesh
    {
        private Point _step;

        private int _countX;

        private int _countY;

        private int _countZ;

        private readonly Point _pointBegin;

        public BuldMesh(ContractMesh mesh)
        {
            _pointBegin = mesh.PointBegin;
            var pointEnd = mesh.PointEnd;

            var kx = 4.0 / 3.0;
            var ky = 4.0 / 3.0;
            var kz = 4.0 / 3.0;

            _step = new Point
            {
                X = (pointEnd.X - _pointBegin.X) / (4 * kx - kx),
                Y = (pointEnd.Y - _pointBegin.Y) / (4 * ky - ky),
                Z = (pointEnd.Z - _pointBegin.Z) / (4 * kz - kz)
            };

            _countX = (int)(4 * kx - (kx - 1));
            _countY = (int)(4 * ky - (ky - 1));
            _countZ = (int)(4 * kz - (kz - 1));
        }

        public void GetMesh()
        {

        }

        private Node[] GetNodes()
        {
            var nodes = new Node[_countX * _countY * _countZ];
            var tmp = new List<int[]>();

            int i1 = 0;
            int i2 = 0;
            int zi = 1;
            var xyz = new List<double>();

            for (int w = 0; w < _countZ; w++)
            {
                int yi = 1;
                for (int j = 0; j < _countY; j++)
                {
                    int xi = 1;
                    for (int i = 0; i < _countX; i++)
                    {
                        for (int l = 0; l < nodes.Length; l++)
                        {
                            nodes[i1] = new Node
                            {
                                NodeTag = i1 + 1,
                                Point = new Point
                                {
                                    X = _pointBegin.X + _step.X * i,
                                    Y = _pointBegin.Y + _step.Y * j,
                                    Z = _pointBegin.Z + _step.Z * w
                                }
                            };

                            if (!xyz.Contains(_pointBegin.Z + _step.Z * w))
                            {
                                xyz.Add(_pointBegin.Z + _step.Z * w);
                            }
                        }

                        if (j < _countY - 1 && i < _countX - 1 && w < _countZ - 1)
                        {
                            if ((xi % _countX != 0) && (yi % _countY != 0) && (zi % _countZ != 0))
                            {
                                tmp.Add(new int[] { i2, i2 + 1, i2 + _countX, i2 + _countX + 1, i2 + _countX * _countX, i2 + _countX * _countX + 1, i2 + _countX * (_countX + 1), i2 + _countX * (_countX + 1) + 1 });
                            }
                        }
                        i2++;
                        xi++;
                        i1++;
                    }
                    yi++;
                }
                zi++;
            }

            return nodes;
        }
    }
}
