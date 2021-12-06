using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi
{
    public class Psi3 : BaseBasicFunction
    {
        public Psi3()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (region.EndY - p.Y) / region.Hy * (p.Z - region.BeginZ) / region.Hz, 0, 0 };
        }
    }
}
