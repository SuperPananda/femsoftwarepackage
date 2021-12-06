using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi5 : BaseBasicFunction
    {
        public RotPsi5()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (region.EndX - p.X) / (region.Hx * region.Hz), 0, (p.Z - region.EndZ) / (region.Hx * region.Hz) };
        }
    }
}
