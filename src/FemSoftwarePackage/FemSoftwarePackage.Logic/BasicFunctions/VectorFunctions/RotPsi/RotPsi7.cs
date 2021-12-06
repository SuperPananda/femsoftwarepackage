using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi7 : BaseBasicFunction
    {
        public RotPsi7()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (p.X - region.EndX) / (region.Hx * region.Hz), 0, (region.BeginZ - p.Z) / (region.Hx * region.Hz) };
        }
    }
}
