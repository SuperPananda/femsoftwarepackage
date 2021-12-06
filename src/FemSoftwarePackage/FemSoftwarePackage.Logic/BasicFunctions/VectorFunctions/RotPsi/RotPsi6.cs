using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi6 : BaseBasicFunction
    {
        public RotPsi6()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (p.X - region.BeginX) / (region.Hx * region.Hz), 0, (region.EndZ - p.Z) / (region.Hx * region.Hz) };
        }
    }
}
