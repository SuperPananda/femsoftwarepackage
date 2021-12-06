using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi10 : BaseBasicFunction
    {
        public RotPsi10()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (region.BeginX - p.X) / (region.Hx * region.Hy), (p.Y - region.EndY) / (region.Hx * region.Hy), 0 };
        }
    }
}
