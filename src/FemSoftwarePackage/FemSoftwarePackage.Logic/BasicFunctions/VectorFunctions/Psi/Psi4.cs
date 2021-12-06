using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi
{
    public class Psi4 : BaseBasicFunction
    {
        public Psi4()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (p.Y - region.BeginY) / region.Hy * (p.Z - region.BeginZ) / region.Hz, 0, 0 };
        }
    }
}
