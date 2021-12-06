using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi
{
    public class Psi8 : BaseBasicFunction
    {
        public Psi8()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { 0, (p.X - region.BeginX) / region.Hx * (p.Z - region.BeginZ) / region.Hz, 0 };
        }
    }
}
