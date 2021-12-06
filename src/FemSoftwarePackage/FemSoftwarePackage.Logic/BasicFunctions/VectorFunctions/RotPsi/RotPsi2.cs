using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi2 : BaseBasicFunction
    {
        public RotPsi2()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { 0, (region.BeginY - p.Y) / (region.Hy * region.Hz), (p.Z - region.EndZ) / (region.Hy * region.Hz) };
        }
    }
}
