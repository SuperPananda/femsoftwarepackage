using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi3 : BaseBasicFunction
    {
        public RotPsi3()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { 0, (region.EndY - p.Y) / (region.Hy * region.Hz), (p.Z - region.BeginZ) / (region.Hy * region.Hz) };
        }
    }
}
