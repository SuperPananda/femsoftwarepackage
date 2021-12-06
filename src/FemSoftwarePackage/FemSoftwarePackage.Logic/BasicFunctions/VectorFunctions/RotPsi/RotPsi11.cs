using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.RotPsi
{
    public class RotPsi11 : BaseBasicFunction
    {
        public RotPsi11()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (region.EndX - p.X) / (region.Hx * region.Hy), (p.Y - region.BeginY) / (region.Hx * region.Hy), 0 };
        }
    }
}
