using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi9 : BaseBasicFunction
    {
        public RotPsi9()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (p.X - region.EndX) / (region.Hx * region.Hy), (region.EndY - p.Y) / (region.Hx * region.Hy), 0 };
        }
    }
}
