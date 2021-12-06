using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi4 : BaseBasicFunction
    {
        public RotPsi4()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { 0, (p.Y - region.BeginZ) / (region.Hy * region.Hz), (region.BeginZ - p.Z) / (region.Hy * region.Hz) };
        }
    }
}
