using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi
{
    public class Psi10 : BaseBasicFunction
    {
        public Psi10()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { 0, 0, (p.X - region.BeginX) / region.Hx * (region.EndY - p.Y) / region.Hy };
        }
    }
}
