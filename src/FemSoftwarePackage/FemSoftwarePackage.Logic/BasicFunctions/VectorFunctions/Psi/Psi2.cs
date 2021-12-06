using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi
{
    public class Psi2 : BaseBasicFunction
    {
        public Psi2()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (p.Y - region.BeginY) / region.Hy * (region.EndZ - p.Z) / region.Hz, 0, 0 };
        }
    }
}
