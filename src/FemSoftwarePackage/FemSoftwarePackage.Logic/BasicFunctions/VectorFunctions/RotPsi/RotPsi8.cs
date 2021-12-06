using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi8 : BaseBasicFunction
    {
        public RotPsi8()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (region.BeginX - p.X) / (region.Hx * region.Hz), 0, (p.Z - region.BeginZ) / (region.Hx * region.Hz) };
        }
    }
}
