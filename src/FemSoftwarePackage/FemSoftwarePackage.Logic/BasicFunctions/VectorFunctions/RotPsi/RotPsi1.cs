using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi1 : BaseBasicFunction
    {
        public RotPsi1()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { 0, (p.Y - region.EndY) / (region.Hy * region.Hz), (region.EndZ - p.Z) / (region.Hy * region.Hz) };
        }
    }
}
