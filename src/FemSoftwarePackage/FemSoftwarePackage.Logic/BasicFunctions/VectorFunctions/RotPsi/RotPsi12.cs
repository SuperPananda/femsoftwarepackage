using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi12 : BaseBasicFunction
    {
        public RotPsi12()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (p.X - region.BeginX) / (region.Hx * region.Hy), (region.BeginY - p.Y) / (region.Hx * region.Hy), 0 };
        }
    }
}
